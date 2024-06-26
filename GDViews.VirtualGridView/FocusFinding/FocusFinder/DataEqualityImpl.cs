﻿using System;
using System.Collections.Generic;

namespace GodotViews.VirtualGrid.FocusFinding;

public static partial class FocusFinders
{
    private class DataEqualityImpl : IEqualityDataFocusFinder
    {
        public bool TryResolveFocus<TDataType>(
            ref readonly TDataType matchingArgument,
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            out int dataSetRowIndex,
            out int dataSetColumnIndex
        ) =>
            currentView.TryGetData(
                CachedComparer<TDataType>.EqualsHandler,
                matchingArgument,
                out dataSetRowIndex,
                out dataSetColumnIndex
            );

        private static class CachedComparer<TDataType>
        {
            public static readonly Func<TDataType, TDataType, bool> EqualsHandler =
                EqualityComparer<TDataType>.Default.Equals;
        }
    }
}