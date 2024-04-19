using System;

namespace GodotViews.VirtualGrid.FocusFinding;

/// <summary>
/// Allow the developer to indirectly access the populate state of the current viewport.
/// </summary>
public readonly struct ReadOnlyViewArray
{
    /// <summary>
    /// Check if the cell at the given position has populated.
    /// </summary>
    /// <param name="columnIndex">The view column index.</param>
    /// <param name="rowIndex">The view row index.</param>
    public bool this[int columnIndex, int rowIndex] => _backingResolver(_backing[columnIndex, rowIndex]);
    
    /// <summary>
    /// The total defined rows of the viewport.
    /// </summary>
    public readonly int ViewRows;
    
    /// <summary>
    /// The total defined columns of the viewport.
    /// </summary>
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