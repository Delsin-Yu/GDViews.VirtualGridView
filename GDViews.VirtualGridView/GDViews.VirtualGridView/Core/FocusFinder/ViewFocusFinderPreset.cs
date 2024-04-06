using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public record struct ViewFocusFinderPreset(
    ViewFocusFinderPreset<Vector2I> Preset,
    Vector2I Argument,
    SearchDirection SearchDirection);

public record struct ViewFocusFinderPreset<TArgument>(
    IViewFocusFinder<TArgument> FocusFinder,
    IViewStartHandler<TArgument> StartHandler);

public record struct DataFocusFinderPreset(
    DataFocusFinderPreset<Vector2I> Preset,
    Vector2I Argument,
    SearchDirection SearchDirection);

public record struct DataFocusFinderPreset<TArgument>(
    IDataFocusFinder<TArgument> FocusFinder,
    IDataStartHandler<TArgument> StartHandler
);