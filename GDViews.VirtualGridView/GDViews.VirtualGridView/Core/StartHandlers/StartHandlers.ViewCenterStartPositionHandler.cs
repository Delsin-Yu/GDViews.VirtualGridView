using Godot;

namespace GodotViews.VirtualGrid;

public static partial class StartHandlers
{
    private class ViewCenterStartPositionHandler : IViewStartHandler<Vector2I>
    {
        public Vector2I ResolveStartPosition(ref readonly ReadOnlyViewArray currentView, Vector2I argument) => new Vector2I(currentView.ViewRows, currentView.ViewColumns) / 2 + argument;
    }
}