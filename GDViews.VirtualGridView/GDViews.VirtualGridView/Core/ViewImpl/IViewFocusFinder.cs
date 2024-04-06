using System;
using Godot;
using GodotViews.Core.FocusFinder;

namespace GodotViews.VirtualGrid;

public interface IViewFocusFinder<TArgument>
{
    public bool TryResolveFocus(
        ref readonly ReadOnlyViewArray currentView,
        ref readonly ReadOnlySpan<Vector2I> searchDirection,
        IViewStartHandler<TArgument> viewStartPositionHandler,
        ref readonly TArgument argument,
        out int viewRowIndex,
        out int viewColumnIndex
    );
}