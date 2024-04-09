using System.Diagnostics.CodeAnalysis;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Dynamic Grid Viewer is the abstraction of a 2D list with one fixed dimension,
/// it can also be used for emulating a 2D view from a regular list.<br/>
/// You may create an instance of the dynamic grid viewer from the <see cref="DynamicGridViewers"/> class.
/// </summary>
/// <typeparam name="T">The type of the 2D list.</typeparam>
public interface IDynamicGridViewer<T>
{
    /// <summary>
    /// The fixed metric of the 2D list, that is,
    /// this value stays the same as the total length of the list changes.
    /// </summary>
    int FixedMetric { set; }

    /// <summary>
    /// Obtains the dynamic metric of the 2D list, that is,
    /// the value that changes as the total length of the list changes.
    /// </summary>
    /// <returns>The calculated dynamic metric.</returns>
    int GetDynamicMetric();

    /// <summary>
    /// Trying to access the element at the specified location base on the provided metric indexes. 
    /// </summary>
    /// <param name="fixedMetricIndex">The fixed metric index.</param>
    /// <param name="dynamicMetricIndex">The dynamic metric index</param>
    /// <param name="element">When this method returns, contains the value associated with the specified metric indexes
    /// if falls inside the 2D list; otherwise, the default value for the type of the <paramref name="element" /> parameter.</param>
    /// <returns><see langword="true" /> if the specified metric indexes falls inside the 2D list; otherwise, <see langword="false" />.</returns>
    bool TryGetGridElement(int fixedMetricIndex, int dynamicMetricIndex, [NotNullWhen(true)] out T? element);
}