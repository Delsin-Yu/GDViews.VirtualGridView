﻿using Godot;

namespace GodotViews.VirtualGrid;

public static class VirtualGridView
{
    public static IViewHandlerBuilder Create(int viewportColumns, int viewportRows) => 
        new ViewHandlerBuilder(viewportColumns, viewportRows);
    
    internal static bool TryGetMoveDirection(ref Vector2I vector, out Vector2I moveDirection)
    {
        moveDirection = Vector2I.Zero;
        if (vector == Vector2I.Zero)
        {
            return false;
        }
        
        switch (vector.X)
        {
            case > 0:
                moveDirection.X = -1;
                vector.X -= 1;
                return true;
            case < 0:
                moveDirection.X = 1;
                vector.X += 1;
                return true;
        }
        switch (vector.Y)
        {
            case > 0:
                moveDirection.Y = -1;
                vector.Y -= 1;
                return true;
            case < 0:
                moveDirection.Y = 1;
                vector.Y += 1;
                return true;
        }

        return true;
    }
    
    internal static Vector2I CreatePosition(int rowIndex, int columnIndex) => new(columnIndex, rowIndex);

}