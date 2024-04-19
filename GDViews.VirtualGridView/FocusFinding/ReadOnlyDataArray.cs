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
    /// The current rows of the viewport.
    /// </summary>
    public readonly int DataSetRows;
    
    /// <summary>
    /// The current columns of the viewport.
    /// </summary>
    public readonly int DataSetColumns;
    private readonly IDataInspector<TDataType> _dataInspector;
    private readonly int _viewRows;
    private readonly int _viewColumns;

    internal ReadOnlyDataArray(IDataInspector<TDataType> dataInspector, int viewRows, int viewColumns)
    {
        _dataInspector = dataInspector;
        _viewRows = viewRows;
        _viewColumns = viewColumns;
        _dataInspector.GetDataSetCurrentMetrics(out DataSetRows, out DataSetColumns);
    }

    /// <summary>
    /// Trying to match the first element in the datasets that satisfies a specified condition.
    /// </summary>
    /// <param name="matchHandler">A function to test each element for a condition.</param>
    /// <param name="matchArgument">The argument passes to the <paramref name="matchHandler"/> to avoid closure allocation.</param>
    /// <param name="absoluteRowIndex">When this method returns, contains the row index of the matched value
    /// if any of the datasets element satisfies the <paramref name="matchHandler"/>; otherwise, -1.</param>
    /// <param name="absoluteColumnIndex">When this method returns, contains the column index of the matched value
    /// if any of the datasets element satisfies the <paramref name="matchHandler"/>; otherwise, -1.</param>
    /// <typeparam name="TMatchArgument">The type of the argument passes to the <paramref name="matchHandler"/>.</typeparam>
    /// <returns><see langword="true" /> if any of the datasets element satisfies the <paramref name="matchHandler"/>; otherwise, <see langword="false" />.</returns>
    /// <remarks>This optimized method is more suitable for matching across large chunks of datasets.</remarks>
    public bool TryGetData<TMatchArgument>(
        Func<TDataType, TMatchArgument, bool> matchHandler,
        TMatchArgument matchArgument,
        out int absoluteRowIndex,
        out int absoluteColumnIndex
    )
    {
        if (!Utils.SearchForData(
                _dataInspector,
                _viewRows,
                _viewColumns,
                out var viewData,
                matchHandler,
                matchArgument
            ))
        {
            absoluteRowIndex = -1;
            absoluteColumnIndex = -1;
            return false;
        }

        var (matchedRowIndex, matchedColumnOffset, matchedRowOffset, matchColumnIndex) = viewData;
        absoluteRowIndex = matchedRowOffset + matchedRowIndex;
        absoluteColumnIndex = matchedColumnOffset + matchColumnIndex;
        return true;
    }

    /// <summary>
    /// Trying to access the element at the specified datasets location based on the provided position. 
    /// </summary>
    /// <param name="dataRowIndex">The datasets row index.</param>
    /// <param name="dataColumnIndex">The datasets column index.</param>
    /// <param name="data">When this method returns, contains the value associated with the specified position
    /// if falls inside the datasets; otherwise, the default value for the type of the <paramref name="data" /> parameter.</param>
    /// <returns><see langword="true" /> if the specified position falls inside the datasets; otherwise, <see langword="false" />.</returns>
    /// <remarks>This method is less efficient than <see cref="TryGetData{TMatchArgument}"/>
    /// when enumerating through large chunks of datasets, as doing per-cell position mapping
    /// takes significantly longer than the optimized version of enumeration APIs by design.</remarks>
    public bool TryGetData(
        int dataRowIndex,
        int dataColumnIndex,
        [NotNullWhen(true)] out TDataType? data
    )
    {
        var viewRowIndex = dataRowIndex / _viewRows;
        var viewColumnIndex = dataColumnIndex / _viewColumns;
        var viewLocalRowOffset = dataRowIndex % _viewRows;
        var viewLocalColumnOffset = dataColumnIndex % _viewColumns;

        var cellData = _dataInspector.InspectViewCell(
            viewRowIndex,
            viewColumnIndex,
            viewLocalRowOffset,
            viewLocalColumnOffset
        );

        return cellData.TryUnwrap(out data);
    }
}