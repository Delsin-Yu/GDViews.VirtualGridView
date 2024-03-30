using System.Collections.Generic;

namespace GodotViews.VirtualGrid;

internal class DataLayoutSelectionBuilder(ViewHandlerBuilder viewHandlerBuilder, IViewPositioner viewPositioner, IElementTweener elementTweener, IElementFader elementFader) : IDataLayoutBuilder
{
    public ViewHandlerBuilder ViewHandlerBuilder { get; } = viewHandlerBuilder;
    public IViewPositioner ViewPositioner { get; } = viewPositioner;
    public IElementTweener ElementTweener { get; } = elementTweener;
    public IElementFader ElementFader { get; } = elementFader;

    public IHorizontalDataLayoutBuilder<TDataType> WithHorizontalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false) => new DataLayoutBuilder<TDataType>(this, DataLayoutDirection.Horizontal, equalityComparer, reverseLocalLayout);

    public IVerticalDataLayoutBuilder<TDataType> WithVerticalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false) => new DataLayoutBuilder<TDataType>(this, DataLayoutDirection.Vertical, equalityComparer, reverseLocalLayout);
}