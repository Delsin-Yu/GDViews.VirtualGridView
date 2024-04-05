using System;
using System.Collections.Generic;
using Godot;
using GodotViews.Core.FocusFinder;

namespace GodotViews.VirtualGrid;

internal class VirtualGridViewImpl<TDataType, TButtonType, TExtraArgument> :
    IVirtualGridViewParent<TDataType, TExtraArgument>,
    IVirtualGridView<TDataType> where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    private class DataView
    {
        public NullableData<TDataType> Data;
        public TButtonType? AssignedButton;
        public override string ToString() => $"Button: {AssignedButton?.Name ?? "Null"}, Data: {Data}";
    }
    
    private readonly ScrollBar? _horizontalScrollBar;
    private readonly ScrollBar? _verticalScrollBar;
    private readonly int _maxViewColumnIndex;
    private readonly int _maxViewRowIndex;

    private readonly IDataInspector<TDataType> _dataInspector;
    private readonly IEqualityComparer<TDataType> _equalityComparer;
    private readonly Func<TDataType, TDataType, bool> _equalityComparerEquals;
    private readonly PackedScene _itemPrefab;
    private readonly Control _itemContainer;
    private readonly Viewport _containerViewport;
    private readonly IInfiniteLayoutGrid _layoutGrid;
    private readonly Vector2 _cellItemSize;
    private readonly Vector2I _viewportSize;

    private readonly Stack<TButtonType> _buttonPool;
    private readonly HashSet<TButtonType> _movingOutControls = [];
    private readonly Stack<TButtonType> _pendingRemove = [];
    private readonly Action<Control> _collectInvincibleControlHandler;

    private DataView[,] _currentView;
    private DataView[,] _nextView;

    private int _currentSelectedViewRowIndex;
    private int _currentSelectedViewColumnIndex;
    private NullableData<TDataType> _currentSelectedData;

    private Vector2 _startDragPosition;
    private bool _isDragging;

    public int ViewColumnIndex { get; private set; }

    public int ViewRowIndex { get; private set; }

    public int ViewColumns { get; }
    public int ViewRows { get; }
    public TExtraArgument? ExtraArgument { get; }
    public IElementPositioner ElementPositioner { get; set; }
    public IElementTweener ElementTweener { get; set; }
    public IElementFader ElementFader { get; set; }

    public bool GrabFocus() =>
        _currentSelectedData.TryUnwrap(out var currentSelectedData) &&
        TryGetDataPositionRelativeToViewport(_equalityComparerEquals, out var relativeRowIndex, out var relativeColumnIndex, currentSelectedData) &&
        TryGrabFocusCore(relativeRowIndex, relativeColumnIndex) ||
        TryGrabFocusCore(_currentSelectedViewRowIndex, _currentSelectedViewColumnIndex) ||
        GrabFocus(FocusFinders.ByPosition, StartPositions.TopLeft, SearchDirections.RightDown);

    private bool TryGetDataPositionRelativeToViewport(Func<TDataType, TDataType, bool> comparer, out int rowIndex, out int columnIndex, TDataType data)
    {
        rowIndex = -1;
        columnIndex = -1;
        if (!VirtualGridView.SearchForData(_dataInspector, ViewRows, ViewColumns, out var matchedViewData, comparer, data)) return false;
        var (matchedRowIndex, matchedColumnOffset, matchedRowOffset, matchColumnIndex) = matchedViewData;
        var absoluteDataPosition = VirtualGridView.CreatePosition(
            matchedRowOffset + matchedRowIndex,
            matchedColumnOffset + matchColumnIndex
        );

        var currentViewOffset = VirtualGridView.CreatePosition(
            ViewRowIndex,
            ViewColumnIndex
        );

        var relativeDataPosition = absoluteDataPosition - currentViewOffset;

        rowIndex = relativeDataPosition.Y;
        columnIndex = relativeDataPosition.X;
        
        return true;
    }
    
    private bool TryGrabFocusCore(int relativeRowIndex, int relativeColumnIndex)
    {
        FocusToTarget(
            relativeRowIndex,
            relativeColumnIndex,
            out var targetColumnIndex,
            out var targetRowIndex
        );

        var selectedView = _currentView[targetRowIndex, targetColumnIndex].AssignedButton;

        if (selectedView is null) return false;

        selectedView.GrabFocus();

        return true;
    }

    private void FocusToTarget(int viewportRowIndex, int viewportColumnIndex, out int targetRowIndex, out int targetColumnIndex)
    {
        var dataPositionRelativeToViewport = VirtualGridView.CreatePosition(viewportRowIndex, viewportColumnIndex);
        ElementPositioner.GetTargetPosition(
            _viewportSize,
            dataPositionRelativeToViewport,
            out var targetDataPosition
        );
        
        var offset = dataPositionRelativeToViewport - targetDataPosition;
        
        if (targetDataPosition.X < 0 || targetDataPosition.Y < 0 || targetDataPosition.X >= _viewportSize.X || targetDataPosition.Y >= _viewportSize.Y)
        {
            var fixedTargetPosition = targetDataPosition.Clamp(Vector2I.Zero, _viewportSize - Vector2I.One);
            offset += targetDataPosition - fixedTargetPosition;
            targetDataPosition = fixedTargetPosition;
        }

        ApplyMovementOffset(offset);
        (targetRowIndex, targetColumnIndex) = targetDataPosition;
    }


    public bool GrabFocus(IViewFocusFinder focusFinder, StartPositionHandler startPositionHandler, SearchDirection searchDirection)
    {
        var wrapper = new ReadOnlyViewArray(_currentView, ViewRows, ViewColumns, BackingResolver);
        var span = searchDirection.GetSpan();
        return focusFinder.TryResolveFocus(
            in wrapper,
            in span,
            startPositionHandler,
            out var viewRowIndex,
            out var viewColumnIndex
        ) && TryGrabFocusCore(viewRowIndex, viewColumnIndex);
    }

    private static bool BackingResolver(object obj) => !((DataView)obj).Data.IsNull;

    public bool GrabFocus(IEqualityDataFocusFinder focusFinder, in TDataType matchingArgument)
    {
        var wrapper = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewRows, ViewColumns);
        return focusFinder.TryResolveFocus(
            in matchingArgument,
            in wrapper,
            out var dataSetRowIndex,
            out var dataSetColumnIndex
        ) && TryGrabFocusCore(dataSetRowIndex - ViewRowIndex, dataSetColumnIndex - ViewColumnIndex);
    }

    public bool GrabFocus(IPredicateDataFocusFinder focusFinder, Predicate<TDataType> matchingArgument)
    {
        var wrapper = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewRows, ViewColumns);
        return focusFinder.TryResolveFocus(
            in matchingArgument,
            in wrapper,
            out var dataSetRowIndex,
            out var dataSetColumnIndex
        ) && TryGrabFocusCore(dataSetRowIndex - ViewRowIndex, dataSetColumnIndex - ViewColumnIndex);
    }
    
    public bool GrabFocus<TMatchingArgument>(IDataFocusFinder<TMatchingArgument> focusFinder, in TMatchingArgument matchingArgument)
    {
        var wrapper = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewRows, ViewColumns);
        return focusFinder.TryResolveFocus(
            in matchingArgument,
            in wrapper,
            out var rowIndex,
            out var columnIndex
        ) && TryGrabFocusCore(rowIndex, columnIndex);
    }

    private VirtualGridViewItem<TDataType, TExtraArgument>.CurrentInfo ConstructInfo(
        int rowIndex,
        int columnIndex,
        int dataSetMaxRowIndex,
        int dataSetMaxColumnIndex,
        TDataType data)
    {
        var edgeElementType = EdgeElementType.None;

        if (rowIndex == 0) edgeElementType |= EdgeElementType.Up;
        if (columnIndex == 0) edgeElementType |= EdgeElementType.Left;
        if (rowIndex == _maxViewRowIndex) edgeElementType |= EdgeElementType.Down;
        if (columnIndex == _maxViewColumnIndex) edgeElementType |= EdgeElementType.Right;

        var absRowIndex = rowIndex + ViewRowIndex;
        var absColumnIndex = columnIndex + ViewColumnIndex;

        if (absRowIndex == 0) edgeElementType &= ~ EdgeElementType.Up;
        if (absColumnIndex == 0) edgeElementType &= ~ EdgeElementType.Left;
        if (absRowIndex == dataSetMaxRowIndex) edgeElementType &= ~ EdgeElementType.Down;
        if (absColumnIndex == dataSetMaxColumnIndex) edgeElementType &= ~ EdgeElementType.Right;

        return new(
            this,
            rowIndex,
            columnIndex,
            edgeElementType,
            data
        );
    }

    internal VirtualGridViewImpl(
        int viewportRows,
        int viewportColumns,
        IElementPositioner elementPositioner,
        IElementTweener elementTweener,
        IElementFader elementFader,
        ScrollBar? horizontalScrollBar,
        ScrollBar? verticalScrollBar,
        IDataInspector<TDataType> dataInspector,
        IEqualityComparer<TDataType> equalityComparer,
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid,
        TExtraArgument? extraArgument)
    {
        ViewRows = viewportRows;
        ViewColumns = viewportColumns;

        _maxViewRowIndex = ViewRows - 1;
        _maxViewColumnIndex = ViewColumns - 1;

        ElementTweener = elementTweener;
        ElementFader = elementFader;
        ElementPositioner = elementPositioner;

        _horizontalScrollBar = horizontalScrollBar;
        _verticalScrollBar = verticalScrollBar;
        _dataInspector = dataInspector;
        _equalityComparer = equalityComparer;
        _equalityComparerEquals = equalityComparer.Equals;
        _itemPrefab = itemPrefab;
        _itemContainer = itemContainer;
        _layoutGrid = layoutGrid;
        _cellItemSize = layoutGrid.ItemSize * 0.75f;
        ExtraArgument = extraArgument;

        _containerViewport = _itemContainer.GetViewport();
        _itemContainer.GuiInput += ProcessScrollWheelAndDragInput;
        _itemContainer.MouseExited += () => _isDragging = false;

        _collectInvincibleControlHandler = CollectButtonInstance;
        _currentView = new DataView[ViewRows, ViewColumns];
        _nextView = new DataView[ViewRows, ViewColumns];
        _viewportSize = VirtualGridView.CreatePosition(ViewRows, ViewColumns);

        for (var rowIndex = 0; rowIndex < ViewRows; rowIndex++)
        for (var columnIndex = 0; columnIndex < ViewColumns; columnIndex++)
        {
            _currentView[rowIndex, columnIndex] = new() { Data = NullableData.Null<TDataType>() };
            _nextView[rowIndex, columnIndex] = new() { Data = NullableData.Null<TDataType>() };
        }

        ViewColumnIndex = 0;
        ViewRowIndex = 0;

        var preloadAmount = Math.Max(ViewRows, ViewColumns) * 2;
        _buttonPool = new(preloadAmount);
        for (var i = 0; i < preloadAmount; i++)
        {
            var instance = itemPrefab.Instantiate<TButtonType>();
            _buttonPool.Push(instance);
            instance.Hide();
            itemContainer.AddChild(instance, @internal: Node.InternalMode.Front);
        }
    }

    private void ProcessScrollWheelAndDragInput(InputEvent inputEvent)
    {
        using (inputEvent)
        {
            if (this != VirtualGridView.CurrentActiveGridView) return;

            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                if (!_isDragging)
                {
                    _isDragging = true;
                    _startDragPosition = _containerViewport.GetMousePosition();
                }
            }
            else
            {
                if (_isDragging)
                {
                    _isDragging = false;
                    _startDragPosition = Vector2.Zero;
                }
            }


            MoveDirection simulatedMoveDirection;

            switch (inputEvent)
            {
                case InputEventMouseButton mouseButton:

                    var mouseButtonButtonIndex = mouseButton.ButtonIndex;

                    var mapVH = mouseButton.GetModifiersMask().HasFlag(KeyModifierMask.MaskShift);
                    switch (mouseButtonButtonIndex)
                    {
                        case MouseButton.WheelUp:
                            simulatedMoveDirection = mapVH ? MoveDirection.Left : MoveDirection.Up;
                            break;
                        case MouseButton.WheelDown:
                            simulatedMoveDirection = mapVH ? MoveDirection.Right : MoveDirection.Down;
                            break;
                        case MouseButton.WheelLeft:
                            simulatedMoveDirection = MoveDirection.Left;
                            break;
                        case MouseButton.WheelRight:
                            simulatedMoveDirection = MoveDirection.Right;
                            break;
                        default:
                            return;
                    }

                    break;
                case InputEventMouseMotion mouseMotion:
                    if (_isDragging == false) return;
                    if (!TryGetMoveDirection(
                            ref _startDragPosition,
                            mouseMotion.GlobalPosition,
                            _cellItemSize,
                            out simulatedMoveDirection
                        )) return;
                    break;
                default: return;
            }

            var currentFocusPosition = VirtualGridView.CreatePosition(
                _currentSelectedViewRowIndex,
                _currentSelectedViewColumnIndex
            );

            ElementPositioner.GetDragViewPosition(
                _viewportSize,
                simulatedMoveDirection,
                currentFocusPosition,
                out var targetFocusPosition
            );

            if (currentFocusPosition != targetFocusPosition)
            {
                _currentView[targetFocusPosition.Y, targetFocusPosition.X]
                    .AssignedButton?.GrabFocus();
            }

            var eventName = simulatedMoveDirection switch
            {
                MoveDirection.Up => VirtualGridView._uiUp,
                MoveDirection.Down => VirtualGridView._uiDown,
                MoveDirection.Left => VirtualGridView._uiLeft,
                MoveDirection.Right => VirtualGridView._uiRight,
                _ => throw new InvalidOperationException()
            };

            Input.ParseInputEvent(new InputEventAction { Pressed = true, Action = eventName });
        }
    }

    private static bool TryGetMoveDirection(ref Vector2 startDragPosition, Vector2 currentPosition, Vector2 objectDistance, out MoveDirection simulateDirection)
    {
        var mouseTravelDistance = startDragPosition - currentPosition;
        var sign = mouseTravelDistance.Sign();
        var absDistance = mouseTravelDistance.Abs();
        var diff = absDistance - objectDistance;


        if (diff.X > 0)
        {
            startDragPosition += new Vector2(objectDistance.X * -sign.X, 0);
            simulateDirection = sign.X > 0 ? MoveDirection.Right : MoveDirection.Left;
            return true;
        }

        if (diff.Y > 0)
        {
            startDragPosition += new Vector2(0, objectDistance.Y * -sign.Y);
            simulateDirection = sign.Y > 0 ? MoveDirection.Down : MoveDirection.Up;
            return true;
        }

        simulateDirection = (MoveDirection)(-1);
        return false;
    }

    private TButtonType GetAndInitializeButtonInstance(
        TDataType data,
        int rowIndex,
        int columnIndex,
        int dataSetMaxRowIndex,
        int dataSetMaxColumnIndex
    )
    {
        if (!_buttonPool.TryPop(out var instance))
        {
            instance = _itemPrefab.Instantiate<TButtonType>();
            _itemContainer.AddChild(instance);
        }
        else
        {
            instance.Show();
        }

        instance.FocusMode = Control.FocusModeEnum.All;
        instance.MouseFilter = Control.MouseFilterEnum.Pass;
        instance.DrawGridItem(ConstructInfo(rowIndex, columnIndex, dataSetMaxRowIndex, dataSetMaxColumnIndex, data));

        return instance;
    }

    public void FocusTo(VirtualGridViewItem<TDataType, TExtraArgument>.CurrentInfo info)
    {
        _currentSelectedViewRowIndex = info.RowIndex;
        _currentSelectedViewColumnIndex = info.ColumnIndex;
        _currentSelectedData = NullableData.Create<TDataType>(info.Data!);
        FocusToTarget(
            _currentSelectedViewRowIndex,
            _currentSelectedViewColumnIndex,
            out _,
            out _
        );
    }

    private void CollectButtonInstance(Control button)
    {
        ElementTweener.KillTween(button);
        ElementFader.KillTween(button);
        ElementFader.Show(button);
        var typedButton = (TButtonType)button;
        typedButton.CallDisappear();
        _movingOutControls.Remove(typedButton);
        _buttonPool.Push(typedButton);
        button.Hide();
    }

    public void Redraw() => Redraw(out _, out _);


    private void Redraw(out int dataSetMaxRowIndex, out int dataSetMaxColumnIndex)
    {
        {
            _dataInspector.GetDataSetCurrentMetrics(out var rows, out var columns);
            dataSetMaxRowIndex = rows - 1;
            dataSetMaxColumnIndex = columns - 1;
        }

        for (var rowIndex = 0; rowIndex < ViewRows; rowIndex++)
        {
            var span = _dataInspector.InspectViewColumn(rowIndex, ViewColumnIndex, ViewRowIndex);
            for (var columnIndex = 0; columnIndex < ViewColumns; columnIndex++)
            {
                var currentDataValue = span[columnIndex];
                var currentViewItem = _currentView[rowIndex, columnIndex];

                if (IsMatch(
                        ref currentDataValue,
                        ref currentViewItem.Data,
                        out var currentDataIsNull,
                        out var currentViewDataIsNull
                    ))
                {
                    if (!currentViewDataIsNull)
                    {
                        currentViewItem.AssignedButton!.Info =
                            ConstructInfo(
                                rowIndex,
                                columnIndex,
                                dataSetMaxRowIndex,
                                dataSetMaxColumnIndex,
                                currentDataValue.Unwrap()
                            );
                    }

                    continue;
                }

                if (!currentViewDataIsNull)
                {
                    var assignedButton = currentViewItem.AssignedButton!;
                    ElementTweener.KillTween(assignedButton);
                    _movingOutControls.Add(assignedButton);
                    ElementFader.Disappear(assignedButton, _collectInvincibleControlHandler);
                    currentViewItem.AssignedButton = null;
                    currentViewItem.Data = NullableData.Null<TDataType>();
                }

                if (currentDataIsNull) continue;

                currentViewItem.Data = currentDataValue;

                var unwrappedData = currentDataValue.Unwrap();
                var button = GetAndInitializeButtonInstance(unwrappedData, rowIndex, columnIndex, dataSetMaxRowIndex, dataSetMaxColumnIndex);
                button.Position = _layoutGrid.GetGridElementPosition(VirtualGridView.CreatePosition(rowIndex, columnIndex));
                button.CallAppear();
                ElementTweener.KillTween(button);
                ElementFader.Appear(button);
                currentViewItem.AssignedButton = button;
            }
        }
    }

    private bool IsMatch(ref readonly NullableData<TDataType> a, ref readonly NullableData<TDataType> b, out bool aIsNull, out bool bIsNull)
    {
        aIsNull = a.IsNull;
        bIsNull = b.IsNull;

        if (aIsNull == bIsNull)
        {
            if (aIsNull) return true;
            return _equalityComparer.Equals(a.Unwrap(), b.Unwrap());
        }

        return false;
    }

    private void ApplyMovementOffset(Vector2I offset)
    {
        while (VirtualGridView.TryGetMoveDirection(ref offset, out var moveDirection))
        {
            if (moveDirection == Vector2I.Left) Move(MoveDirection.Right);
            if (moveDirection == Vector2I.Right) Move(MoveDirection.Left);
            if (moveDirection == Vector2I.Up) Move(MoveDirection.Down);
            if (moveDirection == Vector2I.Down) Move(MoveDirection.Up);
        }
    }

    public void MoveAndGrabFocus(MoveDirection moveDirection, int rowIndex, int columnIndex)
    {
        Move(moveDirection);
        _currentView[rowIndex, columnIndex].AssignedButton?.GrabFocus();
    }

    public void Move(MoveDirection moveDirection)
    {
        Redraw(out var dataSetMaxRowIndex, out var dataSetMaxColumnIndex);
        Vector2I moveDirectionVector;
        int isMovingOutLineIndex;
        int isMovingInLineIndex;
        int movingOutLineIndex;
        int movingInLineIndex;
        bool isRow;
        switch (moveDirection)
        {
            case MoveDirection.Up:
                isRow = true;
                isMovingOutLineIndex = ViewRows - 1;
                isMovingInLineIndex = 0;
                movingOutLineIndex = ViewRows;
                movingInLineIndex = -1;
                moveDirectionVector = new(-1, 0);
                break;
            case MoveDirection.Down:
                isRow = true;
                isMovingOutLineIndex = 0;
                isMovingInLineIndex = ViewRows - 1;
                movingOutLineIndex = -1;
                movingInLineIndex = ViewRows;
                moveDirectionVector = new(1, 0);
                break;
            case MoveDirection.Left:
                isRow = false;
                isMovingOutLineIndex = ViewColumns - 1;
                isMovingInLineIndex = 0;
                movingOutLineIndex = ViewColumns;
                movingInLineIndex = -1;
                moveDirectionVector = new(0, -1);
                break;
            case MoveDirection.Right:
                isRow = false;
                isMovingOutLineIndex = 0;
                isMovingInLineIndex = ViewColumns - 1;
                movingOutLineIndex = -1;
                movingInLineIndex = ViewColumns;
                moveDirectionVector = new(0, 1);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
        }

        ViewRowIndex += moveDirectionVector.X;
        ViewColumnIndex += moveDirectionVector.Y;

        const byte EDGE_NORMAL = 0;
        const byte EDGE_OUT = 1;
        const byte EDGE_IN = 2;

        foreach (var movingOutControl in _movingOutControls)
            _pendingRemove.Push(movingOutControl);

        while (_pendingRemove.TryPop(out var movingOutControl))
        {
            ElementTweener.KillTween(movingOutControl);
            ElementFader.KillTween(movingOutControl);
            CollectButtonInstance(movingOutControl);
        }

        // Move in-view items to new positions
        for (var rowIndex = 0; rowIndex < ViewRows; rowIndex++)
        for (var columnIndex = 0; columnIndex < ViewColumns; columnIndex++)
        {
            var currentViewItem = _currentView[rowIndex, columnIndex];
            var currentButton = currentViewItem.AssignedButton;

            if (currentButton is null) continue;

            var currentGridIndex = new Vector2I(rowIndex, columnIndex);
            var movementType = GetEdgeType(rowIndex, columnIndex);

            Vector2I targetGridPosition;
            switch (movementType)
            {
                case EDGE_NORMAL:
                case EDGE_IN:
                    targetGridPosition = currentGridIndex - moveDirectionVector;
                    currentButton.CallMove(
                        ConstructInfo(
                            targetGridPosition.X,
                            targetGridPosition.Y,
                            dataSetMaxRowIndex,
                            dataSetMaxColumnIndex,
                            currentViewItem.Data.Unwrap()
                        )
                    );
                    ElementTweener.MoveTo(currentButton, _layoutGrid.GetGridElementPosition(SwapXY(targetGridPosition)));
                    break;
                case EDGE_OUT:
                    targetGridPosition = isRow ? new(movingOutLineIndex, columnIndex) : new(rowIndex, movingOutLineIndex);
                    currentButton.CallMoveOut();
                    currentButton.FocusMode = Control.FocusModeEnum.None;
                    currentButton.MouseFilter = Control.MouseFilterEnum.Ignore;
                    _movingOutControls.Add(currentButton);
                    ElementTweener.MoveOut(currentButton, _layoutGrid.GetGridElementPosition(SwapXY(targetGridPosition)), _collectInvincibleControlHandler);
                    break;
                default:
                    throw new ArgumentException(nameof(movementType));
            }

            if (movementType is EDGE_NORMAL or EDGE_IN)
            {
                var nextViewItem = _nextView[targetGridPosition.X, targetGridPosition.Y];
                nextViewItem.AssignedButton = currentButton;
                nextViewItem.Data = currentViewItem.Data;
            }
        }

        // Resolve newly occured item
        for (var rowIndex = 0; rowIndex < ViewRows; rowIndex++)
        {
            var span = _dataInspector.InspectViewColumn(rowIndex, ViewColumnIndex, ViewRowIndex);
            for (var columnIndex = 0; columnIndex < ViewColumns; columnIndex++)
            {
                var nextViewItem = _nextView[rowIndex, columnIndex];

                if (nextViewItem.AssignedButton is not null) continue;

                var currentValue = span[columnIndex];

                if (!currentValue.TryUnwrap(out var data)) continue;

                nextViewItem.Data = currentValue;

                var movementType = GetEdgeType(rowIndex, columnIndex);

                var newButton = GetAndInitializeButtonInstance(
                    data,
                    rowIndex,
                    columnIndex,
                    dataSetMaxRowIndex,
                    dataSetMaxColumnIndex
                );
                nextViewItem.AssignedButton = newButton;

                switch (movementType)
                {
                    case EDGE_IN:
                        Vector2I emulatedStartPosition =
                            isRow ? new(columnIndex, movingInLineIndex) : new(movingInLineIndex, rowIndex);

                        newButton.Position = _layoutGrid.GetGridElementPosition(emulatedStartPosition);
                        newButton.CallMoveIn();
                        ElementFader.KillTween(newButton);
                        ElementTweener.MoveIn(newButton, _layoutGrid.GetGridElementPosition(new(columnIndex, rowIndex)));
                        break;
                    default:
                        throw new ArgumentException(nameof(movementType));
                }
            }
        }

        (_currentView, _nextView) = (_nextView, _currentView);

        foreach (var dataView in _nextView)
        {
            dataView.Data = NullableData.Null<TDataType>();
            dataView.AssignedButton = null;
        }

        return;

        byte GetEdgeType(int rowIndex, int columnIndex)
        {
            if (isRow)
            {
                if (rowIndex == isMovingOutLineIndex) return EDGE_OUT;
                if (rowIndex == isMovingInLineIndex) return EDGE_IN;
                return EDGE_NORMAL;
            }

            if (columnIndex == isMovingOutLineIndex) return EDGE_OUT;
            if (columnIndex == isMovingInLineIndex) return EDGE_IN;
            return EDGE_NORMAL;
        }
    }

    private static Vector2I SwapXY(Vector2I vector2I) => new(vector2I.Y, vector2I.X);
}