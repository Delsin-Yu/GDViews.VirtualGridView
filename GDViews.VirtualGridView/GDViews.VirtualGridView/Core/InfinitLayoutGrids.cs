using Godot;

namespace GodotViews.VirtualGrid;

public static class InfinitLayoutGrids
{
    public static IInfiniteLayoutGrid CreateSimple(Vector2 itemSize, Vector2? itemSeparation = null) => 
        new SimpleInfiniteLayoutGrid(itemSize, itemSeparation ?? Vector2.Zero);
    
    private class SimpleInfiniteLayoutGrid(Vector2 itemSize, Vector2 itemSeparation) : IInfiniteLayoutGrid
    {
        public Vector2 ItemSize { get; } = itemSize;
        public Vector2 ItemSeparation { get; } = itemSeparation;

        public Vector2 GetGridElementPosition(Vector2I gridPosition) => 
            gridPosition * (ItemSize + ItemSeparation);
    }
}