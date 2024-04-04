using System;
using Godot;
using GodotViews.Core.FocusFinder;

namespace GodotViews.VirtualGrid;

public interface IViewFocusFinder
{
    public bool TryResolveFocus(
        ref readonly ReadOnlyViewArray currentView,
        ref readonly ReadOnlySpan<Vector2I> searchDirection,
        StartPositionHandler startPositionHandler,
        out int viewRowIndex,
        out int viewColumnIndex
    );
}