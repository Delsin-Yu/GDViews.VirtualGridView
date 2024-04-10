﻿using Godot;

namespace GodotViews.VirtualGrid;

public static partial class InfiniteLayoutGrids
{
    private class SimpleImpl(Vector2 itemSize, Vector2 itemSeparation) : IInfiniteLayoutGrid
    {
        public Vector2 ItemSize { get; } = itemSize;
        public Vector2 ItemSeparation { get; } = itemSeparation;

        public Vector2 GetGridElementPosition(Vector2I gridPosition) => gridPosition * (ItemSize + ItemSeparation);
    }
}