using Godot;
using GodotViews.VirtualGrid.FocusFinding;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a set of predefined focus presets that cover common grab focus requirements.
/// </summary>
public static class FocusPresets
{
    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus by <see cref="FocusFinders.DataPosition"/>,
    /// the developer need to specify the start <see cref="Corners"/> and <see cref="SearchDirections"/> in addition.
    /// </summary>
    public static readonly DataFocusFinderPreset<Vector2I> DataPosition = new(FocusFinders.DataPosition, StartHandlers.DataPosition);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus by <see cref="FocusFinders.Value"/>,
    /// the developer need to provide a value for matching against each data from the datasets.
    /// </summary>
    public static readonly IEqualityDataFocusFinder Value = FocusFinders.Value;
    
    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus by <see cref="FocusFinders.Predicate"/>,
    /// the developer need to provide a predicate for matching against each data from the datasets.
    /// </summary>   
    public static readonly IPredicateDataFocusFinder Predicate = FocusFinders.Predicate;
    
    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus by <see cref="FocusFinders.ViewPosition"/>,
    /// the developer need to specify the start <see cref="Corners"/> and <see cref="SearchDirections"/> in addition.
    /// </summary>
    public static readonly ViewFocusFinderPreset<Vector2I> ViewPosition = new(FocusFinders.ViewPosition, StartHandlers.ViewPosition);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus by <see cref="StartHandlers.ViewCenter"/>,
    /// the developer need to specify the offset and <see cref="SearchDirections"/> in addition.
    /// </summary>
    public static readonly ViewFocusFinderPreset<Vector2I> ViewCenter = new(FocusFinders.ViewPosition, StartHandlers.ViewCenter);


    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available data
    /// starting from the <see cref="Corners.TopLeft"/> corner of the datasets towards the <see cref="SearchDirections.RightDown"/> search direction.
    /// </summary>
    public static readonly DataFocusFinderPreset TopLeftData = new(DataPosition, Corners.TopLeft, SearchDirections.RightDown);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available data
    /// starting from the <see cref="Corners.TopRight"/> corner of the datasets towards the <see cref="SearchDirections.LeftDown"/> search direction.
    /// </summary>
    public static readonly DataFocusFinderPreset TopRightData = new(DataPosition, Corners.TopRight, SearchDirections.LeftDown);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available data
    /// starting from the <see cref="Corners.BottomLeft"/> corner of the datasets towards the <see cref="SearchDirections.RightUp"/> search direction.
    /// </summary>
    public static readonly DataFocusFinderPreset BottomLeftData = new(DataPosition, Corners.BottomLeft, SearchDirections.RightUp);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available data
    /// starting from the <see cref="Corners.BottomRight"/> corner of the datasets towards the <see cref="SearchDirections.LeftUp"/> search direction.
    /// </summary>
    public static readonly DataFocusFinderPreset BottomRightData = new(DataPosition, Corners.BottomRight, SearchDirections.LeftUp);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available data
    /// starting from the <see cref="Corners.TopLeft"/> corner of the datasets towards the <see cref="SearchDirections.DownRight"/> search direction.
    /// </summary>
    public static readonly DataFocusFinderPreset LeftTopData = new(DataPosition, Corners.TopLeft, SearchDirections.DownRight);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available data
    /// starting from the <see cref="Corners.TopRight"/> corner of the datasets towards the <see cref="SearchDirections.DownLeft"/> search direction.
    /// </summary>
    public static readonly DataFocusFinderPreset RightTopData = new(DataPosition, Corners.TopRight, SearchDirections.DownLeft);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available data
    /// starting from the <see cref="Corners.BottomLeft"/> corner of the datasets towards the <see cref="SearchDirections.UpRight"/> search direction.
    /// </summary>
    public static readonly DataFocusFinderPreset LeftBottomData = new(DataPosition, Corners.BottomLeft, SearchDirections.UpRight);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available data
    /// starting from the <see cref="Corners.BottomRight"/> corner of the datasets towards the <see cref="SearchDirections.UpLeft"/> search direction.
    /// </summary>
    public static readonly DataFocusFinderPreset RightBottomData = new(DataPosition, Corners.BottomRight, SearchDirections.UpLeft);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available virtualized grid element 
    /// starting from the <see cref="Corners.TopLeft"/> corner of the viewport towards the <see cref="SearchDirections.RightDown"/> search direction.
    /// </summary>
    public static readonly ViewFocusFinderPreset TopLeftView = new(ViewPosition, Corners.TopLeft, SearchDirections.RightDown);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available virtualized grid element 
    /// starting from the <see cref="Corners.TopRight"/> corner of the viewport towards the <see cref="SearchDirections.LeftDown"/> search direction.
    /// </summary>
    public static readonly ViewFocusFinderPreset TopRightView = new(ViewPosition, Corners.TopRight, SearchDirections.LeftDown);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available virtualized grid element 
    /// starting from the <see cref="Corners.BottomLeft"/> corner of the viewport towards the <see cref="SearchDirections.RightUp"/> search direction.
    /// </summary>
    public static readonly ViewFocusFinderPreset BottomLeftView = new(ViewPosition, Corners.BottomLeft, SearchDirections.RightUp);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available virtualized grid element 
    /// starting from the <see cref="Corners.BottomRight"/> corner of the viewport towards the <see cref="SearchDirections.LeftUp"/> search direction.
    /// </summary>
    public static readonly ViewFocusFinderPreset BottomRightView = new(ViewPosition, Corners.BottomRight, SearchDirections.LeftUp);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available virtualized grid element 
    /// starting from the <see cref="Corners.TopLeft"/> corner of the viewport towards the <see cref="SearchDirections.DownRight"/> search direction.
    /// </summary>
    public static readonly ViewFocusFinderPreset LeftTopView = new(ViewPosition, Corners.TopLeft, SearchDirections.DownRight);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available virtualized grid element 
    /// starting from the <see cref="Corners.TopRight"/> corner of the viewport towards the <see cref="SearchDirections.DownLeft"/> search direction.
    /// </summary>
    public static readonly ViewFocusFinderPreset RightTopView = new(ViewPosition, Corners.TopRight, SearchDirections.DownLeft);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available virtualized grid element 
    /// starting from the <see cref="Corners.BottomLeft"/> corner of the viewport towards the <see cref="SearchDirections.UpRight"/> search direction.
    /// </summary>
    public static readonly ViewFocusFinderPreset LeftBottomView = new(ViewPosition, Corners.BottomLeft, SearchDirections.UpRight);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available virtualized grid element 
    /// starting from the <see cref="Corners.BottomRight"/> corner of the viewport towards the <see cref="SearchDirections.UpLeft"/> search direction.
    /// </summary>
    public static readonly ViewFocusFinderPreset RightBottomView = new(ViewPosition, Corners.BottomRight, SearchDirections.UpLeft);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available virtualized grid element 
    /// starting from the center of the viewport towards the <see cref="SearchDirections.FourWayClockwise"/> search direction.
    /// </summary>
    public static readonly ViewFocusFinderPreset CenterClockwiseView = new(ViewCenter, Vector2I.Zero, SearchDirections.FourWayClockwise);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available virtualized grid element 
    /// starting from the center of the viewport towards the <see cref="SearchDirections.FourWayAnticlockwise"/> search direction.
    /// </summary>
    public static readonly ViewFocusFinderPreset CenterAnticlockwiseView = new(ViewCenter, Vector2I.Zero, SearchDirections.FourWayAnticlockwise);

    /// <summary>
    /// Instructs the <see cref="IVirtualGridView{TDataType}"/> to grab focus on the first available virtualized grid element 
    /// starting from the center of the viewport towards the <see cref="SearchDirections.UpDownLeftRight"/> search direction.
    /// </summary>
    public static readonly ViewFocusFinderPreset CenterUpDownLeftRightView = new(ViewCenter, Vector2I.Zero, SearchDirections.UpDownLeftRight);
}