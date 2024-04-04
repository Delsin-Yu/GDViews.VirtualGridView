namespace GodotViews.Core.FocusFinder;

public static class FocusPresets
{
    public static readonly ViewFocusFinderPreset TopLeft = new(FocusFinders.ByPosition, StartPositions.TopLeft, SearchDirections.RightDown);
    public static readonly ViewFocusFinderPreset TopRight = new(FocusFinders.ByPosition, StartPositions.TopRight, SearchDirections.LeftDown);
    public static readonly ViewFocusFinderPreset BottomLeft = new(FocusFinders.ByPosition, StartPositions.BottomLeft, SearchDirections.RightUp);
    public static readonly ViewFocusFinderPreset BottomRight = new(FocusFinders.ByPosition, StartPositions.BottomRight, SearchDirections.LeftUp);
    public static readonly ViewFocusFinderPreset LeftTop = new(FocusFinders.ByPosition, StartPositions.TopLeft, SearchDirections.DownRight);
    public static readonly ViewFocusFinderPreset RightTop = new(FocusFinders.ByPosition, StartPositions.TopRight, SearchDirections.DownLeft);
    public static readonly ViewFocusFinderPreset LeftBottom = new(FocusFinders.ByPosition, StartPositions.BottomLeft, SearchDirections.UpRight);
    public static readonly ViewFocusFinderPreset RightBottom = new(FocusFinders.ByPosition, StartPositions.BottomRight, SearchDirections.UpLeft);
    public static readonly ViewFocusFinderPreset CenterClockwise = new(FocusFinders.ByPosition, StartPositions.Center, SearchDirections.FourWayClockwise);
    public static readonly ViewFocusFinderPreset CenterAnticlockwise = new(FocusFinders.ByPosition, StartPositions.Center, SearchDirections.FourWayAnticlockwise);
    public static readonly ViewFocusFinderPreset CenterUpDownLeftRight = new(FocusFinders.ByPosition, StartPositions.Center, SearchDirections.UpDownLeftRight);
}