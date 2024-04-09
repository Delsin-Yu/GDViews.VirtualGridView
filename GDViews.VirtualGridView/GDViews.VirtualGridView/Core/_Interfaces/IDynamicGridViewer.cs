﻿using System.Diagnostics.CodeAnalysis;

namespace GodotViews.VirtualGrid;

public interface IDynamicGridViewer<T>
{
    int FixedMetric { set; }

    int GetDynamicMetric();

    bool TryGetGridElement(int fixedMetricIndex, int dynamicMetricIndex, [NotNullWhen(true)] out T? element);
}