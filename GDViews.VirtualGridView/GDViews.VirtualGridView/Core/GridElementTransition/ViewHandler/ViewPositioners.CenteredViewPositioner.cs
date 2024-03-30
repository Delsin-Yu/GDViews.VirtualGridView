using Godot;

namespace GodotViews.VirtualGrid;

public static partial class ViewPositioners
{
    private class CenteredViewPositioner : IViewPositioner
    {
        public void GetTargetPosition(Vector2I viewportSize, Vector2I dataPositionRelativeToViewport, out Vector2I targetDataPosition)
        {
            targetDataPosition = viewportSize / 2;
        }
    }
}