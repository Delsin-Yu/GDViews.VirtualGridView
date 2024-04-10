using System.Collections.Generic;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a built-in <see cref="IDynamicGridViewer{T}"/>.
/// </summary>
public static partial class DynamicGridViewers
{
    /// <summary>
    /// Create an dynamic grid viewer that emulates a 2D list view from provided regular list.
    /// </summary>
    /// <param name="list">The backing list to emulate from.</param>
    /// <typeparam name="T">The type of elements in the list</typeparam>
    /// <returns>The instance of the created dynamic grid viewer that
    /// can be passed to the builders of <see cref="IVirtualGridView{TDataType}"/>
    /// for constructing the datasets.</returns>
    public static IDynamicGridViewer<T> CreateList<T>(IReadOnlyList<T> list) => 
        new CollectionImpl<T>(list);
}