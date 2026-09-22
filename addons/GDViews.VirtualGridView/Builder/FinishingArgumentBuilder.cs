using Godot;
using GodotViews.VirtualGrid.Layout;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid.Builder;

class FinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument>(
    DataLayoutBuilder<TDataType> dataLayoutBuilder,
    IDataInspector<TDataType> dataInspector,
    PackedScene itemPrefab,
    Control itemContainer,
    IInfiniteLayoutGrid layoutGrid,
    TExtraArgument extraArgument
) : IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument>
    where TButtonType : VirtualGridViewItemArg<TDataType, TExtraArgument>
{
    private readonly TExtraArgument _extraArgument = extraArgument;
    private bool _autoHideHorizontalScrollBar;
    private bool _autoHideVerticalScrollBar;
    private bool _interactiveHorizontalScrollBar;
    private bool _interactiveVerticalScrollBar;
    private ScrollBar? _horizontalScrollBar;
    private IElementFader? _horizontalScrollBarFader;
    private IScrollBarTweener? _horizontalScrollBarTweener;

    private ScrollBar? _verticalScrollBar;
    private IElementFader? _verticalScrollBarFader;
    private IScrollBarTweener? _verticalScrollBarTweener;
    private FocusEdgeBehavior _horizontalFocusEdgeBehavior = FocusEdgeBehavior.None;
    private FocusEdgeBehavior _verticalFocusEdgeBehavior = FocusEdgeBehavior.None;
    private Control[] _additionalScrollInputTargets = [];

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> SetFocusEdgeBehavior(FocusEdgeBehavior behavior) =>
        SetFocusEdgeBehavior(behavior, behavior);

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> SetFocusEdgeBehavior(
        FocusEdgeBehavior horizontal,
        FocusEdgeBehavior vertical
    )
    {
        _horizontalFocusEdgeBehavior = horizontal;
        _verticalFocusEdgeBehavior = vertical;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureVerticalScrollBar(
        ScrollBar verticalScrollBar,
        IScrollBarTweener? tweener,
        IElementFader? fader,
        bool autoHide = false
    )
    {
        _verticalScrollBar = verticalScrollBar;
        _autoHideVerticalScrollBar = autoHide;
        _verticalScrollBarTweener = tweener;
        _verticalScrollBarFader = fader;
        _interactiveVerticalScrollBar = false;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureHorizontalScrollBar(
        ScrollBar horizontalScrollBar,
        IScrollBarTweener? tweener,
        IElementFader? fader,
        bool autoHide = false
    )
    {
        _horizontalScrollBar = horizontalScrollBar;
        _autoHideHorizontalScrollBar = autoHide;
        _horizontalScrollBarTweener = tweener;
        _horizontalScrollBarFader = fader;
        _interactiveHorizontalScrollBar = false;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureInteractiveVerticalScrollBar(
        ScrollBar verticalScrollBar,
        IElementFader? fader = null,
        bool autoHide = false
    )
    {
        _verticalScrollBar = verticalScrollBar;
        _autoHideVerticalScrollBar = autoHide;
        _verticalScrollBarTweener = null;
        _verticalScrollBarFader = fader;
        _interactiveVerticalScrollBar = true;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureInteractiveHorizontalScrollBar(
        ScrollBar horizontalScrollBar,
        IElementFader? fader = null,
        bool autoHide = false
    )
    {
        _horizontalScrollBar = horizontalScrollBar;
        _autoHideHorizontalScrollBar = autoHide;
        _horizontalScrollBarTweener = null;
        _horizontalScrollBarFader = fader;
        _interactiveHorizontalScrollBar = true;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureAdditionalScrollInput(params ReadOnlySpan<Control> controls)
    {
        foreach (var control in controls)
            ArgumentNullException.ThrowIfNull(control);

        _additionalScrollInputTargets = [..controls];
        return this;
    }

    public IVirtualGridView<TDataType> Build()
    {
        var dataLayoutSelectionBuilder = dataLayoutBuilder.DataLayoutSelectionBuilder;
        var viewAlignmentBuilder = dataLayoutSelectionBuilder.ViewHandlerBuilder;

        var view = new VirtualGridViewImpl<TDataType, TButtonType, TExtraArgument>(
            viewAlignmentBuilder.ViewportXCount,
            viewAlignmentBuilder.ViewportYCount,
            dataLayoutSelectionBuilder.ElementPositioner,
            dataLayoutSelectionBuilder.ElementTweener,
            dataLayoutSelectionBuilder.ElementFader,
            _horizontalScrollBar,
            _autoHideHorizontalScrollBar,
            _interactiveHorizontalScrollBar,
            _horizontalScrollBarTweener ?? ScrollBarTweeners.None,
            _horizontalScrollBarFader ?? ElementFaders.None,
            _verticalScrollBar,
            _autoHideVerticalScrollBar,
            _interactiveVerticalScrollBar,
            _verticalScrollBarTweener ?? ScrollBarTweeners.None,
            _verticalScrollBarFader ?? ElementFaders.None,
            dataInspector,
            dataLayoutBuilder.EqualityComparer,
            itemPrefab,
            itemContainer,
            layoutGrid,
            _extraArgument,
            _additionalScrollInputTargets
        );
        view.HorizontalFocusEdgeBehavior = _horizontalFocusEdgeBehavior;
        view.VerticalFocusEdgeBehavior = _verticalFocusEdgeBehavior;
        return view;
    }
}