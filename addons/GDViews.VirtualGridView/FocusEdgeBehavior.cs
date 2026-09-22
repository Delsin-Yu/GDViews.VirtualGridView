namespace GodotViews.VirtualGrid;

/// <summary>
/// Focus behavior when directional input reaches a dataset edge of a virtual grid.
/// </summary>
public enum FocusEdgeBehavior
{
    /// <summary>
    /// Do not intercept; the event continues to <c>_OnGuiInput</c> / engine default focus navigation.
    /// </summary>
    None,

    /// <summary>
    /// Swallow the edge input and keep focus on the current edge cell.
    /// </summary>
    Clamped,

    /// <summary>
    /// Wrap focus to the opposite side of the same grid (data-space; may scroll the viewport).
    /// </summary>
    Looped,
}
