using System;
using Godot;

namespace GodotViews.VirtualGrid.FocusFinding;

public static partial class FocusFinders
{
    private class ViewBFSImpl : IViewFocusFinder<Vector2I>
    {
        public bool TryResolveFocus(
            ref readonly ReadOnlyViewArray currentView,
            ref readonly ReadOnlySpan<Vector2I> searchDirection,
            IViewStartHandler<Vector2I> viewStartPositionHandler,
            ref readonly Vector2I argument,
            out int viewXIndex,
            out int viewYIndex)
        {
            var start = viewStartPositionHandler.ResolveStartPosition(in currentView, argument);
            return BFSSearch.BFSCore(
                start,
                currentView,
                searchDirection,
                out viewXIndex,
                out viewYIndex
            );
        }
    }
}