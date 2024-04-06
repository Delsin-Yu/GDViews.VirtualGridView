using Godot;

namespace GodotViews.Core.FocusFinder;

public static class FocusPresets
{
    public static readonly ViewFocusFinderPreset TopLeft = new(FocusBy.View, StartFrom.ViewPosition, ViewCorner.TopLeft, SearchDirections.RightDown);
    public static readonly ViewFocusFinderPreset TopRight = new(FocusBy.View, StartFrom.ViewPosition, ViewCorner.TopRight, SearchDirections.LeftDown);
    public static readonly ViewFocusFinderPreset BottomLeft = new(FocusBy.View, StartFrom.ViewPosition, ViewCorner.BottomLeft, SearchDirections.RightUp);
    public static readonly ViewFocusFinderPreset BottomRight = new(FocusBy.View, StartFrom.ViewPosition, ViewCorner.BottomRight, SearchDirections.LeftUp);
    public static readonly ViewFocusFinderPreset LeftTop = new(FocusBy.View, StartFrom.ViewPosition, ViewCorner.TopLeft, SearchDirections.DownRight);
    public static readonly ViewFocusFinderPreset RightTop = new(FocusBy.View, StartFrom.ViewPosition, ViewCorner.TopRight, SearchDirections.DownLeft);
    public static readonly ViewFocusFinderPreset LeftBottom = new(FocusBy.View, StartFrom.ViewPosition, ViewCorner.BottomLeft, SearchDirections.UpRight);
    public static readonly ViewFocusFinderPreset RightBottom = new(FocusBy.View, StartFrom.ViewPosition, ViewCorner.BottomRight, SearchDirections.UpLeft);
    public static readonly ViewFocusFinderPreset CenterClockwise = new(FocusBy.View, StartFrom.ViewCenter, Vector2I.Zero, SearchDirections.FourWayClockwise);
    public static readonly ViewFocusFinderPreset CenterAnticlockwise = new(FocusBy.View, StartFrom.ViewCenter, Vector2I.Zero, SearchDirections.FourWayAnticlockwise);
    public static readonly ViewFocusFinderPreset CenterUpDownLeftRight = new(FocusBy.View, StartFrom.ViewCenter, Vector2I.Zero, SearchDirections.UpDownLeftRight);
}