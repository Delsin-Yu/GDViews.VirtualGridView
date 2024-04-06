namespace GodotViews.VirtualGrid;

public static partial class ElementPositioners
{
    public static IElementPositioner Centered { get; } = new CenteredElementPositioner();
    public static IElementPositioner Side { get; } = new SideElementPositioner();
}