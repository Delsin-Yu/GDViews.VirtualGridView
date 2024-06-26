﻿using Godot;

namespace GodotViews.VirtualGrid.FocusFinding;

public static partial class StartHandlers
{
    private class ViewImpl : IViewStartHandler<Vector2I>
    {
        public Vector2I ResolveStartPosition(ref readonly ReadOnlyViewArray currentView, Vector2I position) =>
            new(
                position.X < 0 ? currentView.ViewRows + position.X : position.X,
                position.Y < 0 ? currentView.ViewColumns + position.Y : position.Y
            );
    }
}