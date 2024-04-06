using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public static class ViewCorner
{
    public static readonly Vector2I TopLeft = Vector2I.Zero;
    public static readonly Vector2I TopRight = new(0, -1);
    public static readonly Vector2I BottomLeft = new(-1, 0);
    public static readonly Vector2I BottomRight = new(-1, -1);
}

public static class StartFrom
{
    private static Vector2I PositionHandler(ref readonly ReadOnlyViewArray currentView, Vector2I position) =>
        new(
            position.X < 0 ? currentView.ViewRows + position.X : position.X,
            position.Y < 0 ? currentView.ViewColumns + position.Y : position.Y
        );  
    
    private static Vector2I PositionHandler<TDataType>(ref readonly ReadOnlyDataArray<TDataType> currentView, Vector2I position) =>
        new(
            position.X < 0 ? currentView.DataSetRows + position.X : position.X,
            position.Y < 0 ? currentView.DataSetColumns + position.Y : position.Y
        );

    private static Vector2I CenterHandler(ref readonly ReadOnlyViewArray currentView, Vector2I offset) => 
        new Vector2I(currentView.ViewRows, currentView.ViewColumns) / 2 + offset;
    
    public static DataStartPositionHandler<TDataType, Vector2I> DataSetPosition<TDataType>() => PositionHandler<TDataType>;
    public static ViewStartPositionHandler<Vector2I> ViewPosition { get; } = PositionHandler;
    public static ViewStartPositionHandler<Vector2I> ViewCenter { get; } = CenterHandler;

}