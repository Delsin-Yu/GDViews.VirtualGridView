using System;
using Godot;

namespace GodotViews.VirtualGrid;

public readonly struct SearchDirection
{
    private readonly Vector2I[] _backing;

    internal SearchDirection(Vector2I[] backing)
    {
        _backing = backing;
    }

    internal ReadOnlySpan<Vector2I> GetSpan() => _backing.AsSpan();
}

public static class SearchDirections
{
    internal static readonly Vector2I SearchRight = new(0, 1);
    internal static readonly Vector2I SearchDown = new(1, 0);
    internal static readonly Vector2I SearchLeft = new(0, -1);
    internal static readonly Vector2I SearchUp = new(-1, 0);

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