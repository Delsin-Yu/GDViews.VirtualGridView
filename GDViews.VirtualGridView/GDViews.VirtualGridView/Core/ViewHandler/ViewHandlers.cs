using Godot;

namespace GodotViews.VirtualGrid;

public static class ViewPositioners
{
    public static IViewPositioner CreateCentered() => new CenteredViewPositioner();
    // public static IViewHandler CreateBorderAlignedHandler();

    private class CenteredViewPositioner : IViewPositioner
    {
        public void GetTargetPosition(Vector2I viewportSize, Vector2I dataPositionRelativeToViewport, out Vector2I targetDataPosition)
        {
            targetDataPosition = viewportSize / 2;
        }
    }
}

