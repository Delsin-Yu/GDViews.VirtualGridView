using Godot;

namespace GodotViews.VirtualGrid;

public interface IInfiniteLayoutGrid
{
    Vector2 GetGridElementPosition(Vector2I gridPosition);
}