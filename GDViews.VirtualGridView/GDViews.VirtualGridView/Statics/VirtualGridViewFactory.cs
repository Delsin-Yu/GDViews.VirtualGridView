namespace GodotViews.VirtualGrid;

public static class VirtualGridViewFactory
{
    public static IViewHandlerBuilder CreateView(int viewportColumns, int viewportRows) => 
        new ViewHandlerBuilder(viewportColumns, viewportRows);
}