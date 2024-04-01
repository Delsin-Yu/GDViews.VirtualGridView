using System;
using Godot;

namespace GodotViews.VirtualGrid;

public static partial class ViewPositioners
{
    private class SideViewPositioner : IViewPositioner
    {
        public void GetTargetPosition(Vector2I viewportSize, Vector2I dataPositionRelativeToViewport, out Vector2I targetDataPosition)
        {
            if (dataPositionRelativeToViewport >= Vector2I.Zero && dataPositionRelativeToViewport < viewportSize)
            {
                targetDataPosition = dataPositionRelativeToViewport;
                return;
            }

            targetDataPosition = dataPositionRelativeToViewport.Clamp(Vector2I.Zero, viewportSize - Vector2I.One);
        }

        public void GetDragViewPosition(Vector2I viewportSize, MoveDirection dragDirection, Vector2I currentFocusPosition, out Vector2I targetFocusPosition)
        {
            targetFocusPosition = dragDirection switch
            {
                MoveDirection.Up => currentFocusPosition with { Y = 0 },
                MoveDirection.Down => currentFocusPosition with { Y = viewportSize.Y - 1 },
                MoveDirection.Left => currentFocusPosition with { X = 0 },
                MoveDirection.Right => currentFocusPosition with { X = viewportSize.X - 1 },
                _ => throw new ArgumentOutOfRangeException(nameof(dragDirection), dragDirection, null)
            };
        }
    }
}