using Godot;

namespace GodotViews.VirtualGrid.FocusFinding;

/// <summary>
/// View Start Handler is responsible for resolving the start position from the viewport when finding focus.
/// </summary>
public interface IViewStartHandler<in TArgument>
{
    /// <summary>
    /// Resolves the start position relative to the viewport.
    /// </summary>
    /// <param name="currentView">Provides access to the current displayed viewport items.</param>
    /// <param name="argument">An extra argument passes to the handler.</param>
    /// <returns>The resolved start position, relative to the viewport.</returns>
    Vector2I ResolveStartPosition(ref readonly ReadOnlyViewArray currentView, TArgument argument);
}