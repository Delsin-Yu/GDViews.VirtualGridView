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

public interface IViewStartHandler<in TArgument>
{
    Vector2I ResolveStartPosition(ref readonly ReadOnlyViewArray currentView, TArgument argument);
}

public interface IDataStartHandler<in TArgument>
{
    Vector2I ResolveStartPosition<TDataType>(ref readonly ReadOnlyDataArray<TDataType> currentView, TArgument argument);
}

public static class StartHandlers
{
    public static IDataStartHandler<Vector2I> DataPosition { get; } = new DataStartPositionHandler();
    public static IViewStartHandler<Vector2I> ViewPosition { get; } = new ViewStartPositionHandler();
    public static IViewStartHandler<Vector2I> ViewCenter { get; } = new ViewCenterStartPositionHandler();

    private class DataStartPositionHandler : IDataStartHandler<Vector2I>
    {
        public Vector2I ResolveStartPosition<TDataType>(ref readonly ReadOnlyDataArray<TDataType> currentView, Vector2I position)
         => new(
             position.X < 0 ? currentView.DataSetRows + position.X : position.X,
             position.Y < 0 ? currentView.DataSetColumns + position.Y : position.Y
         );
    }
    
    private class ViewCenterStartPositionHandler : IViewStartHandler<Vector2I>
    {
        public Vector2I ResolveStartPosition(ref readonly ReadOnlyViewArray currentView, Vector2I argument) => 
            new Vector2I(currentView.ViewRows, currentView.ViewColumns) / 2 + argument;
    }
    
    private class ViewStartPositionHandler : IViewStartHandler<Vector2I>
    {
        public Vector2I ResolveStartPosition(ref readonly ReadOnlyViewArray currentView, Vector2I position) =>
            new(
                position.X < 0 ? currentView.ViewRows + position.X : position.X,
                position.Y < 0 ? currentView.ViewColumns + position.Y : position.Y
            );
    }

}