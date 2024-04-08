using System;

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