using System;
using System.Diagnostics.CodeAnalysis;
using GodotViews.VirtualGrid.Builder;

namespace GodotViews.VirtualGrid.FocusFinding;

/// <summary>
/// Allow the developer to indirectly access the content of the datasets.
/// </summary>
public readonly struct ReadOnlyDataArray<TDataType>
{
    /// <summary>
    /// The current xs of the viewport.
    /// </summary>
    public readonly int DataSetXCount;
    
    /// <summary>
    /// The current ys of the viewport.
    /// </summary>
    public readonly int DataSetYCount;

    private readonly IDataInspector<TDataType> _dataInspector;
    private readonly int _viewXCount;
    private readonly int _viewYCount;

    internal ReadOnlyDataArray(IDataInspector<TDataType> dataInspector, int viewXCount, int viewYCount)
    {
        _dataInspector = dataInspector;
        _viewXCount = viewXCount;
        _viewYCount = viewYCount;
        _dataInspector.GetDataSetCurrentMetrics(out DataSetXCount, out DataSetYCount);
    }

    /// <summary>
    /// Trying to match the first element in the datasets that satisfies a specified condition.
    /// </summary>
    /// <param name="matchHandler">A function to test each element for a condition.</param>
    /// <param name="matchArgument">The argument passes to the <paramref name="matchHandler"/> to avoid closure allocation.</param>
    /// <param name="absoluteXIndex">When this method returns, contains the x index of the matched value
    ///     if any of the datasets element satisfies the <paramref name="matchHandler"/>; otherwise, -1.</param>
    /// <param name="absoluteYIndex">When this method returns, contains the y index of the matched value
    ///     if any of the datasets element satisfies the <paramref name="matchHandler"/>; otherwise, -1.</param>
    /// <typeparam name="TMatchArgument">The type of the argument passes to the <paramref name="matchHandler"/>.</typeparam>
    /// <returns><see langword="true" /> if any of the datasets element satisfies the <paramref name="matchHandler"/>; otherwise, <see langword="false" />.</returns>
    /// <remarks>This optimized method is more suitable for matching across large chunks of datasets.</remarks>
    public bool TryGetData<TMatchArgument>(
        Func<TDataType, TMatchArgument, bool> matchHandler,
        TMatchArgument matchArgument,
        out int absoluteXIndex,
        out int absoluteYIndex)
    {
        if (!Utils.SearchForData(
                _dataInspector,
                _viewXCount,
                _viewYCount,
                out var viewData,
                matchHandler,
                matchArgument
            ))
        {
            absoluteXIndex = -1;
            absoluteYIndex = -1;
            return false;
        }

        var (matchedYIndex, matchedXOffset, matchedYOffset, matchXIndex) = viewData;
        absoluteYIndex = matchedYOffset + matchedYIndex;
        absoluteXIndex = matchedXOffset + matchXIndex;
        return true;
    }

    /// <summary>
    /// Trying to access the element at the specified datasets location based on the provided position. 
    /// </summary>
    /// <param name="dataXIndex">The datasets x index.</param>
    /// <param name="dataYIndex">The datasets y index.</param>
    /// <param name="data">When this method returns, contains the value associated with the specified position
    ///     if falls inside the datasets; otherwise, the default value for the type of the <paramref name="data" /> parameter.</param>
    /// <returns><see langword="true" /> if the specified position falls inside the datasets; otherwise, <see langword="false" />.</returns>
    /// <remarks>This method is less efficient than <see cref="TryGetData{TMatchArgument}"/>
    /// when enumerating through large chunks of datasets, as doing per-cell position mapping
    /// takes significantly longer than the optimized version of enumeration APIs by design.</remarks>
    public bool TryGetData(
        int dataXIndex,
        int dataYIndex,
        [NotNullWhen(true)] out TDataType? data
    )
    {
        var viewXIndex = dataXIndex / _viewXCount;
        var viewYIndex = dataYIndex / _viewYCount;
        var viewLocalXOffset = dataXIndex % _viewXCount;
        var viewLocalYOffset = dataYIndex % _viewYCount;

        var cellData = _dataInspector.InspectViewCell(
            viewYIndex,
            viewXIndex,
            viewLocalYOffset,
            viewLocalXOffset
        );

        return cellData.TryUnwrap(out data);
    }
}