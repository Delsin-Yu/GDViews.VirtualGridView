using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public static partial class ViewFocusFinders
{
    private static Vector2I TopLeftGetStartHandler(ref readonly ReadOnly2DArray view) => Vector2I.Zero;
    private static Vector2I TopRightGetStartHandler(ref readonly ReadOnly2DArray view) => new(0, view.ViewColumns - 1);
    private static Vector2I BottomLeftGetStartHandler(ref readonly ReadOnly2DArray view) => new(view.ViewRows - 1, 0);
    private static Vector2I BottomRightGetStartHandler(ref readonly ReadOnly2DArray view) => new(view.ViewRows - 1, view.ViewColumns - 1);
    private static Vector2I CenterGetStartHandler(ref readonly ReadOnly2DArray currentView) => new Vector2I(currentView.ViewRows, currentView.ViewColumns) / 2;
    
    public static IViewFocusFinder TopLeft { get; } = new BFSViewFocusFinder([new(0, 1), new(1, 0)], TopLeftGetStartHandler);
    public static IViewFocusFinder TopRight { get; } = new BFSViewFocusFinder([new(0, -1), new(1, 0)], TopRightGetStartHandler);
    public static IViewFocusFinder BottomLeft { get; } = new BFSViewFocusFinder([new(0, 1), new(-1, 0)], BottomLeftGetStartHandler);
    public static IViewFocusFinder BottomRight { get; } = new BFSViewFocusFinder([new(0, -1), new(-1, 0)], BottomRightGetStartHandler);
    public static IViewFocusFinder LeftTop { get; } = new BFSViewFocusFinder([new(1, 0), new(0, 1)], TopLeftGetStartHandler);
    public static IViewFocusFinder RightTop { get; } = new BFSViewFocusFinder([new(1, 0), new(0, -1)], TopRightGetStartHandler);
    public static IViewFocusFinder LeftBottom { get; } = new BFSViewFocusFinder([new(-1, 0), new(0, 1)], BottomLeftGetStartHandler);
    public static IViewFocusFinder RightBottom { get; } = new BFSViewFocusFinder([new(-1, 0), new(0, -1)], BottomRightGetStartHandler);
    public static IViewFocusFinder CenterClockwise { get; } = new BFSViewFocusFinder([new(-1, 0), new(0, 1), new(1, 0), new(0, -1)], CenterGetStartHandler);
    public static IViewFocusFinder CenterAnticlockwise { get; } = new BFSViewFocusFinder([new(-1, 0), new(0, -1), new(1, 0), new(0, 1)], CenterGetStartHandler);
    public static IViewFocusFinder CenterUpDownLeftRight { get; } = new BFSViewFocusFinder([new(-1, 0), new(1, 0), new(0, -1), new(0, 1)], CenterGetStartHandler);
    //public static IArgumentViewFocusFinder<Vector2I> FromCellPosition { get; }
}