using Godot;
using GodotViews.VirtualGrid.Layout;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid.Builder;

internal class FinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument>(
    DataLayoutBuilder<TDataType> dataLayoutBuilder,
    IDataInspector<TDataType> dataInspector,
    PackedScene itemPrefab,
    Control itemContainer,
    IInfiniteLayoutGrid layoutGrid,
    TExtraArgument extraArgument) : IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument>
    where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    private bool _autoHideHorizontalScrollBar;
    private bool _autoHideVerticalScrollBar;

    private readonly TExtraArgument? _extraArgument = extraArgument;
    private ScrollBar? _horizontalScrollBar;
    private IElementFader? _horizontalScrollBarFader;
    private IScrollBarTweener? _horizontalScrollBarTweener;

    private ScrollBar? _verticalScrollBar;
    private IElementFader? _verticalScrollBarFader;
    private IScrollBarTweener? _verticalScrollBarTweener;

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
        return this;
    }

    public IVirtualGridView<TDataType> Build()
    {
        var dataLayoutSelectionBuilder = dataLayoutBuilder.DataLayoutSelectionBuilder;
        var viewAlignmentBuilder = dataLayoutSelectionBuilder.ViewHandlerBuilder;

        return new VirtualGridViewImpl<TDataType, TButtonType, TExtraArgument>(
            viewAlignmentBuilder.ViewportRows,
            viewAlignmentBuilder.ViewportColumns,
            dataLayoutSelectionBuilder.ElementPositioner,
            dataLayoutSelectionBuilder.ElementTweener,
            dataLayoutSelectionBuilder.ElementFader,
            _horizontalScrollBar,
            _autoHideHorizontalScrollBar,
            _horizontalScrollBarTweener ?? ScrollBarTweeners.None,
            _horizontalScrollBarFader ?? ElementFaders.None,
            _verticalScrollBar,
            _autoHideVerticalScrollBar,
            _verticalScrollBarTweener ?? ScrollBarTweeners.None,
            _verticalScrollBarFader ?? ElementFaders.None,
            dataInspector,
            dataLayoutBuilder.EqualityComparer,
            itemPrefab,
            itemContainer,
            layoutGrid,
            _extraArgument
        );
    }
}