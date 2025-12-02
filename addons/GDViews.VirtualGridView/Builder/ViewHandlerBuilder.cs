using System;
using GodotViews.VirtualGrid.Positioner;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid.Builder;

class ViewHandlerBuilder : IViewHandlerBuilder
{
    public ViewHandlerBuilder(int viewportXCount, int viewportYCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(viewportXCount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(viewportYCount);

        ViewportXCount = viewportXCount;
        ViewportYCount = viewportYCount;
    }

    public int ViewportXCount { get; }
    public int ViewportYCount { get; }

    public IDataLayoutBuilder WithHandlers(
        IElementPositioner elementPositioner,
        IElementTweener elementTweener,
        IElementFader elementFader
    ) =>
        new DataLayoutSelectionBuilder(
            this,
            elementPositioner,
            elementTweener,
            elementFader
        );
}