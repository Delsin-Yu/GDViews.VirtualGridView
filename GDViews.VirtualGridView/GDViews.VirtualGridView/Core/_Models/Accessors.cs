using System;
using System.Diagnostics.CodeAnalysis;

namespace GodotViews.VirtualGrid;

public readonly struct ReadOnlyViewArray
{
    public bool this[int columnIndex, int rowIndex] => _backingResolver(_backing[columnIndex, rowIndex]);
    public readonly int ViewRows;
    public readonly int ViewColumns;
    private readonly object[,] _backing;
    private readonly Func<object, bool> _backingResolver;

    internal ReadOnlyViewArray(
        object[,] backing,
        int viewRows,
        int viewColumns,
        Func<object, bool> backingResolver
    )
    {
        _backing = backing;
        _backingResolver = backingResolver;
        ViewRows = viewRows;
        ViewColumns = viewColumns;
    }
}

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
        _dataInspector.GetDataSetCurrentMetrics(out DataSetRows, out DataSetColumns);
    }

    public bool TryGetData<TMatchArgument>(Func<TDataType, TMatchArgument, bool> matchHandler, TMatchArgument matchArgument, out int absoluteRowIndex, out int absoluteColumnIndex)
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