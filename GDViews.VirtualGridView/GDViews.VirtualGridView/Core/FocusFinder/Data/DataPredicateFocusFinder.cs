using System;

namespace GodotViews.VirtualGrid;

public static partial class FocusFiners
{
    private class DataPredicateFocusFinder : IPredicateDataFocusFinder
    {
        public bool TryResolveFocus<TDataType>(
            ref readonly Predicate<TDataType> matchingArgument,
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            out int rowIndex,
            out int columnIndex
        ) =>
            currentView.TryGetData(
                static (data, predicate) => predicate(data),
                matchingArgument,
                out rowIndex,
                out columnIndex
            );

        public bool TryResolveFocus<TDataType, TExtraArgument>(
            ref readonly Func<TDataType, TExtraArgument, bool> predicate,
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            in TExtraArgument extraArgument,
            out int rowIndex,
            out int columnIndex
        ) =>
            currentView.TryGetData(
                static (data, composite) => composite.predicate(data, composite.extraArgument),
                (predicate, extraArgument),
                out rowIndex,
                out columnIndex
            );
    }
}