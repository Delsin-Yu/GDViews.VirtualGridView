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
                (data, predicate) => predicate(data),
                matchingArgument,
                out rowIndex,
                out columnIndex
            );
    }
}