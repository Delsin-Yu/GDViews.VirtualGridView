using System;
using Godot;

namespace GodotViews.VirtualGrid;

public static partial class FocusFinders
{
    private class DataBFSFocusFinder : IDataFocusFinder<Vector2I>
    {
        public bool TryResolveFocus<TDataType>(
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            ref readonly ReadOnlySpan<Vector2I> searchDirection,
            IDataStartHandler<Vector2I> dataStartPositionHandler,
            ref readonly Vector2I argument,
            out int dataSetRowIndex,
            out int dataSetColumnIndex
        )
        {
            var start = dataStartPositionHandler.ResolveStartPosition(in currentView, argument);
            return BFSSearch.BFSCore(
                in start,
                in currentView,
                in searchDirection,
                out dataSetRowIndex,
                out dataSetColumnIndex
            );
        }
    }
}