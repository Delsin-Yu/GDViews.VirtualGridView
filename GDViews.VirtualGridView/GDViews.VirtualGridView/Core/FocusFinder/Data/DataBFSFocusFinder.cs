using System;
using Godot;

namespace GodotViews.VirtualGrid;

public static partial class FocusFiners
{
    private class DataBFSFocusFinder : IDataFocusFinder<Vector2I>
    {
        public bool TryResolveFocus<TDataType>(
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            ref readonly ReadOnlySpan<Vector2I> searchDirection,
            IDataStartHandler<Vector2I> dataStartPositionHandler,
            ref readonly Vector2I argument, 
            out int viewRowIndex,
            out int viewColumnIndex
        )
        {
            var start = dataStartPositionHandler.ResolveStartPosition(in currentView, argument);
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