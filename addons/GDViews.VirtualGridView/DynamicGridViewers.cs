using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GodotViews.VirtualGrid.Viewer;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a built-in <see cref="IDynamicGridViewer{T}"/>.
/// </summary>
public static class DynamicGridViewers
{
    /// <summary>
    /// Create an dynamic grid viewer that emulates a 2D list view from provided regular list.
    /// </summary>
    /// <param name="list">The backing list to emulate from.</param>
    /// <typeparam name="T">The type of elements in the list</typeparam>
    /// <returns>The instance of the created dynamic grid viewer that
    /// can be passed to the builders of <see cref="IVirtualGridView{TDataType}"/>
    /// for constructing the datasets.</returns>
    public static IDynamicGridViewer<T> CreateList<T>(IReadOnlyList<T> list) => new CollectionImpl<T>(list);

    private class CollectionImpl<T>(IReadOnlyList<T> backing) : IDynamicGridViewer<T>
    {
        public int FixedMetric { get; set; }

        public int GetDynamicMetric()
        {
            var backingCount = backing.Count;
            var initialMetric = backingCount / FixedMetric;
            if (backingCount % FixedMetric > 0) initialMetric++;
            return initialMetric;
        }

        public bool TryGetGridElement(int fixedMetricIndex, int dynamicMetricIndex, [NotNullWhen(true)] out T? element)
        {
            var index = dynamicMetricIndex * FixedMetric + fixedMetricIndex;
            if (backing.Count <= index)
            {
                element = default;
                return false;
            }

            element = backing[index]!;
            return true;
        }
    }
}