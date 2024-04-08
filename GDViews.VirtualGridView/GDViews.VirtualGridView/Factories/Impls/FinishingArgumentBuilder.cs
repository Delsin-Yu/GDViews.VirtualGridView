using Godot;

namespace GodotViews.VirtualGrid;

internal class FinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument>(
    DataLayoutBuilder<TDataType> dataLayoutBuilder,
    IDataInspector<TDataType> dataInspector,
    PackedScene itemPrefab,
    Control itemContainer,
    IInfiniteLayoutGrid layoutGrid) : IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    private ScrollBar? _horizontalScrollBar;
    private bool _autoHideHorizontalScrollBar;
    private IScrollBarTweener? _horizontalScrollBarTweener;
    private IElementFader? _horizontalScrollBarFader;
    
    private ScrollBar? _verticalScrollBar;
    private bool _autoHideVerticalScrollBar;
    private IScrollBarTweener? _verticalScrollBarTweener;
    private IElementFader? _verticalScrollBarFader;
    
    private TExtraArgument? _extraArgument;

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureVerticalScrollBar(
        ScrollBar verticalScrollBar,
        IScrollBarTweener? tweener,
        IElementFader? fader,
        bool autoHide = false
    )
    {
        _verticalScrollBar = verticalScrollBar;
        _autoHideHorizontalScrollBar = autoHide;
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
        _autoHideVerticalScrollBar = autoHide;
        _horizontalScrollBarTweener = tweener;
        _horizontalScrollBarFader = fader;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureExtraArgument(TExtraArgument extraArgument)
    {
        _extraArgument = extraArgument;
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