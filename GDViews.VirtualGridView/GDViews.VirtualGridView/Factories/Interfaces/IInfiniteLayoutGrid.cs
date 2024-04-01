using Godot;

namespace GodotViews.VirtualGrid;

public interface IInfiniteLayoutGrid
{
    Vector2 ItemSize { get; }
    Vector2 ItemSeparation { get; }
    Vector2 GetGridElementPosition(Vector2I gridPosition);
}