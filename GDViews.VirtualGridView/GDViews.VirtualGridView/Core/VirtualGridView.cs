using System;
using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid;

public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}

internal interface IVirtualGridViewParent<TDataType, TExtraArgument>
{
    TExtraArgument? ExtraArgument { get; }
    void FocusTo(VirtualGridViewItem<TDataType, TExtraArgument>.CurrentInfo info);
}

public record struct DataSetDefinition<TDataType>(IDynamicGridViewer<TDataType> DataSet, IReadOnlyList<int> DataSpan);

internal class VirtualGridViewImpl<TDataType, TButtonType, TExtraArgument> :
    IVirtualGridViewParent<TDataType, TExtraArgument>,
    IVirtualGridView<TDataType, TButtonType, TExtraArgument> where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    private class DataView
    {
        public NullableData<TDataType> Data;
        public TButtonType? AssignedButton;

        public override string ToString() => $"Button: {AssignedButton?.Name ?? "Null"}, Data: {Data}";
    }

    private record struct DelegateBindings(
        Action FocusEnteredHandler,
        Action FocusExitedHandler,
        Action PressedHandler
    );

    private readonly Vector2I _viewportSize;
    private readonly int _viewportRows;
    private readonly int _viewportColumns;
    private readonly IViewPositioner _viewPositioner;
    private readonly IElementTweener _elementTweener;
    private readonly IElementFader _elementFader;
    private readonly DataLayoutDirection _dataLayoutDirection;

    private readonly ScrollBar? _horizontalScrollBar;
    private readonly ScrollBar? _verticalScrollBar;
    private readonly IDataInspector<TDataType> _dataInspector;
    private readonly IEqualityComparer<TDataType> _equalityComparer;
    private readonly PackedScene _itemPrefab;
    private readonly Control _itemContainer;
    private readonly IInfiniteLayoutGrid _layoutGrid;

    private readonly Stack<TButtonType> _buttonPool;
    private readonly Action<Control> _collectInvincibleControlHandler;

    private readonly TExtraArgument? _extraArgument;

    private DataView[,] _currentView;
    private DataView[,] _nextView;

    private int _currentSelectedViewRowIndex;
    private int _currentSelectedViewColumnIndex;
    private NullableData<TDataType> _currentSelectedData;

    public int ViewColumnIndex { get; private set; }
    public int ViewRowIndex { get; private set; }
    public TExtraArgument? ExtraArgument => _extraArgument;


    public bool GrabLastFocus(LastFocusType lastFocusType)
    {
        Vector2I dataPositionRelativeToViewport;
        switch (lastFocusType)
        {
            case LastFocusType.LastViewFocus:

                dataPositionRelativeToViewport = VirtualGridView.CreatePosition(
                    _currentSelectedViewRowIndex,
                    _currentSelectedViewColumnIndex
                );
                break;
            case LastFocusType.LastDataFocus:

                var lastSelectionData = GetLastDataFocusAbsolutePosition();

                if (lastSelectionData is null) return false;

                var (rowIndex, columnOffset, rowOffset, spanIndex) = lastSelectionData.Value;

                var absoluteDataPosition = VirtualGridView.CreatePosition(
                    rowIndex + rowOffset,
                    columnOffset + spanIndex
                );

                var currentViewOffset = VirtualGridView.CreatePosition(
                    ViewRowIndex,
                    ViewColumnIndex
                );

                dataPositionRelativeToViewport = absoluteDataPosition - currentViewOffset;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lastFocusType), lastFocusType, null);
        }

        return FocusToTarget(dataPositionRelativeToViewport.Y, dataPositionRelativeToViewport.X);
    }

    internal bool FocusToTarget(int viewportRowIndex, int viewportColumnIndex)
    {
        var viewportSize = VirtualGridView.CreatePosition(_viewportRows, _viewportColumns);
        var dataPositionRelativeToViewport = VirtualGridView.CreatePosition(viewportRowIndex, viewportColumnIndex);
        _viewPositioner.GetTargetPosition(
            viewportSize,
            dataPositionRelativeToViewport,
            out var targetDataPosition
        );
        
        if (targetDataPosition < Vector2I.Zero || targetDataPosition >= viewportSize) return false;
        
        var offset = dataPositionRelativeToViewport - targetDataPosition;
        ApplyMovementOffset(offset);
        return true;
    }

    private record struct ViewData(int RowIndex, int ColumnOffset, int RowOffset, int SpanIndex);
    
    private ViewData? GetLastDataFocusAbsolutePosition()
    {
        _dataInspector.GetDataSetMetrics(out var rows, out var columns);
        for (var rowOffset = 0; rowOffset < rows; rowOffset += _viewportRows)
        {
            for (var columnOffset = 0; columnOffset < columns; columnOffset += _viewportColumns)
            {
                for (var rowIndex = 0; rowIndex < _viewportRows; rowIndex++)
                {
                    var columnSpan = _dataInspector.InspectViewColumn(rowIndex, columnOffset, rowOffset);
                    for (var spanIndex = 0; spanIndex < columnSpan.Length; spanIndex++)
                    {
                        var cellData = columnSpan[spanIndex];
                        if (!IsMatch(in cellData, in _currentSelectedData, out var aIsNull, out var bIsNull)) continue;
                        if (aIsNull) continue;
                        return new(rowIndex, columnOffset, rowOffset, spanIndex);
                    }
                }
            }
        }

        return default;
    }

    public bool GrabFocus(IViewFocusFinder focusFinder) => throw new NotImplementedException();

    public bool GrabFocus(IDataFocusFinder<TDataType> focusFinder) => throw new NotImplementedException();

    internal VirtualGridViewImpl(int viewportRows,
        int viewportColumns,
        IViewPositioner viewPositioner,
        IElementTweener elementTweener,
        IElementFader elementFader,
        DataLayoutDirection dataLayoutDirection,
        ScrollBar? horizontalScrollBar,
        ScrollBar? verticalScrollBar,
        IDataInspector<TDataType> dataInspector,
        IEqualityComparer<TDataType> equalityComparer,
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid,
        TExtraArgument? extraArgument)
    {
        _viewportRows = viewportRows;
        _viewportColumns = viewportColumns;
        _viewportSize = new(_viewportRows, _viewportColumns);
        _elementTweener = elementTweener;
        _elementFader = elementFader;
        _viewPositioner = viewPositioner;
        _dataLayoutDirection = dataLayoutDirection;

        _horizontalScrollBar = horizontalScrollBar;
        _verticalScrollBar = verticalScrollBar;
        _dataInspector = dataInspector;
        _equalityComparer = equalityComparer;
        _itemPrefab = itemPrefab;
        _itemContainer = itemContainer;
        _layoutGrid = layoutGrid;
        _extraArgument = extraArgument;

        _collectInvincibleControlHandler = CollectionButtonInstance;
        _currentView = new DataView[_viewportRows, _viewportColumns];
        _nextView = new DataView[_viewportRows, _viewportColumns];

        for (var rowIndex = 0; rowIndex < _viewportRows; rowIndex++)
        for (var columnIndex = 0; columnIndex < _viewportColumns; columnIndex++)
        {
            _currentView[rowIndex, columnIndex] = new() { Data = NullableData<TDataType>.Null };
            _nextView[rowIndex, columnIndex] = new() { Data = NullableData<TDataType>.Null };
        }

        ViewColumnIndex = 0;
        ViewRowIndex = 0;

        var preloadAmount = Math.Max(_viewportRows, _viewportColumns) * 2;
        _buttonPool = new(preloadAmount);
        for (var i = 0; i < preloadAmount; i++)
        {
            var instance = itemPrefab.Instantiate<TButtonType>();
            _buttonPool.Push(instance);
            instance.Hide();
            itemContainer.AddChild(instance, @internal: Node.InternalMode.Front);
        }
    }
    
    private TButtonType GetAndInitializeButtonInstance(TDataType data, int rowIndex, int columnIndex)
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

        instance.DrawGridItem(new(this, rowIndex, columnIndex, data));

        return instance;
    }

    public void FocusTo(VirtualGridViewItem<TDataType, TExtraArgument>.CurrentInfo info)
    {
        _currentSelectedViewRowIndex = info.RowIndex;
        _currentSelectedViewColumnIndex = info.ColumnIndex;
        _currentSelectedData = new(true, info.Data);
        FocusToTarget(_currentSelectedViewRowIndex, _currentSelectedViewColumnIndex);
    }
    
    private void CollectionButtonInstance(Control button)
    {
        var typedButton = (TButtonType)button;
        typedButton.CallDisappear();
        _buttonPool.Push(typedButton);
        button.Hide();
    }

    public void Redraw()
    {
        for (var rowIndex = 0; rowIndex < _viewportRows; rowIndex++)
        {
            var span = _dataInspector.InspectViewColumn(rowIndex, ViewColumnIndex, ViewRowIndex);
            for (var columnIndex = 0; columnIndex < _viewportColumns; columnIndex++)
            {
                var currentDataValue = span[columnIndex];
                var currentViewItem = _currentView[rowIndex, columnIndex];

                if (IsMatch(
                        ref currentDataValue,
                        ref currentViewItem.Data,
                        out var currentDataIsNull,
                        out var currentViewDataIsNull
                    )) continue;

                if (!currentViewDataIsNull)
                {
                    _elementTweener.KillTween(currentViewItem.AssignedButton!);
                    _elementFader.Disappear(currentViewItem.AssignedButton!, _collectInvincibleControlHandler);
                    currentViewItem.AssignedButton = null;
                    currentViewItem.Data = NullableData<TDataType>.Null;
                }

                if (currentDataIsNull) continue;

                currentViewItem.Data = currentDataValue;

                var unwrappedData = currentDataValue.Unwrap();
                var button = GetAndInitializeButtonInstance(unwrappedData, rowIndex, columnIndex);
                button.Position = _layoutGrid.GetGridElementPosition(VirtualGridView.CreatePosition(rowIndex, columnIndex));
                button.CallAppear();
                _elementTweener.KillTween(button);
                _elementFader.Appear(button);
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
            if(moveDirection == Vector2I.Left) Move(MoveDirection.Right);
            if(moveDirection == Vector2I.Right) Move(MoveDirection.Left);
            if(moveDirection == Vector2I.Up) Move(MoveDirection.Down);
            if(moveDirection == Vector2I.Down) Move(MoveDirection.Up);
        }
    }
    
    public void Move(MoveDirection moveDirection)
    {
        Redraw();
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
                isMovingOutLineIndex = _viewportRows - 1;
                isMovingInLineIndex = 0;
                movingOutLineIndex = _viewportRows;
                movingInLineIndex = -1;
                moveDirectionVector = new(-1, 0);
                break;
            case MoveDirection.Down:
                isRow = true;
                isMovingOutLineIndex = 0;
                isMovingInLineIndex = _viewportRows - 1;
                movingOutLineIndex = -1;
                movingInLineIndex = _viewportRows;
                moveDirectionVector = new(1, 0);
                break;
            case MoveDirection.Left:
                isRow = false;
                isMovingOutLineIndex = _viewportColumns - 1;
                isMovingInLineIndex = 0;
                movingOutLineIndex = _viewportColumns;
                movingInLineIndex = -1;
                moveDirectionVector = new(0, -1);
                break;
            case MoveDirection.Right:
                isRow = false;
                isMovingOutLineIndex = 0;
                isMovingInLineIndex = _viewportColumns - 1;
                movingOutLineIndex = -1;
                movingInLineIndex = _viewportColumns;
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

        // Move in-view items to new positions
        for (var rowIndex = 0; rowIndex < _viewportRows; rowIndex++)
        for (var columnIndex = 0; columnIndex < _viewportColumns; columnIndex++)
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
                    currentButton.CallMove(new(this, targetGridPosition.X, targetGridPosition.Y, currentViewItem.Data.Unwrap()));
                    _elementFader.KillTween(currentButton);
                    _elementTweener.MoveTo(currentButton, _layoutGrid.GetGridElementPosition(SwapXY(targetGridPosition)));
                    break;
                case EDGE_OUT:
                    targetGridPosition = isRow ? new(movingOutLineIndex, columnIndex) : new(rowIndex, movingOutLineIndex);
                    currentButton.CallMoveOut();
                    _elementFader.KillTween(currentButton);
                    _elementTweener.MoveOut(currentButton, _layoutGrid.GetGridElementPosition(SwapXY(targetGridPosition)), _collectInvincibleControlHandler);
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
        for (var rowIndex = 0; rowIndex < _viewportRows; rowIndex++)
        {
            var span = _dataInspector.InspectViewColumn(rowIndex, ViewColumnIndex, ViewRowIndex);
            for (var columnIndex = 0; columnIndex < _viewportColumns; columnIndex++)
            {
                var nextViewItem = _nextView[rowIndex, columnIndex];

                if (nextViewItem.AssignedButton is not null) continue;

                var currentValue = span[columnIndex];

                if (!currentValue.TryUnwrap(out var data)) continue;

                nextViewItem.Data = currentValue;

                var movementType = GetEdgeType(rowIndex, columnIndex);

                var newButton = GetAndInitializeButtonInstance(data, rowIndex, columnIndex);
                nextViewItem.AssignedButton = newButton;

                switch (movementType)
                {
                    case EDGE_IN:
                        Vector2I emulatedStartPosition =
                            isRow ?
                                new(columnIndex, movingInLineIndex) :
                                new(movingInLineIndex, rowIndex);

                        newButton.Position = _layoutGrid.GetGridElementPosition(emulatedStartPosition);
                        newButton.CallMoveIn();
                        _elementFader.KillTween(newButton);
                        _elementTweener.MoveIn(newButton, _layoutGrid.GetGridElementPosition(new(columnIndex, rowIndex)));
                        break;
                    default:
                        throw new ArgumentException(nameof(movementType));
                }
            }
        }

        (_currentView, _nextView) = (_nextView, _currentView);

        foreach (var dataView in _nextView)
        {
            dataView.Data = NullableData<TDataType>.Null;
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