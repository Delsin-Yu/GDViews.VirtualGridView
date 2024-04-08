using Godot;

namespace GodotViews.VirtualGrid;

public record struct DataFocusFinderPreset(
    DataFocusFinderPreset<Vector2I> Preset,
    Vector2I Argument,
    SearchDirection SearchDirection);

public record struct DataFocusFinderPreset<TArgument>(
    IDataFocusFinder<TArgument> FocusFinder,
    IDataStartHandler<TArgument> StartHandler
);