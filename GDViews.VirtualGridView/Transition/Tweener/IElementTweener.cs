using System;
using Godot;

namespace GodotViews.VirtualGrid.Transition;

/// <summary>
/// Element Tweener is responsible for managing the visual positional interpolation of the elements when user moves the virtualized viewport.<br/>
/// You may access a set of built-in element tweeners from the <see cref="ElementTweeners"/> class.
/// </summary>
/// <remarks>
/// The <see cref="IVirtualGridView{TDataType}"/> abstracted the virtualized element movement to three separate method calls,
/// developers may inherit this interface and create their customized element tweeners.
/// </remarks>
public interface IElementTweener
{
    /// <summary>
    /// Invoked when the view controller is moving a virtualized element inside the viewport.
    /// </summary>
    /// <param name="control">The moving virtualized element.</param>
    /// <param name="targetPosition">The target position this element is meant to get moves to.</param>
    void MoveTo(Control control, Vector2 targetPosition);
    
    /// <summary>
    /// Invoked when the view controller is moving a newly spawned or reused virtualized element into the viewport.
    /// </summary>
    /// <param name="control">The newly spawned or reused virtualized element, outside of the viewport.</param>
    /// <param name="targetPosition">The target position this element is meant to get moves to.</param>
    void MoveIn(Control control, Vector2 targetPosition);
    
    /// <summary>
    /// Invoked when the view controller is moving a virtualized element out from the viewport.
    /// </summary>
    /// <param name="control">The virtualized element to get moves out.</param>
    /// <param name="targetPosition">The target position this element is meant to get moves out to.</param>
    /// <param name="onFinish">Invoked by the tweener when finishing the moving process,
    /// this delegate notifies the controller to hide and cache the element.</param>
    void MoveOut(Control control, Vector2 targetPosition, Action<Control> onFinish);
    
    /// <summary>
    /// Invoked when the view controller is about to perform a moving action.<br/>
    /// The developer should interrupt and clean up the existing tweens for the given element.
    /// </summary>
    /// <param name="control">The virtualized element that should have its associated tween interrupted.</param>
    void KillTween(Control control);
}