using System;
using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Data Focus Finder is the abstraction of the focus finding algorithm that
/// calculates the target focus coordinate based on the elements in the datasets. 
/// </summary>
/// <typeparam name="TArgument">The type of the argument required by the focus finder.</typeparam>
public interface IDataFocusFinder<TArgument>
{
    /// <summary> Try to calculate the target focus coordinate based on the specified arguments. </summary>
    /// <param name="currentView">Provides indirect access to the content of the datasets.</param>
    /// <param name="searchDirection">The search direction from the start position.</param>
    /// <param name="dataStartPositionHandler">The handler responsible for resolving the start position.</param>
    /// <param name="argument">The argument passes to the <see cref="dataStartPositionHandler"/></param>
    /// <param name="dataSetRowIndex">The calculated datasets row index,
    /// unused when the finder fails to obtain the coordinate.</param>
    /// <param name="dataSetColumnIndex">The calculated datasets column index,
    /// unused when the finder fails to obtain the coordinate.</param>
    /// <returns><see langword="true" /> if the finder successfully obtain the coordinate;
    /// otherwise, <see langword="false" />.</returns>
    bool TryResolveFocus<TDataType>(
        ref readonly ReadOnlyDataArray<TDataType> currentView,
        ref readonly ReadOnlySpan<Vector2I> searchDirection,
        IDataStartHandler<TArgument> dataStartPositionHandler,
        ref readonly TArgument argument,
        out int dataSetRowIndex,
        out int dataSetColumnIndex
    );
}

/// <summary>
/// Equality Data Focus Finder is the abstraction of the focus finding algorithm that
/// calculates the target focus coordinate based on matching the elements in the datasets. 
/// </summary>
public interface IEqualityDataFocusFinder
{
    /// <summary>Try to calculate the target focus coordinate based on the specified arguments.</summary>
    /// <param name="matchingArgument">The argument that uses for performing the equality matching.</param>
    /// <param name="currentView">Provides indirect access to the content of the datasets.</param>
    /// <param name="dataSetRowIndex">The calculated datasets row index,
    /// unused when the finder fails to obtain the coordinate.</param>
    /// <param name="dataSetColumnIndex">The calculated datasets column index,
    /// unused when the finder fails to obtain the coordinate.</param>
    /// <typeparam name="TDataType">The type for the data this handler focuses on.</typeparam>
    /// <returns><see langword="true" /> if the finder successfully obtain the coordinate;
    /// otherwise, <see langword="false" />.</returns>
    public bool TryResolveFocus<TDataType>(
        ref readonly TDataType matchingArgument,
        ref readonly ReadOnlyDataArray<TDataType> currentView,
        out int dataSetRowIndex,
        out int dataSetColumnIndex
    );
}

/// <summary>
/// Predicate Data Focus Finder is the abstraction of the focus finding algorithm that
/// calculates the target focus coordinate based on the matching the elements
/// by the custom predicate in the datasets. 
/// </summary>
public interface IPredicateDataFocusFinder
{
    /// <summary>
    /// <summary>Try to calculate the target focus coordinate based on the specified arguments.</summary>
    /// </summary>
    /// <param name="predicate">The predicate passes that uses for performing the matching.</param>
    /// <param name="currentView">Provides indirect access to the content of the datasets.</param>
    /// <param name="dataSetRowIndex">The calculated datasets row index,
    /// unused when the finder fails to obtain the coordinate.</param>
    /// <param name="dataSetColumnIndex">The calculated datasets column index,
    /// unused when the finder fails to obtain the coordinate.</param>
    /// <typeparam name="TDataType">The type for the data this handler focuses on.</typeparam>
    /// <returns><see langword="true" /> if the finder successfully obtain the coordinate;
    /// otherwise, <see langword="false" />.</returns>
    public bool TryResolveFocus<TDataType>(
        ref readonly Predicate<TDataType> predicate,
        ref readonly ReadOnlyDataArray<TDataType> currentView,
        out int dataSetRowIndex,
        out int dataSetColumnIndex
    );

    /// <summary>
    /// <summary>Try to calculate the target focus coordinate based on the specified arguments.</summary>
    /// </summary>
    /// <param name="predicate">The predicate passes that uses for performing the matching.</param>
    /// <param name="currentView">Provides indirect access to the content of the datasets.</param>
    /// <param name="extraArgument">The predicate passes to the <paramref name="predicate"/> to avoid closure allocation.</param>
    /// <param name="dataSetRowIndex">The calculated datasets row index,
    /// unused when the finder fails to obtain the coordinate.</param>
    /// <param name="dataSetColumnIndex">The calculated datasets column index,
    /// unused when the finder fails to obtain the coordinate.</param>
    /// <typeparam name="TDataType">The type for the data this handler focuses on.</typeparam>
    /// <typeparam name="TExtraArgument">The type of the argument required by the focus finder.</typeparam>
    /// <returns><see langword="true" /> if the finder successfully obtain the coordinate;
    /// otherwise, <see langword="false" />.</returns>
    public bool TryResolveFocus<TDataType, TExtraArgument>(
        ref readonly Func<TDataType, TExtraArgument, bool> predicate,
        ref readonly ReadOnlyDataArray<TDataType> currentView,
        in TExtraArgument extraArgument,
        out int dataSetRowIndex,
        out int dataSetColumnIndex
    );
}