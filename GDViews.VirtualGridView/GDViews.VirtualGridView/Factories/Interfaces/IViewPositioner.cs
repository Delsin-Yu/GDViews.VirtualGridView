using Godot;

namespace GodotViews.VirtualGrid;

public interface IViewPositioner
{
    void GetTargetPosition(Vector2I viewportSize, Vector2I dataPositionRelativeToViewport, out Vector2I targetDataPosition);
    bool RequireScrollWhenMovingOnSideElement => false;
}