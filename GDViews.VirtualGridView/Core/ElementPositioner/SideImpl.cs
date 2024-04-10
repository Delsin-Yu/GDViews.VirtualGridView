using System;
using Godot;

namespace GodotViews.VirtualGrid;

public static partial class ElementPositioners
{
    private class SideImpl : IElementPositioner
    {
        public void GetTargetPosition(Vector2I viewportSize, Vector2I positionRelativeToViewport, out Vector2I destPositionRelativeToViewport)
        {
            if (positionRelativeToViewport >= Vector2I.Zero && positionRelativeToViewport < viewportSize)
            {
                destPositionRelativeToViewport = positionRelativeToViewport;
                return;
            }

            destPositionRelativeToViewport = positionRelativeToViewport.Clamp(Vector2I.Zero, viewportSize - Vector2I.One);
        }

        public void GetDragViewPosition(Vector2I viewportSize, MoveDirection dragDirection, Vector2I positionRelativeToViewport, out Vector2I destPositionRelativeToViewport)
        {
            destPositionRelativeToViewport = dragDirection switch
            {
                MoveDirection.Up => positionRelativeToViewport with { Y = 0 },
                MoveDirection.Down => positionRelativeToViewport with { Y = viewportSize.Y - 1 },
                MoveDirection.Left => positionRelativeToViewport with { X = 0 },
                MoveDirection.Right => positionRelativeToViewport with { X = viewportSize.X - 1 },
                _ => throw new ArgumentOutOfRangeException(nameof(dragDirection), dragDirection, null)
            };
        }
    }
}