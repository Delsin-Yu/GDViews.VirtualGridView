using System;
using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// View Focus Finder is the abstraction of the focus finding algorithm that
/// calculates the target focus coordinate based on the elements in the current viewport. 
/// </summary>
/// <typeparam name="TArgument">The type of the argument required by the focus finder.</typeparam>
public interface IViewFocusFinder<TArgument>
{
    /// <summary> Try to calculate the target focus coordinate based on the specified arguments. </summary>
    /// <param name="currentView">Provides indirect access to the populated state of the current viewport.</param>
    /// <param name="searchDirection">The search direction from the start position.</param>
    /// <param name="viewStartPositionHandler">The handler responsible for resolving the start position.</param>
    /// <param name="argument">The argument passes to the <paramref name="viewStartPositionHandler"/></param>
    /// <param name="viewRowIndex">The calculated target row index relative to the viewport,
    /// unused when the finder fails to obtain the coordinate.</param>
    /// <param name="viewColumnIndex">The calculated target column index relative to the viewport,
    /// unused when the finder fails to obtain the coordinate.</param>
    /// <returns><see langword="true" /> if the finder successfully obtain the coordinate;
    /// otherwise, <see langword="false" />.</returns>
    public bool TryResolveFocus(
        ref readonly ReadOnlyViewArray currentView,
        ref readonly ReadOnlySpan<Vector2I> searchDirection,
        IViewStartHandler<TArgument> viewStartPositionHandler,
        ref readonly TArgument argument,
        out int viewRowIndex,
        out int viewColumnIndex
    );
}