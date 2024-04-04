using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public readonly struct ViewFocusFinderPreset(IViewFocusFinder ViewFocusFinder, StartPositionHandler StartPosition, SearchDirection SearchDirection)
{
    public readonly IViewFocusFinder ViewFocusFinder = ViewFocusFinder;
    public readonly StartPositionHandler StartPosition = StartPosition;
    public readonly SearchDirection SearchDirection = SearchDirection;
}