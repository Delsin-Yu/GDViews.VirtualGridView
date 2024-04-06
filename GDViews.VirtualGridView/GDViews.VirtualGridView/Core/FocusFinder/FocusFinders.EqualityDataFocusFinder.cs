using System;
using System.Collections.Generic;
using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public static partial class FocusBy
{
    private class EqualityDataFocusFinder : IEqualityDataFocusFinder
    {
        private static class CachedComparer<TDataType>
        {
            public static readonly Func<TDataType, TDataType, bool> EqualsHandler = 
                EqualityComparer<TDataType>.Default.Equals;
        }
        
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
    }
    
        
    private class BFSDataSetFocusFinder : IDataFocusFinder<Vector2I>
    {
        public bool TryResolveFocus<TDataType>(
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            ref readonly ReadOnlySpan<Vector2I> searchDirection,
            DataStartPositionHandler<TDataType, Vector2I> dataStartPositionHandler,
            ref readonly Vector2I argument, 
            out int viewRowIndex,
            out int viewColumnIndex
        )
        {
            var start = dataStartPositionHandler(in currentView, argument);
            return BFSSearch.BFSCore(
                in start,
                in currentView,
                in searchDirection,
                out viewRowIndex,
                out viewColumnIndex
            );
        }
    }
}