using System.Collections.Generic;
using GodotViews.VirtualGrid.Positioner;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid.Builder;

internal class DataLayoutSelectionBuilder(ViewHandlerBuilder viewHandlerBuilder, IElementPositioner elementPositioner, IElementTweener elementTweener, IElementFader elementFader) : IDataLayoutBuilder
{
    public ViewHandlerBuilder ViewHandlerBuilder { get; } = viewHandlerBuilder;
    public IElementPositioner ElementPositioner { get; } = elementPositioner;
    public IElementTweener ElementTweener { get; } = elementTweener;
    public IElementFader ElementFader { get; } = elementFader;

    public IHorizontalDataLayoutBuilder<TDataType> WithHorizontalDataLayout<TDataType>(
        IEqualityComparer<TDataType>? equalityComparer = null,
        bool reverseLocalLayout = false
    ) =>
        new DataLayoutBuilder<TDataType>(
            this,
            equalityComparer,
            reverseLocalLayout,
            true
        );

    public IVerticalDataLayoutBuilder<TDataType> WithVerticalDataLayout<TDataType>(
        IEqualityComparer<TDataType>? equalityComparer = null,
        bool reverseLocalLayout = false
    ) =>
        new DataLayoutBuilder<TDataType>(
            this,
            equalityComparer,
            reverseLocalLayout,
            false
        );
}