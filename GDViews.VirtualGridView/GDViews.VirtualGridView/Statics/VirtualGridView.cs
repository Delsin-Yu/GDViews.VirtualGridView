namespace GodotViews.VirtualGrid;

public static class VirtualGridView
{
    public static IViewHandlerBuilder Create(int viewportColumns, int viewportRows) => 
        new ViewHandlerBuilder(viewportColumns, viewportRows);
}