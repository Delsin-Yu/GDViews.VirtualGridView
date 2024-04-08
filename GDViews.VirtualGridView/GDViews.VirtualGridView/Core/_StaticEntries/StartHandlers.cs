using Godot;

namespace GodotViews.VirtualGrid;

public interface IDataStartHandler<in TArgument>
{
    Vector2I ResolveStartPosition<TDataType>(ref readonly ReadOnlyDataArray<TDataType> currentView, TArgument argument);
}

public interface IViewStartHandler<in TArgument>
{
    Vector2I ResolveStartPosition(ref readonly ReadOnlyViewArray currentView, TArgument argument);
}

public static class StartHandlers
{
    public static readonly IDataStartHandler<Vector2I> DataPosition = new DataStartPositionHandler();
    public static readonly IViewStartHandler<Vector2I> ViewPosition = new ViewStartPositionHandler();
    public static readonly IViewStartHandler<Vector2I> ViewCenter = new ViewCenterStartPositionHandler();

    private class DataStartPositionHandler : IDataStartHandler<Vector2I>
    {
        public Vector2I ResolveStartPosition<TDataType>(ref readonly ReadOnlyDataArray<TDataType> currentView, Vector2I position) =>
            new(
                position.X < 0 ? currentView.DataSetRows + position.X : position.X,
                position.Y < 0 ? currentView.DataSetColumns + position.Y : position.Y
            );
    }

    private class ViewCenterStartPositionHandler : IViewStartHandler<Vector2I>
    {
        public Vector2I ResolveStartPosition(ref readonly ReadOnlyViewArray currentView, Vector2I argument) => new Vector2I(currentView.ViewRows, currentView.ViewColumns) / 2 + argument;
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