using Godot;

namespace GodotViews.VirtualGrid.Transition;

/// <summary>
/// ScrollBar Tweener is responsible for managing the <see cref="Range.Value"/> and <see cref="Range.Page"/>
/// value interpolation of the <see cref="ScrollBar"/> when user moves the virtualized viewport.<br/>
/// You may access a set of built-in element tweeners from the <see cref="ScrollBarTweeners"/> class.
/// </summary>
public interface IScrollBarTweener
{
    /// <summary>
    /// Invoked when the view controller is updating the values of a <see cref="ScrollBar"/>.
    /// </summary>
    /// <param name="scrollBar">The scroll bar to have its value updated.</param>
    /// <param name="targetValue">The target <see cref="Range.Value"/> value.</param>
    /// <param name="targetPage">The target <see cref="Range.Page"/> value.</param>
    void UpdateValue(ScrollBar scrollBar, float targetValue, float targetPage);

    /// <summary>
    /// Invoked when the view controller is about to perform a viewport moving action.<br/>
    /// The developer should interrupt and clean up the existing tweens for the given scrollBar.
    /// </summary>
    /// <param name="scrollBar">The <see cref="ScrollBar"/> that should have its associated tween interrupted.</param>
    void KillTween(ScrollBar scrollBar);
}