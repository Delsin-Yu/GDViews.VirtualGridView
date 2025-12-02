using System;
using Godot;

namespace GodotViews.VirtualGrid.FocusFinding;

public static partial class FocusFinders
{
    private class DataBFSImpl : IDataFocusFinder<Vector2I>
    {
        public bool TryResolveFocus<TDataType>(
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            ref readonly ReadOnlySpan<Vector2I> searchDirection,
            IDataStartHandler<Vector2I> dataStartPositionHandler,
            ref readonly Vector2I argument,
            out int dataSetXIndex,
            out int dataSetYIndex)
        {
            var start = dataStartPositionHandler.ResolveStartPosition(in currentView, argument);
            return BFSSearch.BFSCore(
                start,
                currentView,
                searchDirection,
                out dataSetXIndex,
                out dataSetYIndex
            );
        }
    }
}