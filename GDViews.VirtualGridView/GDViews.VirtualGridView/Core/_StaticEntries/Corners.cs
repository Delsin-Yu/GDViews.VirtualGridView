using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a set of predefined <see cref="Vector2I"/> for indicating the corners defined by the <see cref="IVirtualGridView{TDataType}"/>
/// </summary>
public static class Corners
{
    /// <summary>The TopLeft corner.</summary>
    public static readonly Vector2I TopLeft = Vector2I.Zero;

    /// <summary>The TopRight corner.</summary>
    public static readonly Vector2I TopRight = new(0, -1);

    /// <summary>The BottomLeft corner.</summary>
    public static readonly Vector2I BottomLeft = new(-1, 0);

    /// <summary>The BottomRight corner.</summary>
    public static readonly Vector2I BottomRight = new(-1, -1);
}