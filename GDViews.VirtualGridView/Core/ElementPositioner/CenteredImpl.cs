using Godot;

namespace GodotViews.VirtualGrid;

public static partial class ElementPositioners
{
    private class CenteredImpl : IElementPositioner
    {
        public void GetTargetPosition(Vector2I viewportSize, Vector2I positionRelativeToViewport, out Vector2I destPositionRelativeToViewport)
        {
            destPositionRelativeToViewport = viewportSize / 2;
        }
    }
}