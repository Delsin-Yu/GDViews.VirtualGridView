using System;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public static partial class FocusFiners
{
    private class PredicateDataFocusFinder : IPredicateDataFocusFinder
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