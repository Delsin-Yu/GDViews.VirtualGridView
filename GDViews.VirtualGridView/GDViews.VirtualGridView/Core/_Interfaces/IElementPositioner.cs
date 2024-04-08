using Godot;

namespace GodotViews.VirtualGrid;

public interface IElementPositioner
{
    void GetTargetPosition(Vector2I viewportSize, Vector2I dataPositionRelativeToViewport, out Vector2I targetDataPosition);

    void GetDragViewPosition(Vector2I viewportSize, MoveDirection dragDirection, Vector2I currentFocusPosition, out Vector2I targetFocusPosition) => targetFocusPosition = currentFocusPosition;
}