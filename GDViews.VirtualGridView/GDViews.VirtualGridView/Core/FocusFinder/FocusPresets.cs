using Godot;

namespace GodotViews.VirtualGrid;

public static class ViewCorner
{
    public static readonly Vector2I TopLeft = Vector2I.Zero;
    public static readonly Vector2I TopRight = new(0, -1);
    public static readonly Vector2I BottomLeft = new(-1, 0);
    public static readonly Vector2I BottomRight = new(-1, -1);
}

public static class FocusPresets
{
    public static readonly DataFocusFinderPreset<Vector2I> DataPosition = new(FocusFiners.DataPosition, StartHandlers.DataPosition);
    public static readonly DataFocusFinderPreset TopLeftData = new(DataPosition, ViewCorner.TopLeft, SearchDirections.RightDown);
    public static readonly DataFocusFinderPreset TopRightData = new(DataPosition, ViewCorner.TopRight, SearchDirections.LeftDown);
    public static readonly DataFocusFinderPreset BottomLeftData = new(DataPosition, ViewCorner.BottomLeft, SearchDirections.RightUp);
    public static readonly DataFocusFinderPreset BottomRightData = new(DataPosition, ViewCorner.BottomRight, SearchDirections.LeftUp);
    public static readonly DataFocusFinderPreset LeftTopData = new(DataPosition, ViewCorner.TopLeft, SearchDirections.DownRight);
    public static readonly DataFocusFinderPreset RightTopData = new(DataPosition, ViewCorner.TopRight, SearchDirections.DownLeft);
    public static readonly DataFocusFinderPreset LeftBottomData = new(DataPosition, ViewCorner.BottomLeft, SearchDirections.UpRight);
    public static readonly DataFocusFinderPreset RightBottomData = new(DataPosition, ViewCorner.BottomRight, SearchDirections.UpLeft);
    
    public static readonly ViewFocusFinderPreset<Vector2I> ViewPosition = new(FocusFiners.ViewPosition, StartHandlers.ViewPosition);
    public static readonly ViewFocusFinderPreset<Vector2I> ViewCenter = new(FocusFiners.ViewPosition, StartHandlers.ViewCenter);
    public static readonly ViewFocusFinderPreset TopLeftView = new(ViewPosition, ViewCorner.TopLeft, SearchDirections.RightDown);
    public static readonly ViewFocusFinderPreset TopRightView = new(ViewPosition, ViewCorner.TopRight, SearchDirections.LeftDown);
    public static readonly ViewFocusFinderPreset BottomLeftView = new(ViewPosition, ViewCorner.BottomLeft, SearchDirections.RightUp);
    public static readonly ViewFocusFinderPreset BottomRightView = new(ViewPosition, ViewCorner.BottomRight, SearchDirections.LeftUp);
    public static readonly ViewFocusFinderPreset LeftTopView = new(ViewPosition, ViewCorner.TopLeft, SearchDirections.DownRight);
    public static readonly ViewFocusFinderPreset RightTopView = new(ViewPosition, ViewCorner.TopRight, SearchDirections.DownLeft);
    public static readonly ViewFocusFinderPreset LeftBottomView = new(ViewPosition, ViewCorner.BottomLeft, SearchDirections.UpRight);
    public static readonly ViewFocusFinderPreset RightBottomView = new(ViewPosition, ViewCorner.BottomRight, SearchDirections.UpLeft);
    public static readonly ViewFocusFinderPreset CenterClockwiseView = new(ViewCenter, Vector2I.Zero, SearchDirections.FourWayClockwise);
    public static readonly ViewFocusFinderPreset CenterAnticlockwiseView = new(ViewCenter, Vector2I.Zero, SearchDirections.FourWayAnticlockwise);
    public static readonly ViewFocusFinderPreset CenterUpDownLeftRightView = new(ViewCenter, Vector2I.Zero, SearchDirections.UpDownLeftRight);
}