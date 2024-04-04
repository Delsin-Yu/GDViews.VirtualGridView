using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public static class StartPositions
{
    private static Vector2I TopLeftHandler(ref readonly ReadOnlyViewArray view) => Vector2I.Zero;
    private static Vector2I TopRightHandler(ref readonly ReadOnlyViewArray view) => new(0, view.ViewColumns - 1);
    private static Vector2I BottomLeftHandler(ref readonly ReadOnlyViewArray view) => new(view.ViewRows - 1, 0);
    private static Vector2I BottomRightHandler(ref readonly ReadOnlyViewArray view) => new(view.ViewRows - 1, view.ViewColumns - 1);
    private static Vector2I CenterHandler(ref readonly ReadOnlyViewArray currentView) => new Vector2I(currentView.ViewRows, currentView.ViewColumns) / 2;

    public static StartPositionHandler TopLeft { get; } = TopLeftHandler;
    public static StartPositionHandler TopRight { get; } = TopRightHandler;
    public static StartPositionHandler BottomLeft { get; } = BottomLeftHandler;
    public static StartPositionHandler BottomRight { get; } = BottomRightHandler;
    public static StartPositionHandler Center { get; } = CenterHandler;
}