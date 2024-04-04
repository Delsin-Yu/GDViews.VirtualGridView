using System;

namespace GodotViews.VirtualGrid;

public interface IPredicateDataFocusFinder
{
    public bool TryResolveFocus<TDataType>(
        ref readonly Predicate<TDataType> predicate,
        ref readonly ReadOnlyDataArray<TDataType> currentView,
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

public interface IDataFocusFinder<TMatchingArgument>
{
    public bool TryResolveFocus<TDataType>(
        ref readonly TMatchingArgument matchingArgument,
        ref readonly ReadOnlyDataArray<TDataType> currentView,
        out int dataSetRowIndex,
        out int dataSetColumnIndex
    );
}

