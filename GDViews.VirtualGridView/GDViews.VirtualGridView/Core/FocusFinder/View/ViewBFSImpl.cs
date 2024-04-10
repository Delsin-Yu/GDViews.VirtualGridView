using System;
using Godot;

namespace GodotViews.VirtualGrid;

public static partial class FocusFinders
{
    private class ViewBFSImpl : IViewFocusFinder<Vector2I>
    {
        public bool TryResolveFocus(
            ref readonly ReadOnlyViewArray currentView,
            ref readonly ReadOnlySpan<Vector2I> searchDirection,
            IViewStartHandler<Vector2I> viewStartPositionHandler,
            ref readonly Vector2I argument,
            out int viewRowIndex,
            out int viewColumnIndex
        )
        {
            var start = viewStartPositionHandler.ResolveStartPosition(in currentView, argument);
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