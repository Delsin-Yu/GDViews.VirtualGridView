using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GodotViews.VirtualGrid;

public static partial class DynamicGridViewers
{
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