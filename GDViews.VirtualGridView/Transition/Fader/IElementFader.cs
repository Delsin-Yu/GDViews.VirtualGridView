using System;
using Godot;

namespace GodotViews.VirtualGrid.Transition;

/// <summary>
/// Element Fader is responsible for managing the hiding and showing of the virtualized elements.<br/>
/// You may access a set of built-in element faders from the <see cref="ElementFaders"/> class.
/// </summary>
/// <remarks>
/// The <see cref="IVirtualGridView{TDataType}"/> abstracted the virtualized element hiding and showing to three separate method calls,
/// developers may inherit this interface and create their customized element faders.
/// </remarks>
public interface IElementFader
{
    /// <summary>
    /// Invoked when the view controller is showing a virtualized element.
    /// </summary>
    /// <param name="control">The virtualized element that gets shown.</param>
    void Appear(Control control);
    
    /// <summary>
    /// Invoked when the view controller is hiding a virtualized element.
    /// </summary>
    /// <param name="control">The virtualized element that gets hidden.</param>
    /// <param name="onFinish">Invoked by the fader when finishing the hiding process,
    /// this delegate notifies the controller to hide and cache the element.</param>
    void Disappear(Control control, Action<Control> onFinish);
    
    /// <summary>
    /// Invoked when the view controller is caching a virtualized element.<br/>
    /// The developer should reset every value affected by this element fader
    /// to their initial state.
    /// </summary>
    /// <param name="control">The virtualized element that gets reset.</param>
    void Reinitialize(Control control);
    
    /// <summary>
    /// Invoked when the view controller is about to perform a fading action.<br/>
    /// The developer should interrupt and clean up the existing fades for the given element.
    /// </summary>
    /// <param name="control">The virtualized element that should have its associated fade interrupted.</param>
    void KillTween(Control control);
}