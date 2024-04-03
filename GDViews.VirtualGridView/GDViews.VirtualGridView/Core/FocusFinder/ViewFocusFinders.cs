using System;
using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public readonly struct SearchDirection
{
    private readonly Vector2I[] _backing;

    internal SearchDirection(Vector2I[] backing)
    {
        _backing = backing;
    }

    internal ReadOnlySpan<Vector2I> GetSpan() => _backing.AsSpan();
}

public static partial class SearchDirections
{
    private static readonly Vector2I SearchRight = new(0, 1);
    private static readonly Vector2I SearchDown = new(1, 0);
    private static readonly Vector2I SearchLeft = new(0, -1);
    private static readonly Vector2I SearchUp = new(-1, 0);

    private static readonly Vector2I[] _fourWayClockwise = [SearchUp, SearchRight, SearchDown, SearchLeft];
    private static readonly Vector2I[] _fourWayAnticlockwise = [SearchUp, SearchLeft, SearchDown, SearchRight];
    private static readonly Vector2I[] _forWayUpDownLeftRight = [SearchUp, SearchDown, SearchLeft, SearchRight];

    private static readonly Vector2I[] _oneWayRight = [SearchRight];
    private static readonly Vector2I[] _oneWayDown = [SearchDown];
    private static readonly Vector2I[] _oneWayLeft = [SearchLeft];
    private static readonly Vector2I[] _oneWayUp = [SearchUp];

    private static readonly Vector2I[] _rightDown = [SearchRight, SearchDown];
    private static readonly Vector2I[] _leftDown = [SearchLeft, SearchDown];
    private static readonly Vector2I[] _rightUp = [SearchRight, SearchUp];
    private static readonly Vector2I[] _leftUp = [SearchLeft, SearchUp];
    private static readonly Vector2I[] _downRight = [SearchDown, SearchRight];
    private static readonly Vector2I[] _downLeft = [SearchDown, SearchLeft];
    private static readonly Vector2I[] _upRight = [SearchUp, SearchRight];
    private static readonly Vector2I[] _upLeft = [SearchUp, SearchLeft];


    public static SearchDirection FourWayClockwise { get; } = new(_fourWayClockwise);
    public static SearchDirection FourWayAnticlockwise { get; } = new(_fourWayAnticlockwise);
    public static SearchDirection UpDownLeftRight { get; } = new(_forWayUpDownLeftRight);
    public static SearchDirection RightDown { get; } = new(_rightDown);
    public static SearchDirection LeftDown { get; } = new(_leftDown);
    public static SearchDirection RightUp { get; } = new(_rightUp);
    public static SearchDirection LeftUp { get; } = new(_leftUp);
    public static SearchDirection DownRight { get; } = new(_downRight);
    public static SearchDirection DownLeft { get; } = new(_downLeft);
    public static SearchDirection UpRight { get; } = new(_upRight);
    public static SearchDirection UpLeft { get; } = new(_upLeft);
    public static SearchDirection Right { get; } = new(_oneWayRight);
    public static SearchDirection Down { get; } = new(_oneWayDown);
    public static SearchDirection Left { get; } = new(_oneWayLeft);
    public static SearchDirection Up { get; } = new(_oneWayUp);
}

public static class StartPositions
{
    private static Vector2I TopLeftHandler(ref readonly ReadOnly2DArray view) => Vector2I.Zero;
    private static Vector2I TopRightHandler(ref readonly ReadOnly2DArray view) => new(0, view.ViewColumns - 1);
    private static Vector2I BottomLeftHandler(ref readonly ReadOnly2DArray view) => new(view.ViewRows - 1, 0);
    private static Vector2I BottomRightHandler(ref readonly ReadOnly2DArray view) => new(view.ViewRows - 1, view.ViewColumns - 1);
    private static Vector2I CenterHandler(ref readonly ReadOnly2DArray currentView) => new Vector2I(currentView.ViewRows, currentView.ViewColumns) / 2;

    public static StartPositionHandler TopLeft { get; } = TopLeftHandler;
    public static StartPositionHandler TopRight { get; } = TopRightHandler;
    public static StartPositionHandler BottomLeft { get; } = BottomLeftHandler;
    public static StartPositionHandler BottomRight { get; } = BottomRightHandler;
    public static StartPositionHandler Center { get; } = CenterHandler;
}

public readonly struct ViewFocusFinderPreset(IViewFocusFinder ViewFocusFinder, StartPositionHandler StartPosition, SearchDirection SearchDirection)
{
    public readonly IViewFocusFinder ViewFocusFinder = ViewFocusFinder;
    public readonly StartPositionHandler StartPosition = StartPosition;
    public readonly SearchDirection SearchDirection = SearchDirection;
}

public static class ViewFocusFinderPresets
{
    public static readonly ViewFocusFinderPreset TopLeft = new(ViewFocusFinders.ByPosition, StartPositions.TopLeft, SearchDirections.RightDown);
    public static readonly ViewFocusFinderPreset TopRight = new(ViewFocusFinders.ByPosition, StartPositions.TopRight, SearchDirections.LeftDown);
    public static readonly ViewFocusFinderPreset BottomLeft = new(ViewFocusFinders.ByPosition, StartPositions.BottomLeft, SearchDirections.RightUp);
    public static readonly ViewFocusFinderPreset BottomRight = new(ViewFocusFinders.ByPosition, StartPositions.BottomRight, SearchDirections.LeftUp);
    public static readonly ViewFocusFinderPreset LeftTop = new(ViewFocusFinders.ByPosition, StartPositions.TopLeft, SearchDirections.DownRight);
    public static readonly ViewFocusFinderPreset RightTop = new(ViewFocusFinders.ByPosition, StartPositions.TopRight, SearchDirections.DownLeft);
    public static readonly ViewFocusFinderPreset LeftBottom = new(ViewFocusFinders.ByPosition, StartPositions.BottomLeft, SearchDirections.UpRight);
    public static readonly ViewFocusFinderPreset RightBottom = new(ViewFocusFinders.ByPosition, StartPositions.BottomRight, SearchDirections.UpLeft);
    public static readonly ViewFocusFinderPreset CenterClockwise = new(ViewFocusFinders.ByPosition, StartPositions.Center, SearchDirections.FourWayClockwise);
    public static readonly ViewFocusFinderPreset CenterAnticlockwise = new(ViewFocusFinders.ByPosition, StartPositions.Center, SearchDirections.FourWayAnticlockwise);
    public static readonly ViewFocusFinderPreset CenterUpDownLeftRight = new(ViewFocusFinders.ByPosition, StartPositions.Center, SearchDirections.UpDownLeftRight);
}

public static partial class ViewFocusFinders
{
    public static IViewFocusFinder ByPosition { get; } = new BFSViewFocusFinder();
}