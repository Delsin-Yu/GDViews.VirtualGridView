using System;
using System.Diagnostics.CodeAnalysis;

namespace GodotViews.VirtualGrid;

public readonly struct ReadOnlyDataArray<TDataType>
{
    public readonly int DataSetRows;
    public readonly int DataSetColumns;
    private readonly IDataInspector<TDataType> _dataInspector;
    private readonly int _viewRows;
    private readonly int _viewColumns;

    internal ReadOnlyDataArray(IDataInspector<TDataType> dataInspector, int viewRows, int viewColumns)
    {
        _dataInspector = dataInspector;
        _viewRows = viewRows;
        _viewColumns = viewColumns;
        _dataInspector.GetDataSetMetrics(out DataSetRows, out DataSetColumns);
    }

    public bool TryGetData<TMatchArgument>(Func<TDataType, TMatchArgument, bool> matchHandler, TMatchArgument matchArgument, out int absoluteRowIndex, out int absoluteColumnIndex)
    {
        if (!VirtualGridView.SearchForData(
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
    
    public bool TryGetData(int dataRowIndex, int dataColumnIndex, [NotNullWhen(true)] out TDataType? data)
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