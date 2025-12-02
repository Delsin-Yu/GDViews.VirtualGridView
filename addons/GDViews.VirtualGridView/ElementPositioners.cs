using GodotViews.VirtualGrid.Positioner;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a set of built-in <see cref="IElementPositioner"/> that should cover common UI/UX development needs.
/// </summary>
public static partial class ElementPositioners
{
    /// <summary>
    /// This element positioner adjusts the viewport accordingly when the focused element lies outside of the viewport. 
    /// </summary>
    public static IElementPositioner Side { get; } = new SideImpl();

    /// <summary>
    /// This element positioner places the focused element in the center of the viewport.
    /// </summary>
    public static IElementPositioner Centered { get; } = new CenteredImpl();
}