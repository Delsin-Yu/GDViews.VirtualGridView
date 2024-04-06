using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GodotViews.VirtualGrid;

public interface IDynamicGridViewer<T>
{
    int FixedMetric { get; }

    int GetDynamicMetric();

    bool TryGetGridElement(int fixedMetricIndex, int dynamicMetricIndex, [NotNullWhen(true)] out T? element);
}

public static class DataSetDefinition
{
    public static DataSetDefinition<T> Create<T>(IReadOnlyList<T> list, IReadOnlyList<int> dataSpan) => new(new CollectionGridViewerImpl<T>(list, dataSpan.Count), dataSpan);
    
    private class CollectionGridViewerImpl<T> : IDynamicGridViewer<T>
    {
        private readonly IReadOnlyList<T> _backing;

        public CollectionGridViewerImpl(IReadOnlyList<T> backing, int fixedMetric)
        {
            _backing = backing;
            FixedMetric = fixedMetric;
        }

        public int FixedMetric { get; }

        public int GetDynamicMetric()
        {
            var backingCount = _backing.Count;
            var initialMetric = backingCount / FixedMetric;
            if (backingCount % FixedMetric > 0) initialMetric++;
            return initialMetric;
        }

        public bool TryGetGridElement(int fixedMetricIndex, int dynamicMetricIndex, [NotNullWhen(true)] out T? element)
        {
            var index = dynamicMetricIndex * FixedMetric + fixedMetricIndex;
            if (_backing.Count <= index)
            {
                element = default;
                return false;
            }

            element = _backing[index]!;
            return true;
        }
    }
}