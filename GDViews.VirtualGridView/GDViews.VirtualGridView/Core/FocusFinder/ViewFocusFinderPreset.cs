using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public record struct ViewFocusFinderPreset(
    IViewFocusFinder<Vector2I> FocusFinder,
    ViewStartPositionHandler<Vector2I> StartPositionHandler,
    Vector2I Argument,
    SearchDirection SearchDirection);