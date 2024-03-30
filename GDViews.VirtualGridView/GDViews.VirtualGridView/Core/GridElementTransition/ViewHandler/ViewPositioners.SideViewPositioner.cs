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

        public bool RequireScrollWhenMovingOnSideElement => true;
    }
}