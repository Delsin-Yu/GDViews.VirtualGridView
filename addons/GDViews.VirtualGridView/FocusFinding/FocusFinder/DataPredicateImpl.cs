using System;

namespace GodotViews.VirtualGrid.FocusFinding;

public static partial class FocusFinders
{
    private class DataPredicateImpl : IPredicateDataFocusFinder
    {
        public bool TryResolveFocus<TDataType>(
            ref readonly Predicate<TDataType> matchingArgument,
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            out int dataSetRowIndex,
            out int dataSetColumnIndex
        ) =>
            currentView.TryGetData(
                static (data, predicate) => predicate(data),
                matchingArgument,
                out dataSetRowIndex,
                out dataSetColumnIndex
            );

        public bool TryResolveFocus<TDataType, TExtraArgument>(
            ref readonly Func<TDataType, TExtraArgument, bool> predicate,
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            in TExtraArgument extraArgument,
            out int dataSetRowIndex,
            out int dataSetColumnIndex
        ) =>
            currentView.TryGetData(
                static (data, composite) => composite.predicate(data, composite.extraArgument),
                (predicate, extraArgument),
                out dataSetRowIndex,
                out dataSetColumnIndex
            );
    }
}