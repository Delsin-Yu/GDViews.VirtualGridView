using System;
using System.Collections.Generic;
using Godot;
using GodotViews.VirtualGrid.Builder;
using GodotViews.VirtualGrid.FocusFinding;
using GodotViews.VirtualGrid.Layout;
using GodotViews.VirtualGrid.Positioner;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid;

class VirtualGridViewImpl<TDataType, TButtonType, TExtraArgument> :
    IVirtualGridViewParent<TDataType, TExtraArgument>,
    IVirtualGridView<TDataType> where TButtonType : VirtualGridViewItemArg<TDataType, TExtraArgument>
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

    private NullableData<TDataType> _currentSelectedData;
    private int _currentSelectedViewXIndex;
    private int _currentSelectedViewYIndex;

    private DataView[,] _currentView;

    private bool _isDragging;

    private bool _isHorizontalScrollBarVisible = true;
    private bool _isVerticalScrollBarVisible = true;
    private DataView[,] _nextView;

    private Vector2 _startDragPosition;

    internal VirtualGridViewImpl(
        int viewportXCount,
        int viewportYCount,
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
        ViewXCount = viewportXCount;
        ViewYCount = viewportYCount;

        ElementPositioner = elementPositioner;
        ElementTweener = elementTweener;
        ElementFader = elementFader;

        HScrollBarTweener = horizontalScrollBarTweener;
        HScrollBarFader = horizontalScrollBarFader;
        VScrollBarTweener = verticalScrollBarTweener;
        VScrollBarFader = verticalScrollBarFader;

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
        _currentView = new DataView[ViewXCount, ViewYCount];
        _nextView = new DataView[ViewXCount, ViewYCount];
        _viewportSize = new(ViewXCount, ViewYCount);

        for (var yIndex = 0; yIndex < ViewYCount; yIndex++)
        for (var xIndex = 0; xIndex < ViewXCount; xIndex++)
        {
            _currentView[xIndex, yIndex] = new() { Data = NullableData.Null<TDataType>() };
            _nextView[xIndex, yIndex] = new() { Data = NullableData.Null<TDataType>() };
        }

        ViewXIndex = 0;
        ViewYIndex = 0;

        var preloadAmount = Math.Max(ViewXCount, ViewYCount) * 2;
        _buttonPool = new(preloadAmount);

        for (var i = 0; i < preloadAmount; i++)
        {
            var instance = itemPrefab.Instantiate<TButtonType>();
            instance.CallCreate();
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

    public bool EnableDragging { get; set; }

    public int ViewXIndex { get; private set; }
    public int ViewYIndex { get; private set; }

    public int ViewXCount { get; }
    public int ViewYCount { get; }

    public IElementPositioner ElementPositioner { get; set; }
    public IElementTweener ElementTweener { get; set; }
    public IElementFader ElementFader { get; set; }
    public IScrollBarTweener HScrollBarTweener { get; set; }
    public IScrollBarTweener VScrollBarTweener { get; set; }
    public IElementFader HScrollBarFader { get; set; }
    public IElementFader VScrollBarFader { get; set; }
    public bool AutoHideHScrollBar { get; set; }
    public bool AutoHideVScrollBar { get; set; }

    public bool GrabFocus() =>
        _currentSelectedData.TryUnwrap(out var currentSelectedData)
        && TryGetDataPositionRelativeToViewport(
            _equalityComparerEquals,
            out var relativeXIndex,
            out var relativeYIndex,
            currentSelectedData
        )
        && TryGrabFocusCore(relativeXIndex, relativeYIndex)
        || TryGrabFocusCore(_currentSelectedViewXIndex, _currentSelectedViewYIndex)
        || ((IVirtualGridView<TDataType>)this).GrabFocus(FocusPresets.TopLeftView)
        || ((IVirtualGridView<TDataType>)this).GrabFocus(FocusPresets.TopLeftData);


    public bool GrabFocus<TArgument>(
        IViewFocusFinder<TArgument> focusFinder,
        IViewStartHandler<TArgument> startHandler,
        TArgument argument,
        SearchDirection searchDirection
    )
    {
        // ReSharper disable once CoVariantArrayConversion
        var wrapper = new ReadOnlyViewArray(_currentView, ViewXCount, ViewYCount, BackingResolver);
        var span = searchDirection.GetSpan();
        return focusFinder.TryResolveFocus(
                   in wrapper,
                   in span,
                   startHandler,
                   in argument,
                   out var viewXIndex,
                   out var viewYIndex
               )
               && TryGrabFocusCore(viewXIndex, viewYIndex);
    }

    public bool GrabFocus(IEqualityDataFocusFinder focusFinder, in TDataType matchingArgument)
    {
        var wrapper = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewXCount, ViewYCount);
        return focusFinder.TryResolveFocus(
                   in matchingArgument,
                   in wrapper,
                   out var dataSetXIndex,
                   out var dataSetYIndex
               )
               && TryGrabFocusCore(dataSetXIndex - ViewXIndex, dataSetYIndex - ViewYIndex);
    }

    public bool GrabFocus(IPredicateDataFocusFinder focusFinder, Predicate<TDataType> predicate)
    {
        var wrapper = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewXCount, ViewYCount);
        return focusFinder.TryResolveFocus(
                   in predicate,
                   in wrapper,
                   out var dataSetXIndex,
                   out var dataSetYIndex
               )
               && TryGrabFocusCore(dataSetXIndex - ViewXIndex, dataSetYIndex - ViewYIndex);
    }

    public bool GrabFocus<TMatchingExtraArgument>(
        IPredicateDataFocusFinder focusFinder,
        Func<TDataType, TMatchingExtraArgument, bool> predicate,
        TMatchingExtraArgument extraArgument
    )
    {
        var wrapper = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewXCount, ViewYCount);
        return focusFinder.TryResolveFocus(
                   in predicate,
                   in wrapper,
                   extraArgument,
                   out var dataSetXIndex,
                   out var dataSetYIndex
               )
               && TryGrabFocusCore(dataSetXIndex - ViewXIndex, dataSetYIndex - ViewYIndex);
    }

    public bool GrabFocus<TArgument>(
        IDataFocusFinder<TArgument> focusFinder,
        IDataStartHandler<TArgument> startHandler,
        TArgument argument,
        SearchDirection searchDirection
    )
    {
        var wrapper = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewXCount, ViewYCount);
        var span = searchDirection.GetSpan();
        return focusFinder.TryResolveFocus(
                   in wrapper,
                   in span,
                   startHandler,
                   in argument,
                   out var dataSetXIndex,
                   out var dataSetYIndex
               )
               && TryGrabFocusCore(dataSetXIndex - ViewXIndex, dataSetYIndex - ViewYIndex);
    }

    public void Redraw()
    {
        Redraw(out _, out _, out _, out _, out _, out _, out var dataSetMaxXIndex, out var dataSetMaxYIndex);
        UpdateScrollBar(dataSetMaxXIndex + 1, dataSetMaxYIndex + 1);
    }

    public TExtraArgument? ExtraArgument { get; }

    public void FocusTo(VirtualGridViewItemArg<TDataType, TExtraArgument>.CellInfo info)
    {
        _currentSelectedViewXIndex = info.XIndex;
        _currentSelectedViewYIndex = info.YIndex;
        _currentSelectedData = NullableData.Create<TDataType>(info.Data!);
        FocusToTarget(
            _currentSelectedViewXIndex,
            _currentSelectedViewYIndex,
            out _,
            out _
        );
    }

    /// <summary>
    /// This method tries to find the best next candidate
    /// in the given <paramref name="moveDirection"/>
    /// of the provided <paramref name="yIndex"/> and <paramref name="xIndex"/>.
    /// </summary>
    public void MoveAndGrabFocus(MoveDirection moveDirection, int xIndex, int yIndex)
    {
        ReadOnlySpan<Vector2I> searchDirection = moveDirection switch
        {
            MoveDirection.Up => [SearchDirections.SearchUp, SearchDirections.SearchLeft, SearchDirections.SearchRight],
            MoveDirection.Down => [SearchDirections.SearchDown, SearchDirections.SearchRight, SearchDirections.SearchLeft],
            MoveDirection.Left => [SearchDirections.SearchLeft, SearchDirections.SearchUp, SearchDirections.SearchDown],
            MoveDirection.Right => [SearchDirections.SearchRight, SearchDirections.SearchUp, SearchDirections.SearchDown],
            _ => throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null),
        };

        var absoluteStart = new Vector2I(ViewXIndex + xIndex, ViewYIndex + yIndex) + searchDirection[0];
        var readOnlyDataArray = new ReadOnlyDataArray<TDataType>(_dataInspector, ViewXCount, ViewYCount);

        // TODO: Relying on BFS for searching for matching is stupid,
        // this leads to serious performance degradation when
        // the BFS walks through a very long distance.
        // We should either:
        //     Develop a new matching algorithm.
        //     Optimize the hell out of the BFSCore, as accessing cell data involves a lot of calculations.
        if (!FocusFinders.BFSSearch.BFSCore(
                absoluteStart,
                readOnlyDataArray,
                searchDirection,
                out var targetAbsoluteXIndex,
                out var targetAbsoluteYIndex
            )) return;

        if (!readOnlyDataArray.TryGetData(targetAbsoluteXIndex, targetAbsoluteYIndex, out var data)) return;
        GrabFocus(FocusFinders.Value, data);
    }

    private bool TryGetDataPositionRelativeToViewport(Func<TDataType, TDataType, bool> comparer, out int xIndex, out int yIndex, TDataType data)
    {
        xIndex = -1;
        yIndex = -1;
        if (!Utils.SearchForData(_dataInspector, ViewXCount, ViewYCount, out var matchedViewData, comparer, data)) return false;
        var (matchedYIndex, matchedXOffset, matchedYOffset, matchXIndex) = matchedViewData;
        int yIndex1 = matchedYOffset + matchedYIndex;
        var absoluteDataPosition = new Vector2I(matchedXOffset + matchXIndex, yIndex1);

        var currentViewOffset = new Vector2I(ViewXIndex, ViewYIndex);

        var relativeDataPosition = absoluteDataPosition - currentViewOffset;

        yIndex = relativeDataPosition.Y;
        xIndex = relativeDataPosition.X;

        return true;
    }

    private bool TryGrabFocusCore(int relativeXIndex, int relativeYIndex)
    {
        FocusToTarget(
            relativeXIndex,
            relativeYIndex,
            out var targetXIndex,
            out var targetYIndex
        );

        var selectedView = _currentView[targetYIndex, targetXIndex].AssignedButton;

        if (selectedView is null) return false;

        if (selectedView.GetFocusModeWithOverride() is Control.FocusModeEnum.None) return false;

        selectedView.GrabFocus();

        return true;
    }

    private void FocusToTarget(int viewportXIndex, int viewportYIndex, out int targetYIndex, out int targetXIndex)
    {
        var dataPositionRelativeToViewport = new Vector2I(viewportXIndex, viewportYIndex);
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
        (targetXIndex, targetYIndex) = targetDataPosition;
    }

    private static bool BackingResolver(object obj) => !((DataView)obj).Data.IsNull;

    private VirtualGridViewItemArg<TDataType, TExtraArgument>.CellInfo ConstructInfo(
        TDataType data,
        int xIndex,
        int yIndex,
        int viewportMinXIndex,
        int viewportMinYIndex,
        int dataSetMinXIndex,
        int dataSetMinYIndex,
        int viewportMaxXIndex,
        int viewportMaxYIndex,
        int dataSetMaxXIndex,
        int dataSetMaxYIndex
    )
    {
        var definedViewEdgeType = EdgeType.None;

        if (yIndex == 0) definedViewEdgeType |= EdgeType.Up;
        if (xIndex == 0) definedViewEdgeType |= EdgeType.Left;
        if (yIndex == ViewYCount - 1) definedViewEdgeType |= EdgeType.Down;
        if (xIndex == ViewXCount - 1) definedViewEdgeType |= EdgeType.Right;


        var viewEdgeType = EdgeType.None;

        if (yIndex == viewportMinYIndex) viewEdgeType |= EdgeType.Up;
        if (xIndex == viewportMinXIndex) viewEdgeType |= EdgeType.Left;
        if (yIndex == viewportMaxYIndex) viewEdgeType |= EdgeType.Down;
        if (xIndex == viewportMaxXIndex) viewEdgeType |= EdgeType.Right;

        var absYIndex = yIndex + ViewYIndex;
        var absXIndex = xIndex + ViewXIndex;

        var dataSetEdgeType = EdgeType.None;
        if (absYIndex == dataSetMinYIndex) dataSetEdgeType |= EdgeType.Up;
        if (absXIndex == dataSetMinXIndex) dataSetEdgeType |= EdgeType.Left;
        if (absYIndex == dataSetMaxYIndex) dataSetEdgeType |= EdgeType.Down;
        if (absXIndex == dataSetMaxXIndex) dataSetEdgeType |= EdgeType.Right;

        var info = new VirtualGridViewItemArg<TDataType, TExtraArgument>.CellInfo(
            this,
            xIndex,
            yIndex,
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
                if (!_isDragging && EnableDragging)
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

                    if (mouseButton.IsReleased()) return;

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

                    if (!_isDragging) return;

                    if (!TryGetMoveDirection(
                            ref _startDragPosition,
                            mouseMotion.GlobalPosition,
                            _cellItemSize,
                            out simulatedMoveDirection
                        )) return;

                    break;
                default: return;
            }

            var currentFocusPosition = new Vector2I(_currentSelectedViewXIndex, _currentSelectedViewYIndex);

            ElementPositioner.GetDragViewPosition(
                _viewportSize,
                simulatedMoveDirection,
                currentFocusPosition,
                out var targetFocusPosition
            );

            if (currentFocusPosition != targetFocusPosition)
                _currentView[targetFocusPosition.X, targetFocusPosition.Y]
                    .AssignedButton?.GrabFocus();

            var eventName = simulatedMoveDirection switch
            {
                MoveDirection.Up => Utils.UIUp,
                MoveDirection.Down => Utils.UIDown,
                MoveDirection.Left => Utils.UILeft,
                MoveDirection.Right => Utils.UIRight,
                _ => throw new InvalidOperationException(),
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
        int xIndex,
        int yIndex,
        int viewportMinXIndex,
        int viewportMinYIndex,
        int dataSetMinXIndex,
        int dataSetMinYIndex,
        int viewportMaxXIndex,
        int viewportMaxYIndex,
        int dataSetMaxXIndex,
        int dataSetMaxYIndex
    )
    {
        if (!_buttonPool.TryPop(out var instance))
        {
            instance = _itemPrefab.Instantiate<TButtonType>();
            instance.CallCreate();
        }
        else
            instance.Show();

        _itemContainer.AddChild(instance);

        instance.FocusMode = Control.FocusModeEnum.All;
        instance.MouseFilter = Control.MouseFilterEnum.Pass;
        instance.DrawGridItem(
            ConstructInfo(
                data,
                xIndex,
                yIndex,
                viewportMinXIndex,
                viewportMinYIndex,
                dataSetMinXIndex,
                dataSetMinYIndex,
                viewportMaxXIndex,
                viewportMaxYIndex,
                dataSetMaxXIndex,
                dataSetMaxYIndex
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
        out int viewportMinXIndex,
        out int viewportMinYIndex,
        out int dataSetMinXIndex,
        out int dataSetMinYIndex,
        out int viewportMaxXIndex,
        out int viewportMaxYIndex,
        out int dataSetMaxXIndex,
        out int dataSetMaxYIndex
    )
    {
        _dataInspector.GetDataSetCurrentMetrics(out var dataSetXCount, out var dataSetYCount);

        viewportMinYIndex = Mathf.Max(ViewYCount - 1, 0);
        viewportMinXIndex = Mathf.Max(ViewXCount - 1, 0);
        dataSetMinYIndex = 0;
        dataSetMinXIndex = 0;

        viewportMaxYIndex = 0;
        viewportMaxXIndex = 0;
        dataSetMaxYIndex = dataSetYCount - 1;
        dataSetMaxXIndex = dataSetXCount - 1;

        for (var yIndex = 0; yIndex < ViewYCount; yIndex++)
        {
            var span = _dataInspector.InspectViewX(yIndex, ViewXIndex, ViewYIndex);

            for (var xIndex = 0; xIndex < ViewXCount; xIndex++)
            {
                var dataSetValue = span[xIndex];
                var oldViewItem = _currentView[xIndex, yIndex];
                var newViewItem = _nextView[xIndex, yIndex];

                // Current view is null
                if (oldViewItem.AssignedButton is null)
                    // Set or not set to new value
                    newViewItem.Data = dataSetValue;
                // Current view is not null
                else
                {
                    // Set to new value
                    newViewItem.Data = dataSetValue;
                    // If new value is equal to old, set the button as well
                    if (dataSetValue.IsEqual(oldViewItem.Data, _equalityComparer)) newViewItem.AssignedButton = oldViewItem.AssignedButton;
                }

                if (dataSetValue.IsNull) continue;

                viewportMinXIndex = Math.Min(viewportMinXIndex, xIndex);
                viewportMinYIndex = Math.Min(viewportMinYIndex, yIndex);
                viewportMaxXIndex = Math.Max(viewportMaxXIndex, xIndex);
                viewportMaxYIndex = Math.Max(viewportMaxYIndex, yIndex);
            }
        }

        for (var yIndex = 0; yIndex < ViewYCount; yIndex++)
        for (var xIndex = 0; xIndex < ViewXCount; xIndex++)
        {
            var currentViewItem = _currentView[xIndex, yIndex];
            var nextViewItem = _nextView[xIndex, yIndex];

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
                        xIndex,
                        yIndex,
                        viewportMinXIndex,
                        viewportMinYIndex,
                        dataSetMinXIndex,
                        dataSetMinYIndex,
                        viewportMaxXIndex,
                        viewportMaxYIndex,
                        dataSetMaxXIndex,
                        dataSetMaxYIndex
                    );

                    buttonInstance.Position = _layoutGrid.GetGridElementPosition(new(xIndex, yIndex));
                    buttonInstance.CallAppear();
                    ElementTweener.KillTween(buttonInstance);
                    ElementFader.Appear(buttonInstance);
                    nextViewItem.AssignedButton = buttonInstance;
                }
            }
            // No change
            else
                nextViewItem.AssignedButton!.Info = ConstructInfo(
                    nextViewItem.Data.Unwrap(),
                    xIndex,
                    yIndex,
                    viewportMinXIndex,
                    viewportMinYIndex,
                    dataSetMinXIndex,
                    dataSetMinYIndex,
                    viewportMaxXIndex,
                    viewportMaxYIndex,
                    dataSetMaxXIndex,
                    dataSetMaxYIndex
                );
        }

        (_currentView, _nextView) = (_nextView, _currentView);

        foreach (var dataView in _nextView)
        {
            dataView.Data = NullableData.Null<TDataType>();
            dataView.AssignedButton = null;
        }
    }

    private void UpdateScrollBar(int dataXCount, int dataYCount, bool noAnimation = false)
    {
        float xProgress;
        float xPage;
        float yProgress;
        float yPage;

        bool canAutoHideXScrollBar;
        bool canAutoHideYScrollBar;

        if (ViewYCount >= dataYCount)
        {
            yProgress = 1f;
            yPage = 1f;
            canAutoHideYScrollBar = true;
        }
        else
        {
            var fixedDataYCount = (float)dataYCount;
            yProgress = ViewYIndex / fixedDataYCount;
            yProgress = Math.Max(yProgress, 0f);
            yPage = ViewYCount / fixedDataYCount;
            canAutoHideYScrollBar = false;
        }

        if (ViewXCount >= dataXCount)
        {
            xProgress = 1f;
            xPage = 1f;
            canAutoHideXScrollBar = true;
        }
        else
        {
            var fixedDataXCount = (float)dataXCount;
            xProgress = ViewXIndex / fixedDataXCount;
            xProgress = Math.Max(xProgress, 0f);
            xPage = ViewXCount / fixedDataXCount;
            canAutoHideXScrollBar = false;
        }

        UpdateScroller(
            _verticalScrollBar,
            VScrollBarTweener,
            VScrollBarFader,
            yProgress,
            yPage,
            noAnimation,
            canAutoHideYScrollBar,
            AutoHideVScrollBar,
            ref _isVerticalScrollBarVisible
        );

        UpdateScroller(
            _horizontalScrollBar,
            HScrollBarTweener,
            HScrollBarFader,
            xProgress,
            xPage,
            noAnimation,
            canAutoHideXScrollBar,
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

        var dataSetMaxYIndex = 0;
        var dataSetMaxXIndex = 0;

        while (Utils.TryGetMoveDirection(ref offset, out var moveDirection))
        {
            if (moveDirection == Vector2I.Left) Move(MoveDirection.Right, out dataSetMaxXIndex, out dataSetMaxYIndex);
            if (moveDirection == Vector2I.Right) Move(MoveDirection.Left, out dataSetMaxXIndex, out dataSetMaxYIndex);
            if (moveDirection == Vector2I.Up) Move(MoveDirection.Down, out dataSetMaxXIndex, out dataSetMaxYIndex);
            if (moveDirection == Vector2I.Down) Move(MoveDirection.Up, out dataSetMaxXIndex, out dataSetMaxYIndex);
        }

        UpdateScrollBar(dataSetMaxXIndex + 1, dataSetMaxYIndex + 1);
    }

    private void Move(MoveDirection moveDirection, out int dataSetMaxXIndex, out int dataSetMaxYIndex)
    {
        Redraw(
            out var viewportMinXIndex,
            out var viewportMinYIndex,
            out var dataSetMinXIndex,
            out var dataSetMinYIndex,
            out var viewportMaxXIndex,
            out var viewportMaxYIndex,
            out dataSetMaxXIndex,
            out dataSetMaxYIndex
        );
        Vector2I moveDirectionVector;
        int isMovingOutLineIndex;
        int isMovingInLineIndex;
        int movingOutLineIndex;
        int movingInLineIndex;
        bool isY;

        switch (moveDirection)
        {
            case MoveDirection.Up:
                isY = true;
                isMovingOutLineIndex = ViewYCount - 1;
                isMovingInLineIndex = 0;
                movingOutLineIndex = ViewYCount;
                movingInLineIndex = -1;
                moveDirectionVector = new(0, -1);
                break;
            case MoveDirection.Down:
                isY = true;
                isMovingOutLineIndex = 0;
                isMovingInLineIndex = ViewYCount - 1;
                movingOutLineIndex = -1;
                movingInLineIndex = ViewYCount;
                moveDirectionVector = new(0, 1);
                break;
            case MoveDirection.Left:
                isY = false;
                isMovingOutLineIndex = ViewXCount - 1;
                isMovingInLineIndex = 0;
                movingOutLineIndex = ViewXCount;
                movingInLineIndex = -1;
                moveDirectionVector = new(-1, 0);
                break;
            case MoveDirection.Right:
                isY = false;
                isMovingOutLineIndex = 0;
                isMovingInLineIndex = ViewXCount - 1;
                movingOutLineIndex = -1;
                movingInLineIndex = ViewXCount;
                moveDirectionVector = new(1, 0);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
        }

        ViewXIndex += moveDirectionVector.X;
        ViewYIndex += moveDirectionVector.Y;

        var resolvedViewportMinXIndex = Mathf.Max(ViewXCount - 1, 0);
        var resolvedViewportMinYIndex = Mathf.Max(ViewYCount - 1, 0);
        var resolvedViewportMaxXIndex = 0;
        var resolvedViewportMaxYIndex = 0;

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
        for (var yIndex = 0; yIndex < ViewYCount; yIndex++)
        for (var xIndex = 0; xIndex < ViewXCount; xIndex++)
        {
            var currentViewItem = _currentView[xIndex, yIndex];
            var currentButton = currentViewItem.AssignedButton;

            if (currentButton is null) continue;

            var currentGridIndex = new Vector2I(xIndex, yIndex);
            var movementType = GetEdgeType(xIndex, yIndex);

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
                        viewportMinXIndex,
                        viewportMinYIndex,
                        dataSetMinXIndex,
                        dataSetMinYIndex,
                        viewportMaxXIndex,
                        viewportMaxYIndex,
                        dataSetMaxXIndex,
                        dataSetMaxYIndex
                    );
                    currentButton.CallMove(info);
                    ElementTweener.MoveTo(currentButton, _layoutGrid.GetGridElementPosition(targetGridPosition));
                    break;
                case EDGE_OUT:
                    targetGridPosition = isY ? new(xIndex, movingOutLineIndex) : new(movingOutLineIndex, yIndex);
                    currentButton.CallMoveOut();
                    currentButton.FocusMode = Control.FocusModeEnum.None;
                    currentButton.MouseFilter = Control.MouseFilterEnum.Ignore;
                    _movingOutControls.Add(currentButton);
                    ElementTweener.MoveOut(currentButton, _layoutGrid.GetGridElementPosition(targetGridPosition), _collectInvincibleControlHandler);
                    break;
                default:
                    throw new ArgumentException(nameof(movementType));
            }

            if (movementType is EDGE_NORMAL or EDGE_IN)
            {
                var targetYIndex = targetGridPosition.Y;
                var targetXIndex = targetGridPosition.X;
                var nextViewItem = _nextView[targetXIndex, targetYIndex];
                nextViewItem.AssignedButton = currentButton;
                nextViewItem.Data = currentViewItem.Data;

                resolvedViewportMinXIndex = Math.Min(resolvedViewportMinXIndex, targetXIndex);
                resolvedViewportMinYIndex = Math.Min(resolvedViewportMinYIndex, targetYIndex);
                resolvedViewportMaxXIndex = Math.Max(resolvedViewportMaxXIndex, targetXIndex);
                resolvedViewportMaxYIndex = Math.Max(resolvedViewportMaxYIndex, targetYIndex);
            }
        }

        // Resolve newly occured item and update boundaries
        for (var yIndex = 0; yIndex < ViewYCount; yIndex++)
        {
            var span = _dataInspector.InspectViewX(yIndex, ViewXIndex, ViewYIndex);

            for (var xIndex = 0; xIndex < ViewXCount; xIndex++)
            {
                var nextViewItem = _nextView[xIndex, yIndex];

                if (nextViewItem.AssignedButton is not null) continue;

                var currentValue = span[xIndex];

                if (!currentValue.TryUnwrap(out var data)) continue;

                resolvedViewportMinXIndex = Math.Min(resolvedViewportMinXIndex, xIndex);
                resolvedViewportMinYIndex = Math.Min(resolvedViewportMinYIndex, yIndex);
                resolvedViewportMaxXIndex = Math.Max(resolvedViewportMaxXIndex, xIndex);
                resolvedViewportMaxYIndex = Math.Max(resolvedViewportMaxYIndex, yIndex);

                nextViewItem.Data = currentValue;

                var movementType = GetEdgeType(xIndex, yIndex);

                var newButton = GetAndInitializeButtonInstance(
                    data,
                    xIndex,
                    yIndex,
                    viewportMinXIndex,
                    viewportMinYIndex,
                    dataSetMinXIndex,
                    dataSetMinYIndex,
                    viewportMaxXIndex,
                    viewportMaxYIndex,
                    dataSetMaxXIndex,
                    dataSetMaxYIndex
                );
                nextViewItem.AssignedButton = newButton;
                var info = newButton.Info!.Value;
                newButton.Info = info;

                switch (movementType)
                {
                    case EDGE_IN:
                        Vector2I emulatedStartPosition =
                            isY ? new(xIndex, movingInLineIndex) : new(movingInLineIndex, yIndex);

                        newButton.Position = _layoutGrid.GetGridElementPosition(emulatedStartPosition);
                        newButton.CallMoveIn();
                        ElementFader.KillTween(newButton);
                        ElementTweener.MoveIn(newButton, _layoutGrid.GetGridElementPosition(new(xIndex, yIndex)));
                        break;
                    default:
                        throw new ArgumentException(nameof(movementType));
                }
            }
        }

        for (var yIndex = 0; yIndex < ViewYCount; yIndex++)
        for (var xIndex = 0; xIndex < ViewXCount; xIndex++)
        {
            var nextViewItem = _nextView[xIndex, yIndex];
            if (nextViewItem.Data.IsNull) continue;
            var info = nextViewItem.AssignedButton!.Info!.Value;

            nextViewItem.AssignedButton.Info = ConstructInfo(
                info.Data!,
                info.XIndex,
                info.YIndex,
                resolvedViewportMinXIndex,
                resolvedViewportMinYIndex,
                dataSetMinXIndex,
                dataSetMinYIndex,
                resolvedViewportMaxXIndex,
                resolvedViewportMaxYIndex,
                dataSetMaxXIndex,
                dataSetMaxYIndex
            );
        }

        (_currentView, _nextView) = (_nextView, _currentView);

        foreach (var dataView in _nextView)
        {
            dataView.Data = NullableData.Null<TDataType>();
            dataView.AssignedButton = null;
        }

        return;

        byte GetEdgeType(int xIndex, int yIndex)
        {
            if (isY)
            {
                if (yIndex == isMovingOutLineIndex) return EDGE_OUT;
                if (yIndex == isMovingInLineIndex) return EDGE_IN;
                return EDGE_NORMAL;
            }

            if (xIndex == isMovingOutLineIndex) return EDGE_OUT;
            if (xIndex == isMovingInLineIndex) return EDGE_IN;
            return EDGE_NORMAL;
        }
    }

    private class DataView
    {
        public TButtonType? AssignedButton;
        public NullableData<TDataType> Data;
        public override string ToString() => $"Button: {AssignedButton?.Name ?? "Null"}, Data: {Data}";
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        while (_buttonPool.TryPop(out var instance)) instance.QueueFree();
    }
}