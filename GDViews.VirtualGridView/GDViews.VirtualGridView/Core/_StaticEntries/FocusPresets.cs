using Godot;

namespace GodotViews.VirtualGrid;

public static class FocusPresets
{
    public static readonly DataFocusFinderPreset<Vector2I> DataPosition = new(FocusFiners.DataPosition, StartHandlers.DataPosition);
    public static readonly DataFocusFinderPreset TopLeftData = new(DataPosition, ViewCorners.TopLeft, SearchDirections.RightDown);
    public static readonly DataFocusFinderPreset TopRightData = new(DataPosition, ViewCorners.TopRight, SearchDirections.LeftDown);
    public static readonly DataFocusFinderPreset BottomLeftData = new(DataPosition, ViewCorners.BottomLeft, SearchDirections.RightUp);
    public static readonly DataFocusFinderPreset BottomRightData = new(DataPosition, ViewCorners.BottomRight, SearchDirections.LeftUp);
    public static readonly DataFocusFinderPreset LeftTopData = new(DataPosition, ViewCorners.TopLeft, SearchDirections.DownRight);
    public static readonly DataFocusFinderPreset RightTopData = new(DataPosition, ViewCorners.TopRight, SearchDirections.DownLeft);
    public static readonly DataFocusFinderPreset LeftBottomData = new(DataPosition, ViewCorners.BottomLeft, SearchDirections.UpRight);
    public static readonly DataFocusFinderPreset RightBottomData = new(DataPosition, ViewCorners.BottomRight, SearchDirections.UpLeft);

    public static readonly ViewFocusFinderPreset<Vector2I> ViewPosition = new(FocusFiners.ViewPosition, StartHandlers.ViewPosition);
    public static readonly ViewFocusFinderPreset<Vector2I> ViewCenter = new(FocusFiners.ViewPosition, StartHandlers.ViewCenter);
    public static readonly ViewFocusFinderPreset TopLeftView = new(ViewPosition, ViewCorners.TopLeft, SearchDirections.RightDown);
    public static readonly ViewFocusFinderPreset TopRightView = new(ViewPosition, ViewCorners.TopRight, SearchDirections.LeftDown);
    public static readonly ViewFocusFinderPreset BottomLeftView = new(ViewPosition, ViewCorners.BottomLeft, SearchDirections.RightUp);
    public static readonly ViewFocusFinderPreset BottomRightView = new(ViewPosition, ViewCorners.BottomRight, SearchDirections.LeftUp);
    public static readonly ViewFocusFinderPreset LeftTopView = new(ViewPosition, ViewCorners.TopLeft, SearchDirections.DownRight);
    public static readonly ViewFocusFinderPreset RightTopView = new(ViewPosition, ViewCorners.TopRight, SearchDirections.DownLeft);
    public static readonly ViewFocusFinderPreset LeftBottomView = new(ViewPosition, ViewCorners.BottomLeft, SearchDirections.UpRight);
    public static readonly ViewFocusFinderPreset RightBottomView = new(ViewPosition, ViewCorners.BottomRight, SearchDirections.UpLeft);
    public static readonly ViewFocusFinderPreset CenterClockwiseView = new(ViewCenter, Vector2I.Zero, SearchDirections.FourWayClockwise);
    public static readonly ViewFocusFinderPreset CenterAnticlockwiseView = new(ViewCenter, Vector2I.Zero, SearchDirections.FourWayAnticlockwise);
    public static readonly ViewFocusFinderPreset CenterUpDownLeftRightView = new(ViewCenter, Vector2I.Zero, SearchDirections.UpDownLeftRight);
    public static IEqualityDataFocusFinder Value => FocusFiners.Value;
    public static IPredicateDataFocusFinder Predicate => FocusFiners.Predicate;
}