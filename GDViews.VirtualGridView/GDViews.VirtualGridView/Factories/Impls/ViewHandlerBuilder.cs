using System;

namespace GodotViews.VirtualGrid;

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

    public IDataLayoutBuilder WithHandlers(IViewPositioner viewPositioner, IElementTweener elementTweener, IElementFader elementFader) => new DataLayoutSelectionBuilder(this, viewPositioner, elementTweener, elementFader);
}