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
    /// <param name="xIndex">The view x index.</param>
    /// <param name="yIndex">The view y index.</param>
    public bool this[int xIndex, int yIndex] => _backingResolver(_backing[xIndex, yIndex]);

    /// <summary>
    /// The total defined xs of the viewport.
    /// </summary>
    public readonly int ViewXCount;

    /// <summary>
    /// The total defined ys of the viewport.
    /// </summary>
    public readonly int ViewYCount;

    private readonly object[,] _backing;
    private readonly Func<object, bool> _backingResolver;

    internal ReadOnlyViewArray(
        object[,] backing,
        int viewXCount,
        int viewYCount,
        Func<object, bool> backingResolver
    )
    {
        _backing = backing;
        _backingResolver = backingResolver;
        ViewXCount = viewXCount;
        ViewYCount = viewYCount;
    }
}