using System;
using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid;

internal class VirtualGridViewImpl<TDataType, TButtonType, TExtraArgument> :
    IVirtualGridViewParent<TDataType, TExtraArgument>,
    IVirtualGridView<TDataType> where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    private readonly Stack<TButtonType> _buttonPool;
    private readonly Vector2 _cellItemSize;
    private readonly Action<Control> _collectInvincibleControlHandler;
    private readonly Viewport _containerViewport;

    private readonly IDataInspector<TDataType> _dataInspector;
    private readonly IEqualityComparer<TDataType> _equalityComparer;
    private readonly Func<TDataType, TDataType, bool> _equalityComparerEquals;


    private readonly ScrollBar? _horizontalScrollBar;
    private readonly Control _itemContainer;
    private readonly PackedScene _itemPrefab;
    private readonly IInfiniteLayoutGrid _layoutGrid;
    private readonly HashSet<TButtonType> _movingOutControls = [];
    private readonly Stack<TButtonType> _pendingRemove = [];
    private readonly ScrollBar? _verticalScrollBar;
    private readonly Vector2I _viewportSize;
    
    private IElementPositioner _elementPositioner;
    private IElementTweener _elementTweener;
    private IElementFader _elementFader;
    private IScrollBarTweener _hScrollBarTweener;
    private IScrollBarTweener _vScrollBarTweener;
    private IElementFader _hScrollBarFader;
    private IElementFader _vScrollBarFader;

    private NullableData<TDataType> _currentSelectedData;
    private int _currentSelectedViewColumnIndex;

    private int _currentSelectedViewRowIndex;

    private DataView[,] _currentView;
    private bool _isDragging;

    private bool _isHorizontalScrollBarVisible = true;
    private bool _isVerticalScrollBarVisible = true;
    private DataView[,] _nextView;

    private Vector2 _startDragPosition;

    internal VirtualGridViewImpl(
        int viewportRows,
        int viewportColumns,
        IElementPositioner elementPositioner,
        IElementTweener elementTweener,
        IElementFader elementFader,
        ScrollBar? horizontalScrollBar,
        bool autoHideHorizontalScrollBar,
        IScrollBarTweener horizontalScrollBarTweener,
        IElementFader horizontalScrollBarFader,
        ScrollBar? verticalScrollBar,
        bool autoHideVerticalScrollBar,
        IScrollBarTweener verticalScrollBarTweener,
        IElementFader verticalScrollBarFader,
        IDataInspector<TDataType> dataInspector,
        IEqualityComparer<TDataType> equalityComparer,
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid,
        TExtraArgument? extraArgument
    )
    {
        ViewRows = viewportRows;
        ViewColumns = viewportColumns;

        
        _elementPositioner = elementPositioner;
        _elementTweener = elementTweener;
        _elementFader = elementFader;
        
        _hScrollBarTweener = horizontalScrollBarTweener;
        _hScrollBarFader = horizontalScrollBarFader;
        _vScrollBarTweener = verticalScrollBarTweener;
        _vScrollBarFader = verticalScrollBarFader;

        AutoHideHScrollBar = autoHideHorizontalScrollBar;
        AutoHideVScrollBar = autoHideVerticalScrollBar;

        _horizontalScrollBar = horizontalScrollBar;
        _verticalScrollBar = verticalScrollBar;

        InitializeScrollBar(_horizontalScrollBar);
        InitializeScrollBar(_verticalScrollBar);

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
        _viewportSize = Utils.CreatePosition(ViewRows, ViewColumns);

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
        }

        return;

        static void InitializeScrollBar(ScrollBar? scrollBar)
        {
            if (scrollBar is null) return;
            scrollBar.Rounded = false;
            scrollBar.MaxValue = 1f;
            scrollBar.MinValue = 0f;
            scrollBar.Step = 0;
            scrollBar.FocusMode = Control.FocusModeEnum.None;
            scrollBar.MouseFilter = Control.MouseFilterEnum.Ignore;
        }
    }

    public int ViewColumnIndex { get; private set; }
    public int ViewRowIndex { get; private set; }

    public int ViewColumns { get; }
    public int ViewRows { get; }
    
    public IElementPositioner ElementPositioner
    {
        get => _elementPositioner;
        set => _elementPositioner = value ?? ElementPositioners.Side;
    }

    public IElementTweener ElementTweener
    {
        get => _elementTweener;
        set => _elementTweener = value ?? ElementTweeners.None;
    }

    public IElementFader ElementFader
    {
        get => _elementFader;
        set => _elementFader = value ?? ElementFaders.None;
    }

    public IScrollBarTweener HScrollBarTweener
    {
        get => _hScrollBarTweener;
        set => _hScrollBarTweener = value ?? ScrollBarTweeners.None;
    }

    public IScrollBarTweener VScrollBarTweener
    {
        get => _vScrollBarTweener;
        set => _vScrollBarTweener = value ?? ScrollBarTweeners.None;
    }

    public IElementFader HScrollBarFader
    {
        get => _hScrollBarFader;
        set => _hScrollBarFader = value ?? ElementFaders.None;
    }

    public IElementFader VScrollBarFader
    {
        get => _vScrollBarFader;
        set => _vScrollBarFader = value ?? ElementFaders.None;
    }

    public bool AutoHideHScrollBar { get; set; }
    public bool AutoHideVScrollBar { get; set; }

    public bool GrabFocus() =>
        (_currentSelectedData.TryUnwrap(out var currentSelectedData) &&
         TryGetDataPositionRelativeToViewport(
             _equalityComparerEquals,
             out var relativeRowIndex,
             out var relativeColumnIndex,
             currentSelectedData
         ) &&
         TryGrabFocusCore(relativeRowIndex, relativeColumnIndex)) ||
        TryGrabFocusCore(_currentSelectedViewRowIndex, _currentSelectedViewColumnIndex) ||
        ((IVirtualGridView<TDataType>)this).GrabFocus(FocusPresets.TopLeftView) ||
        ((IVirtualGridView<TDataType>)this).GrabFocus(FocusPresets.TopLeftData);


    public bool GrabFocus<TArgument>(
        IViewFocusFinder<TArgument> focusFinder,
        IViewStartHandler<TArgument> startHandler,
        TArgument argument,
        SearchDirection searchDirection
    )
    {
        var wrapper = new ReadOnlyViewArray(_currentView, ViewRows, ViewColumns, BackingResolver);
        var span = searchDirection.GetSpan();
        return focusFinder.TryResolveFocus(
            in wrapper,
            in span,
            startHandler,
            in argument,
            out var viewRowIndex,
            out var viewColumnIndex
        ) && TryGrabFocusCore(viewRowIndex, viewColumnIndex);
    }

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

    public bool GrabFocus(IPredicateDataFocusFinder focusFinder, Predicate<TDataType> predicate)
    {
        var wrapper = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewRows, ViewColumns);
        return focusFinder.TryResolveFocus(
            in predicate,
            in wrapper,
            out var dataSetRowIndex,
            out var dataSetColumnIndex
        ) && TryGrabFocusCore(dataSetRowIndex - ViewRowIndex, dataSetColumnIndex - ViewColumnIndex);
    }

    public bool GrabFocus<TMatchingExtraArgument>(
        IPredicateDataFocusFinder focusFinder,
        Func<TDataType, TMatchingExtraArgument, bool> predicate,
        TMatchingExtraArgument extraArgument
    )
    {
        var wrapper = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewRows, ViewColumns);
        return focusFinder.TryResolveFocus(
            in predicate,
            in wrapper,
            extraArgument,
            out var dataSetRowIndex,
            out var dataSetColumnIndex
        ) && TryGrabFocusCore(dataSetRowIndex - ViewRowIndex, dataSetColumnIndex - ViewColumnIndex);
    }

    public bool GrabFocus<TArgument>(
        IDataFocusFinder<TArgument> focusFinder,
        IDataStartHandler<TArgument> startHandler,
        TArgument argument,
        SearchDirection searchDirection
    )
    {
        var wrapper = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewRows, ViewColumns);
        var span = searchDirection.GetSpan();
        return focusFinder.TryResolveFocus(
            in wrapper,
            in span,
            startHandler,
            in argument,
            out var dataSetRowIndex,
            out var dataSetColumnIndex
        ) && TryGrabFocusCore(dataSetRowIndex - ViewRowIndex, dataSetColumnIndex - ViewColumnIndex);
    }

    public void Redraw()
    {
        Redraw(
            out _,
            out _,
            out _,
            out _,
            out _,
            out _,
            out var dataSetMaxRowIndex,
            out var dataSetMaxColumnIndex
        );
        UpdateScrollBar(dataSetMaxRowIndex + 1, dataSetMaxColumnIndex + 1);
    }

    public TExtraArgument? ExtraArgument { get; }

    public void FocusTo(VirtualGridViewItem<TDataType, TExtraArgument>.CellInfo info)
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

    /// <summary>
    /// This method tries to find the best next candidate
    /// in the given <paramref name="moveDirection"/>
    /// of the provided <paramref name="rowIndex"/> and <paramref name="columnIndex"/>.
    /// </summary>
    public void MoveAndGrabFocus(MoveDirection moveDirection, int rowIndex, int columnIndex)
    {
        ReadOnlySpan<Vector2I> searchDirection = moveDirection switch
        {
            MoveDirection.Up => [SearchDirections.SearchUp, SearchDirections.SearchLeft, SearchDirections.SearchRight],
            MoveDirection.Down => [SearchDirections.SearchDown, SearchDirections.SearchRight, SearchDirections.SearchLeft],
            MoveDirection.Left => [SearchDirections.SearchLeft, SearchDirections.SearchUp, SearchDirections.SearchDown],
            MoveDirection.Right => [SearchDirections.SearchRight, SearchDirections.SearchUp, SearchDirections.SearchDown],
            _ => throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null)
        };

        var absoluteStart = new Vector2I(ViewRowIndex + rowIndex, ViewColumnIndex + columnIndex) + searchDirection[0];
        var readOnlyDataArray = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewRows, ViewColumns);

        // TODO: Relying on BFS for searching for matching is stupid,
        // this leads to serious performance degradation when
        // the BFS walks through a very long distance.
        // We should either:
        //     Develop a new matching algorithm.
        //     Optimize the hell out of the BFSCore, as accessing cell data involves a lot of calculations.
        if (!FocusFinders.BFSSearch.BFSCore(
                in absoluteStart,
                in readOnlyDataArray,
                in searchDirection,
                out var targetAbsoluteRowIndex,
                out var targetAbsoluteColumnIndex
            )) return;

        if (!readOnlyDataArray.TryGetData(targetAbsoluteRowIndex, targetAbsoluteColumnIndex, out var data)) return;
        GrabFocus(FocusFinders.Value, data);
    }

    private bool TryGetDataPositionRelativeToViewport(Func<TDataType, TDataType, bool> comparer, out int rowIndex, out int columnIndex, TDataType data)
    {
        rowIndex = -1;
        columnIndex = -1;
        if (!Utils.SearchForData(_dataInspector, ViewRows, ViewColumns, out var matchedViewData, comparer, data)) return false;
        var (matchedRowIndex, matchedColumnOffset, matchedRowOffset, matchColumnIndex) = matchedViewData;
        var absoluteDataPosition = Utils.CreatePosition(
            matchedRowOffset + matchedRowIndex,
            matchedColumnOffset + matchColumnIndex
        );

        var currentViewOffset = Utils.CreatePosition(
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
        var dataPositionRelativeToViewport = Utils.CreatePosition(viewportRowIndex, viewportColumnIndex);
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

    private static bool BackingResolver(object obj) => !((DataView)obj).Data.IsNull;

    private VirtualGridViewItem<TDataType, TExtraArgument>.CellInfo ConstructInfo(
        TDataType data,
        int rowIndex,
        int columnIndex,
        int viewportMinRowIndex,
        int viewportMinColumnIndex,
        int dataSetMinRowIndex,
        int dataSetMinColumnIndex,
        int viewportMaxRowIndex,
        int viewportMaxColumnIndex,
        int dataSetMaxRowIndex,
        int dataSetMaxColumnIndex
    )
    {
        var definedViewEdgeType = EdgeType.None;

        if (rowIndex == 0) definedViewEdgeType |= EdgeType.Up;
        if (columnIndex == 0) definedViewEdgeType |= EdgeType.Left;
        if (rowIndex == ViewRows - 1) definedViewEdgeType |= EdgeType.Down;
        if (columnIndex == ViewColumns - 1) definedViewEdgeType |= EdgeType.Right;


        var viewEdgeType = EdgeType.None;

        if (rowIndex == viewportMinRowIndex) viewEdgeType |= EdgeType.Up;
        if (columnIndex == viewportMinColumnIndex) viewEdgeType |= EdgeType.Left;
        if (rowIndex == viewportMaxRowIndex) viewEdgeType |= EdgeType.Down;
        if (columnIndex == viewportMaxColumnIndex) viewEdgeType |= EdgeType.Right;

        var absRowIndex = rowIndex + ViewRowIndex;
        var absColumnIndex = columnIndex + ViewColumnIndex;

        var dataSetEdgeType = EdgeType.None;
        if (absRowIndex == dataSetMinRowIndex) dataSetEdgeType |= EdgeType.Up;
        if (absColumnIndex == dataSetMinColumnIndex) dataSetEdgeType |= EdgeType.Left;
        if (absRowIndex == dataSetMaxRowIndex) dataSetEdgeType |= EdgeType.Down;
        if (absColumnIndex == dataSetMaxColumnIndex) dataSetEdgeType |= EdgeType.Right;

        var info = new VirtualGridViewItem<TDataType, TExtraArgument>.CellInfo(
            this,
            rowIndex,
            columnIndex,
            definedViewEdgeType,
            viewEdgeType,
            dataSetEdgeType,
            data
        );
        return info;
    }

    private void ProcessScrollWheelAndDragInput(InputEvent inputEvent)
    {
        using (inputEvent)
        {
            if (this != Utils.CurrentActiveGridView) return;

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

            var currentFocusPosition = Utils.CreatePosition(
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
                _currentView[targetFocusPosition.Y, targetFocusPosition.X]
                    .AssignedButton?.GrabFocus();

            var eventName = simulatedMoveDirection switch
            {
                MoveDirection.Up => Utils._uiUp,
                MoveDirection.Down => Utils._uiDown,
                MoveDirection.Left => Utils._uiLeft,
                MoveDirection.Right => Utils._uiRight,
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
        int viewportMinRowIndex,
        int viewportMinColumnIndex,
        int dataSetMinRowIndex,
        int dataSetMinColumnIndex,
        int viewportMaxRowIndex,
        int viewportMaxColumnIndex,
        int dataSetMaxRowIndex,
        int dataSetMaxColumnIndex
    )
    {
        if (!_buttonPool.TryPop(out var instance)) instance = _itemPrefab.Instantiate<TButtonType>();
        else instance.Show();

        _itemContainer.AddChild(instance);

        instance.FocusMode = Control.FocusModeEnum.All;
        instance.MouseFilter = Control.MouseFilterEnum.Pass;
        instance.DrawGridItem(
            ConstructInfo(
                data,
                rowIndex,
                columnIndex,
                viewportMinRowIndex,
                viewportMinColumnIndex,
                dataSetMinRowIndex,
                dataSetMinColumnIndex,
                viewportMaxRowIndex,
                viewportMaxColumnIndex,
                dataSetMaxRowIndex,
                dataSetMaxColumnIndex
            )
        );

        return instance;
    }

    private void CollectButtonInstance(Control button)
    {
        ElementTweener.KillTween(button);
        ElementFader.KillTween(button);
        ElementFader.Reinitialize(button);
        var typedButton = (TButtonType)button;
        typedButton.CallDisappear();
        _movingOutControls.Remove(typedButton);
        _buttonPool.Push(typedButton);
        _itemContainer.RemoveChild(button);
    }

    private void Redraw(
        out int viewportMinRowIndex,
        out int viewportMinColumnIndex,
        out int dataSetMinRowIndex,
        out int dataSetMinColumnIndex,
        out int viewportMaxRowIndex,
        out int viewportMaxColumnIndex,
        out int dataSetMaxRowIndex,
        out int dataSetMaxColumnIndex
    )
    {
        _dataInspector.GetDataSetCurrentMetrics(out var dataSetRows, out var dataSetColumns);

        viewportMinRowIndex = Mathf.Max(ViewRows - 1, 0);
        viewportMinColumnIndex = Mathf.Max(ViewColumns - 1, 0);
        dataSetMinRowIndex = 0;
        dataSetMinColumnIndex = 0;

        viewportMaxRowIndex = 0;
        viewportMaxColumnIndex = 0;
        dataSetMaxRowIndex = dataSetRows - 1;
        dataSetMaxColumnIndex = dataSetColumns - 1;

        for (var rowIndex = 0; rowIndex < ViewRows; rowIndex++)
        {
            var span = _dataInspector.InspectViewColumn(rowIndex, ViewColumnIndex, ViewRowIndex);
            for (var columnIndex = 0; columnIndex < ViewColumns; columnIndex++)
            {
                var dataSetValue = span[columnIndex];
                var oldViewItem = _currentView[rowIndex, columnIndex];
                var newViewItem = _nextView[rowIndex, columnIndex];

                // Current view is null
                if (oldViewItem.AssignedButton is null)
                {
                    // Set or not set to new value
                    newViewItem.Data = dataSetValue;
                }
                // Current view is not null
                else
                {
                    // Set to new value
                    newViewItem.Data = dataSetValue;
                    // If new value is equal to old, set the button as well
                    if (dataSetValue.IsEqual(oldViewItem.Data, _equalityComparer)) newViewItem.AssignedButton = oldViewItem.AssignedButton;
                }

                if (dataSetValue.IsNull) continue;

                viewportMinRowIndex = Math.Min(viewportMinRowIndex, rowIndex);
                viewportMinColumnIndex = Math.Min(viewportMinColumnIndex, columnIndex);
                viewportMaxRowIndex = Math.Max(viewportMaxRowIndex, rowIndex);
                viewportMaxColumnIndex = Math.Max(viewportMaxColumnIndex, columnIndex);
            }
        }

        for (var rowIndex = 0; rowIndex < ViewRows; rowIndex++)
        for (var columnIndex = 0; columnIndex < ViewColumns; columnIndex++)
        {
            var currentViewItem = _currentView[rowIndex, columnIndex];
            var nextViewItem = _nextView[rowIndex, columnIndex];

            // Initialize new item or empty
            if (nextViewItem.AssignedButton == null)
            {
                // Collect current button
                if (currentViewItem.AssignedButton != null)
                {
                    var assignedButton = currentViewItem.AssignedButton;
                    ElementTweener.KillTween(assignedButton);
                    _movingOutControls.Add(assignedButton);
                    ElementFader.Disappear(assignedButton, _collectInvincibleControlHandler);
                    nextViewItem.AssignedButton = null;
                }

                if (nextViewItem.Data.TryUnwrap(out var newData))
                {
                    var buttonInstance = GetAndInitializeButtonInstance(
                        newData,
                        rowIndex,
                        columnIndex,
                        viewportMinRowIndex,
                        viewportMinColumnIndex,
                        dataSetMinRowIndex,
                        dataSetMinColumnIndex,
                        viewportMaxRowIndex,
                        viewportMaxColumnIndex,
                        dataSetMaxRowIndex,
                        dataSetMaxColumnIndex
                    );

                    buttonInstance.Position = _layoutGrid.GetGridElementPosition(Utils.CreatePosition(rowIndex, columnIndex));
                    buttonInstance.CallAppear();
                    ElementTweener.KillTween(buttonInstance);
                    ElementFader.Appear(buttonInstance);
                    nextViewItem.AssignedButton = buttonInstance;
                }
            }
            // No change
            else
            {
                nextViewItem.AssignedButton!.Info = ConstructInfo(
                    nextViewItem.Data.Unwrap(),
                    rowIndex,
                    columnIndex,
                    viewportMinRowIndex,
                    viewportMinColumnIndex,
                    dataSetMinRowIndex,
                    dataSetMinColumnIndex,
                    viewportMaxRowIndex,
                    viewportMaxColumnIndex,
                    dataSetMaxRowIndex,
                    dataSetMaxColumnIndex
                );
            }
        }

        (_currentView, _nextView) = (_nextView, _currentView);

        foreach (var dataView in _nextView)
        {
            dataView.Data = NullableData.Null<TDataType>();
            dataView.AssignedButton = null;
        }
    }

    private void UpdateScrollBar(int dataRows, int dataColumns, bool noAnimation = false)
    {
        float rowProgress;
        float rowPage;
        float columnProgress;
        float columnPage;

        bool canAutoHideRowScrollBar;
        bool canAutoHideColumnScrollBar;

        if (ViewRows >= dataRows)
        {
            rowProgress = 1f;
            rowPage = 1f;
            canAutoHideRowScrollBar = true;
        }
        else
        {
            rowProgress = ViewRowIndex / (float)(dataRows - ViewRows);
            rowProgress = Math.Max(rowProgress, 0f);
            rowPage = ViewRows / (float)dataRows;
            canAutoHideRowScrollBar = false;
        }

        if (ViewColumns >= dataColumns)
        {
            columnProgress = 1f;
            columnPage = 1f;
            canAutoHideColumnScrollBar = true;
        }
        else
        {
            columnProgress = ViewColumnIndex / (float)(dataColumns - ViewColumns);
            columnProgress = Math.Max(columnProgress, 0f);
            columnPage = ViewColumns / (float)dataColumns;
            canAutoHideColumnScrollBar = false;
        }

        UpdateScroller(
            _verticalScrollBar,
            VScrollBarTweener,
            VScrollBarFader,
            rowProgress,
            rowPage,
            noAnimation,
            canAutoHideRowScrollBar,
            AutoHideVScrollBar,
            ref _isVerticalScrollBarVisible
        );

        UpdateScroller(
            _horizontalScrollBar,
            HScrollBarTweener,
            HScrollBarFader,
            columnProgress,
            columnPage,
            noAnimation,
            canAutoHideColumnScrollBar,
            AutoHideHScrollBar,
            ref _isHorizontalScrollBarVisible
        );

        return;

        static void UpdateScroller(
            ScrollBar? scrollBar,
            IScrollBarTweener tweener,
            IElementFader fader,
            float progress,
            float page,
            bool noAnimation,
            bool canAutoHide,
            bool autoHide,
            ref bool isCurrentVisible
        )
        {
            if (scrollBar is null) return;
            tweener.KillTween(scrollBar);

            if (noAnimation)
            {
                scrollBar.Value = progress;
                scrollBar.Page = page;

                if (!autoHide) return;

                if (isCurrentVisible == canAutoHide) return;

                scrollBar.Visible = canAutoHide;
                isCurrentVisible = canAutoHide;
            }
            else
            {
                if (autoHide)
                {
                    if (canAutoHide)
                    {
                        if (isCurrentVisible)
                        {
                            scrollBar.Show();
                            fader.KillTween(scrollBar);
                            fader.Disappear(scrollBar, control => control.Hide());
                            isCurrentVisible = false;
                        }
                    }
                    else
                    {
                        if (!isCurrentVisible)
                        {
                            scrollBar.Show();
                            fader.KillTween(scrollBar);
                            fader.Appear(scrollBar);
                            isCurrentVisible = true;
                        }
                    }
                }
                else
                {
                    if (!isCurrentVisible)
                    {
                        scrollBar.Show();
                        fader.KillTween(scrollBar);
                        fader.Appear(scrollBar);
                        isCurrentVisible = true;
                    }
                }

                tweener.UpdateValue(scrollBar, progress, page);
            }
        }
    }

    private void ApplyMovementOffset(Vector2I offset)
    {
        if (offset == Vector2I.Zero) return;

        var dataSetMaxRowIndex = 0;
        var dataSetMaxColumnIndex = 0;
        while (Utils.TryGetMoveDirection(ref offset, out var moveDirection))
        {
            if (moveDirection == Vector2I.Left) Move(MoveDirection.Right, out dataSetMaxRowIndex, out dataSetMaxColumnIndex);
            if (moveDirection == Vector2I.Right) Move(MoveDirection.Left, out dataSetMaxRowIndex, out dataSetMaxColumnIndex);
            if (moveDirection == Vector2I.Up) Move(MoveDirection.Down, out dataSetMaxRowIndex, out dataSetMaxColumnIndex);
            if (moveDirection == Vector2I.Down) Move(MoveDirection.Up, out dataSetMaxRowIndex, out dataSetMaxColumnIndex);
        }

        UpdateScrollBar(dataSetMaxRowIndex + 1, dataSetMaxColumnIndex + 1);
    }

    private void Move(MoveDirection moveDirection, out int dataSetMaxRowIndex, out int dataSetMaxColumnIndex)
    {
        Redraw(
            out var viewportMinRowIndex,
            out var viewportMinColumnIndex,
            out var dataSetMinRowIndex,
            out var dataSetMinColumnIndex,
            out var viewportMaxRowIndex,
            out var viewportMaxColumnIndex,
            out dataSetMaxRowIndex,
            out dataSetMaxColumnIndex
        );
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

        var resolvedViewportMinRowIndex = Mathf.Max(ViewRows - 1, 0);
        var resolvedViewportMinColumnIndex = Mathf.Max(ViewColumns - 1, 0);
        var resolvedViewportMaxRowIndex = 0;
        var resolvedViewportMaxColumnIndex = 0;

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
                    var info = ConstructInfo(
                        currentViewItem.Data.Unwrap(),
                        targetGridPosition.X,
                        targetGridPosition.Y,
                        viewportMinRowIndex,
                        viewportMinColumnIndex,
                        dataSetMinRowIndex,
                        dataSetMinColumnIndex,
                        viewportMaxRowIndex,
                        viewportMaxColumnIndex,
                        dataSetMaxRowIndex,
                        dataSetMaxColumnIndex
                    );
                    currentButton.CallMove(info);
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
                var targetRowIndex = targetGridPosition.X;
                var targetColumnIndex = targetGridPosition.Y;
                var nextViewItem = _nextView[targetRowIndex, targetColumnIndex];
                nextViewItem.AssignedButton = currentButton;
                nextViewItem.Data = currentViewItem.Data;

                resolvedViewportMinRowIndex = Math.Min(resolvedViewportMinRowIndex, targetRowIndex);
                resolvedViewportMinColumnIndex = Math.Min(resolvedViewportMinColumnIndex, targetColumnIndex);
                resolvedViewportMaxRowIndex = Math.Max(resolvedViewportMaxRowIndex, targetRowIndex);
                resolvedViewportMaxColumnIndex = Math.Max(resolvedViewportMaxColumnIndex, targetColumnIndex);
            }
        }

        // Resolve newly occured item and update boundaries
        for (var rowIndex = 0; rowIndex < ViewRows; rowIndex++)
        {
            var span = _dataInspector.InspectViewColumn(rowIndex, ViewColumnIndex, ViewRowIndex);
            for (var columnIndex = 0; columnIndex < ViewColumns; columnIndex++)
            {
                var nextViewItem = _nextView[rowIndex, columnIndex];

                if (nextViewItem.AssignedButton is not null) continue;

                var currentValue = span[columnIndex];

                if (!currentValue.TryUnwrap(out var data)) continue;

                resolvedViewportMinRowIndex = Math.Min(resolvedViewportMinRowIndex, rowIndex);
                resolvedViewportMinColumnIndex = Math.Min(resolvedViewportMinColumnIndex, columnIndex);
                resolvedViewportMaxRowIndex = Math.Max(resolvedViewportMaxRowIndex, rowIndex);
                resolvedViewportMaxColumnIndex = Math.Max(resolvedViewportMaxColumnIndex, columnIndex);

                nextViewItem.Data = currentValue;

                var movementType = GetEdgeType(rowIndex, columnIndex);

                var newButton = GetAndInitializeButtonInstance(
                    data,
                    rowIndex,
                    columnIndex,
                    viewportMinRowIndex,
                    viewportMinColumnIndex,
                    dataSetMinRowIndex,
                    dataSetMinColumnIndex,
                    viewportMaxRowIndex,
                    viewportMaxColumnIndex,
                    dataSetMaxRowIndex,
                    dataSetMaxColumnIndex
                );
                nextViewItem.AssignedButton = newButton;
                var info = newButton.Info!.Value;
                newButton.Info = info;

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

        for (var rowIndex = 0; rowIndex < ViewRows; rowIndex++)
        for (var columnIndex = 0; columnIndex < ViewColumns; columnIndex++)
        {
            var nextViewItem = _nextView[rowIndex, columnIndex];
            if (nextViewItem.Data.IsNull) continue;
            var info = nextViewItem.AssignedButton!.Info!.Value;

            nextViewItem.AssignedButton.Info = ConstructInfo(
                info.Data!,
                info.RowIndex,
                info.ColumnIndex,
                resolvedViewportMinRowIndex,
                resolvedViewportMinColumnIndex,
                dataSetMinRowIndex,
                dataSetMinColumnIndex,
                resolvedViewportMaxRowIndex,
                resolvedViewportMaxColumnIndex,
                dataSetMaxRowIndex,
                dataSetMaxColumnIndex
            );
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

    private class DataView
    {
        public TButtonType? AssignedButton;
        public NullableData<TDataType> Data;
        public override string ToString() => $"Button: {AssignedButton?.Name ?? "Null"}, Data: {Data}";
    }
}