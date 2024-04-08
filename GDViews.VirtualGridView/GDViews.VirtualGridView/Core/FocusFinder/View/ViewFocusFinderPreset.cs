using Godot;

namespace GodotViews.VirtualGrid;

public record struct ViewFocusFinderPreset(
    ViewFocusFinderPreset<Vector2I> Preset,
    Vector2I Argument,
    SearchDirection SearchDirection);

public record struct ViewFocusFinderPreset<TArgument>(
    IViewFocusFinder<TArgument> FocusFinder,
    IViewStartHandler<TArgument> StartHandler);