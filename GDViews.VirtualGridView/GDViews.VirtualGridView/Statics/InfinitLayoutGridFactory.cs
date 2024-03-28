﻿using System;
using Godot;

namespace GodotViews.VirtualGrid;

public static class InfinitLayoutGridFactory
{
    public static IInfiniteLayoutGrid CreateSimple(Vector2 itemSize, Vector2? itemSeparation = null) => 
        new SimpleInfiniteLayoutGrid(itemSize, itemSeparation ?? Vector2.Zero);
    
    private class SimpleInfiniteLayoutGrid(Vector2 itemSize, Vector2 itemSeparation) : IInfiniteLayoutGrid
    {
        public Vector2 GetGridElementPosition(Vector2I gridPosition) => 
            gridPosition * (itemSize + itemSeparation);
    }
}