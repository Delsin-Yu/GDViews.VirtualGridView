using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a built-in <see cref="IInfiniteLayoutGrid"/>.
/// </summary>
public static partial class InfiniteLayoutGrids
{
    /// <summary>
    /// Create a simple infinite layout grid that has functions equivalent to a <see cref="GridContainer"/>.
    /// </summary>
    /// <returns>The instance of the created dynamic grid viewer that
    /// can be passed to the builders of <see cref="IVirtualGridView{TDataType}"/>.</returns>
    public static IInfiniteLayoutGrid CreateSimple(Vector2 itemSize, Vector2? itemSeparation = null) => 
        new SimpleInfiniteLayoutGrid(itemSize, itemSeparation ?? Vector2.Zero);
}