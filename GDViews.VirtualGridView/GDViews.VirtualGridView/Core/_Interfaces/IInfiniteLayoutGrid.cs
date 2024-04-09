using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Infinite Layout Grid is the abstraction of a layout grid that has infinite amount of cells.<br/>
/// You may create an instance of the dynamic grid viewer from the <see cref="InfiniteLayoutGrids"/> class.
/// </summary>
public interface IInfiniteLayoutGrid
{
    /// <summary>
    /// The size of a cell.
    /// </summary>
    Vector2 ItemSize { get; }
    
    /// <summary>
    /// The distance between cells.
    /// </summary>
    Vector2 ItemSeparation { get; }
    
    /// <summary>
    /// Calculate the layout position for the given <paramref name="gridPosition"/>.
    /// </summary>
    /// <param name="gridPosition">The position to calculate the layout position from.</param>
    /// <returns>The calculated layout position.</returns>
    Vector2 GetGridElementPosition(Vector2I gridPosition);
}