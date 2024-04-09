using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a set of predefined <see cref="SearchDirection"/> for indicating the search directions
/// to be used with <see cref="FocusPresets"/> or <see cref="FocusFinders"/>.
/// </summary>
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


    /// <summary> Instruct the focus handler to search toward the FourWayClockwise side. </summary>
    public static readonly SearchDirection FourWayClockwise = new(_fourWayClockwise);

    /// <summary> Instruct the focus handler to search toward the FourWayAnticlockwise side. </summary>
    public static readonly SearchDirection FourWayAnticlockwise = new(_fourWayAnticlockwise);

    /// <summary> Instruct the focus handler to search toward the UpDownLeftRight side. </summary>
    public static readonly SearchDirection UpDownLeftRight = new(_forWayUpDownLeftRight);

    /// <summary> Instruct the focus handler to search toward the RightDown side. </summary>
    public static readonly SearchDirection RightDown = new(_rightDown);

    /// <summary> Instruct the focus handler to search toward the LeftDown side. </summary>
    public static readonly SearchDirection LeftDown = new(_leftDown);

    /// <summary> Instruct the focus handler to search toward the RightUp side. </summary>
    public static readonly SearchDirection RightUp = new(_rightUp);

    /// <summary> Instruct the focus handler to search toward the LeftUp side. </summary>
    public static readonly SearchDirection LeftUp = new(_leftUp);

    /// <summary> Instruct the focus handler to search toward the DownRight side. </summary>
    public static readonly SearchDirection DownRight = new(_downRight);

    /// <summary> Instruct the focus handler to search toward the DownLeft side. </summary>
    public static readonly SearchDirection DownLeft = new(_downLeft);

    /// <summary> Instruct the focus handler to search toward the UpRight side. </summary>
    public static readonly SearchDirection UpRight = new(_upRight);

    /// <summary> Instruct the focus handler to search toward the UpLeft side. </summary>
    public static readonly SearchDirection UpLeft = new(_upLeft);

    /// <summary> Instruct the focus handler to search toward the Right side. </summary>
    public static readonly SearchDirection Right = new(_oneWayRight);

    /// <summary> Instruct the focus handler to search toward the Down side. </summary>
    public static readonly SearchDirection Down = new(_oneWayDown);

    /// <summary> Instruct the focus handler to search toward the Left side. </summary>
    public static readonly SearchDirection Left = new(_oneWayLeft);

    /// <summary> Instruct the focus handler to search toward the Up side. </summary>
    public static readonly SearchDirection Up = new(_oneWayUp);
}