using System;
using GodotViews.VirtualGrid.Positioner;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid.Builder;

internal class ViewHandlerBuilder : IViewHandlerBuilder
{
    public ViewHandlerBuilder(int viewportColumns, int viewportRows)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(viewportColumns);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(viewportRows);

        ViewportColumns = viewportColumns;
        ViewportRows = viewportRows;
    }

    public int ViewportColumns { get; }
    public int ViewportRows { get; }

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