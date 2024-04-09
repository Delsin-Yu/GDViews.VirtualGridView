using Godot;

namespace GodotViews.VirtualGrid;

public static partial class StartHandlers
{
    private class DataStartPositionHandler : IDataStartHandler<Vector2I>
    {
        public Vector2I ResolveStartPosition<TDataType>(ref readonly ReadOnlyDataArray<TDataType> currentView, Vector2I position) =>
            new(
                position.X < 0 ? currentView.DataSetRows + position.X : position.X,
                position.Y < 0 ? currentView.DataSetColumns + position.Y : position.Y
            );
    }
}