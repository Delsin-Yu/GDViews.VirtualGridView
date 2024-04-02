namespace GodotViews.VirtualGrid;

public static partial class ElementPositioners
{
    public static IElementPositioner CreateCentered() => new CenteredElementPositioner();
    public static IElementPositioner CreateSide() => new SideElementPositioner();
}

