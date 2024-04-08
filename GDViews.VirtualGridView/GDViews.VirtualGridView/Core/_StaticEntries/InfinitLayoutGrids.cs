using Godot;

namespace GodotViews.VirtualGrid;

public static partial class InfinitLayoutGrids
{
    public static IInfiniteLayoutGrid CreateSimple(Vector2 itemSize, Vector2? itemSeparation = null) => new SimpleInfiniteLayoutGrid(itemSize, itemSeparation ?? Vector2.Zero);
}