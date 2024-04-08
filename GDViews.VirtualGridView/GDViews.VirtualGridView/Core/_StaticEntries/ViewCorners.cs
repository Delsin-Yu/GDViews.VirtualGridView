using Godot;

namespace GodotViews.VirtualGrid;

public static class ViewCorners
{
    public static readonly Vector2I TopLeft = Vector2I.Zero;
    public static readonly Vector2I TopRight = new(0, -1);
    public static readonly Vector2I BottomLeft = new(-1, 0);
    public static readonly Vector2I BottomRight = new(-1, -1);
}