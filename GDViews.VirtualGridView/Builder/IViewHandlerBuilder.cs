using GodotViews.VirtualGrid.Positioner;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid.Builder;


/// <summary>
/// The builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.<br/>
/// Use the <see cref="WithHandlers"/> method to set up the visual transition behavior for the grid elements.
/// </summary>
public interface IViewHandlerBuilder
{
    /// <summary>
    /// Sets the visual transition behavior for the grid elements, and moves to the next build process.
    /// </summary>
    /// <param name="elementPositioner">The Positioner assigned to the <see cref="IVirtualGridView{TDataType}"/>.</param>
    /// <param name="elementTweener">The Tweener assigned to the <see cref="IVirtualGridView{TDataType}"/>.</param>
    /// <param name="elementFader">The Fader assigned to the <see cref="IVirtualGridView{TDataType}"/>.</param>
    /// <returns>A builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    IDataLayoutBuilder WithHandlers(IElementPositioner elementPositioner, IElementTweener elementTweener, IElementFader elementFader);
}