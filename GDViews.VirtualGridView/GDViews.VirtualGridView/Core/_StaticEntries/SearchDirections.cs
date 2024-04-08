using Godot;

namespace GodotViews.VirtualGrid;

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


    public static readonly SearchDirection FourWayClockwise = new(_fourWayClockwise);
    public static readonly SearchDirection FourWayAnticlockwise = new(_fourWayAnticlockwise);
    public static readonly SearchDirection UpDownLeftRight = new(_forWayUpDownLeftRight);
    public static readonly SearchDirection RightDown = new(_rightDown);
    public static readonly SearchDirection LeftDown = new(_leftDown);
    public static readonly SearchDirection RightUp = new(_rightUp);
    public static readonly SearchDirection LeftUp = new(_leftUp);
    public static readonly SearchDirection DownRight = new(_downRight);
    public static readonly SearchDirection DownLeft = new(_downLeft);
    public static readonly SearchDirection UpRight = new(_upRight);
    public static readonly SearchDirection UpLeft = new(_upLeft);
    public static readonly SearchDirection Right = new(_oneWayRight);
    public static readonly SearchDirection Down = new(_oneWayDown);
    public static readonly SearchDirection Left = new(_oneWayLeft);
    public static readonly SearchDirection Up = new(_oneWayUp);
}