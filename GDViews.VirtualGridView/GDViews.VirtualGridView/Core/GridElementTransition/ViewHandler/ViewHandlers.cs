namespace GodotViews.VirtualGrid;

public static partial class ViewPositioners
{
    public static IViewPositioner CreateCentered() => new CenteredViewPositioner();
    public static IViewPositioner CreateSide() => new SideViewPositioner();
}

