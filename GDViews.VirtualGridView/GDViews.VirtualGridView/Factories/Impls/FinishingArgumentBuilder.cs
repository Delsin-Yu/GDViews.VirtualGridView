using Godot;

namespace GodotViews.VirtualGrid;

internal class FinishingArgumentBuilder<TDataType, TButtonType>(
    DelegateBuilder<TDataType, TButtonType> delegateBuilder,
    PackedScene itemPrefab,
    Control itemContainer,
    IInfiniteLayoutGrid layoutGrid) : IFinishingArgumentBuilder<TDataType, TButtonType> where TButtonType : Button
{
    private ScrollBar? _horizontalScrollBar;
    private ScrollBar? _verticalScrollBar;

    public IFinishingArgumentBuilder<TDataType, TButtonType> ConfigureVerticalScrollBar(ScrollBar verticalScrollBar)
    {
        _verticalScrollBar = verticalScrollBar;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType> ConfigureHorizontalScrollBar(ScrollBar horizontalScrollBar)
    {
        _horizontalScrollBar = horizontalScrollBar;
        return this;
    }

    public IVirtualGridView<TDataType, TButtonType> Build()
    {
        var dataLayoutBuilder = delegateBuilder.DataLayoutBuilder;
        var dataLayoutSelectionBuilder = dataLayoutBuilder.DataLayoutSelectionBuilder;
        var viewAlignmentBuilder = dataLayoutSelectionBuilder.ViewHandlerBuilder;

        return new VirtualGridViewImpl<TDataType, TButtonType>(
            viewAlignmentBuilder.ViewportRows,
            viewAlignmentBuilder.ViewportColumns,
            dataLayoutSelectionBuilder.ViewHandler,
            dataLayoutSelectionBuilder.ElementTweener,
            dataLayoutSelectionBuilder.ElementFader,
            dataLayoutBuilder.DataLayoutDirection,
            delegateBuilder.DrawHandler,
            delegateBuilder.FocusEnteredHandler,
            delegateBuilder.FocusExitedHandler,
            delegateBuilder.PressedHandler,
            _horizontalScrollBar,
            _verticalScrollBar,
            delegateBuilder.DataInspector,
            dataLayoutBuilder.EqualityComparer,
            itemPrefab,
            itemContainer,
            layoutGrid
        );
    }
}