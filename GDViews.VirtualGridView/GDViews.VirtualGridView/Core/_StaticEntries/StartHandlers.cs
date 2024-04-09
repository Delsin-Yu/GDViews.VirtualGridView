using Godot;

namespace GodotViews.VirtualGrid;

public static partial class StartHandlers
{
    public static readonly IDataStartHandler<Vector2I> DataPosition = new DataStartPositionHandler();
    public static readonly IViewStartHandler<Vector2I> ViewPosition = new ViewStartPositionHandler();
    public static readonly IViewStartHandler<Vector2I> ViewCenter = new ViewCenterStartPositionHandler();
}