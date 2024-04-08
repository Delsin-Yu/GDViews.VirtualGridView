using System;
using Godot;

namespace GodotViews.VirtualGrid;

public interface IPredicateDataFocusFinder
{
    public bool TryResolveFocus<TDataType>(
        ref readonly Predicate<TDataType> predicate,
        ref readonly ReadOnlyDataArray<TDataType> currentView,
        out int rowIndex,
        out int columnIndex
    );

    public bool TryResolveFocus<TDataType, TExtraArgument>(
        ref readonly Func<TDataType, TExtraArgument, bool> predicate,
        ref readonly ReadOnlyDataArray<TDataType> currentView,
        in TExtraArgument extraArgument,
        out int rowIndex,
        out int columnIndex
    );
}

public interface IEqualityDataFocusFinder
{
    public bool TryResolveFocus<TDataType>(
        ref readonly TDataType matchingArgument,
        ref readonly ReadOnlyDataArray<TDataType> currentView,
        out int dataSetRowIndex,
        out int dataSetColumnIndex
    );
}

public interface IDataFocusFinder<TArgument>
{
    bool TryResolveFocus<TDataType>(
        ref readonly ReadOnlyDataArray<TDataType> currentView,
        ref readonly ReadOnlySpan<Vector2I> searchDirection,
        IDataStartHandler<TArgument> dataStartPositionHandler,
        ref readonly TArgument argument,
        out int viewRowIndex,
        out int viewColumnIndex
    );
}