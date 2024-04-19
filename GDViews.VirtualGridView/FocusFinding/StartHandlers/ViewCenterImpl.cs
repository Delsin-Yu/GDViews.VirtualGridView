using Godot;

namespace GodotViews.VirtualGrid.FocusFinding;

public static partial class StartHandlers
{
    private class ViewCenterImpl : IViewStartHandler<Vector2I>
    {
        public Vector2I ResolveStartPosition(ref readonly ReadOnlyViewArray currentView, Vector2I argument) => new Vector2I(currentView.ViewRows, currentView.ViewColumns) / 2 + argument;
    }
}