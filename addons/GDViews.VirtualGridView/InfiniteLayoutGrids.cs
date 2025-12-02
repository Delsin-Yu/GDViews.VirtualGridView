using Godot;
using GodotViews.VirtualGrid.Layout;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a built-in <see cref="IInfiniteLayoutGrid"/>.
/// </summary>
public static class InfiniteLayoutGrids
{
    /// <summary>
    /// Create a simple infinite layout grid that has functions equivalent to a <see cref="GridContainer"/>.
    /// </summary>
    /// <returns>The instance of the created infinite layout grid that
    /// can be passed to the builders of <see cref="IVirtualGridView{TDataType}"/>.</returns>
    public static IInfiniteLayoutGrid CreateSimple(Vector2 itemSize, Vector2? itemSeparation = null) => new SimpleImpl(itemSize, itemSeparation ?? Vector2.Zero);

    private class SimpleImpl(Vector2 itemSize, Vector2 itemSeparation) : IInfiniteLayoutGrid
    {
        public Vector2 ItemSize { get; } = itemSize;
        public Vector2 ItemSeparation { get; } = itemSeparation;

        public Vector2 GetGridElementPosition(Vector2I gridPosition) => gridPosition * (ItemSize + ItemSeparation);
    }
}