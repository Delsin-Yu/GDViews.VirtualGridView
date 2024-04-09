using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// This implementation of the <see cref="IScrollBarTweener"/> leverages the <see cref="Godot.Tween"/> system for handling transitions,
/// this type handles necessary initialization and interruptions, and leaves blank the actual tween logic abstract.
/// The developer may inherit this type for creating their customized scroll bar tweener. 
/// </summary>
/// <typeparam name="TCachedArgument">The argument that gets cached for each running tween action, per control,
/// it is useful for restoring or fast-forwarding the controls' state when interrupting an existing tween or starting a new one.</typeparam>
public abstract class GodotTweenCoreBasedScrollBarTweener<TCachedArgument> : IGodotTweenScrollBarTweener, ITweenCoreUser<NoExtraArgument, (float targetValue, float targetPage), TCachedArgument>
{
    private readonly GodotTweenCore<NoExtraArgument, (float targetValue, float targetPage), TCachedArgument> _tweenCore;

    /// <summary> The duration takes to complete the interpolation. </summary>
    public float Duration { get; set; }
    
    /// <summary> ZThe <see cref="TweenSetup"/> used for doing the interpolation. </summary>
    public TweenSetup TweenSetup { get; set; }
  
    /// <summary>
    /// Construct an instance of this <see cref="GodotTweenCoreBasedScrollBarTweener{TCachedArgument}"/>
    /// </summary>
    /// <param name="duration">The duration takes to complete the interpolation.</param>
    /// <param name="tweenSetup">The <see cref="TweenSetup"/> used for doing the interpolation.</param>

    protected GodotTweenCoreBasedScrollBarTweener(float duration, TweenSetup tweenSetup)
    {
        _tweenCore = new(this);
        Duration = duration;
        TweenSetup = tweenSetup;
    }


    /// <inheritdoc/>
    public void UpdateValue(ScrollBar scrollBar, float targetValue, float targetPage) => _tweenCore.KillAndCreateNewTween(NoExtraArgument.Default, scrollBar, (targetValue, targetPage), null, "Update Scroll Bar Value");

    /// <inheritdoc />
    public void KillTween(ScrollBar scrollBar) => _tweenCore.KillTween(scrollBar);

    /// <inheritdoc cref="GodotTweenCoreBasedElementFader{TCachedArgument}.IsTweenSupported"/>
    public bool IsTweenSupported(NoExtraArgument type) => true;
    
    /// <inheritdoc cref="GodotTweenCoreBasedElementFader{TCachedArgument}.FastForwardState"/>
    public void FastForwardState(Control control, TCachedArgument previousTarget) => FastForwardState((ScrollBar)control, previousTarget);
    
    /// <inheritdoc cref="GodotTweenCoreBasedElementFader{TCachedArgument}.InitializeTween"/>
    public TCachedArgument InitializeTween(NoExtraArgument type, in (float targetValue, float targetPage) targetValue, Control control, Tween tween) => InitializeTween(in targetValue, (ScrollBar)control, tween);

    /// <summary>
    /// Called for setting up the tween for the target scroll bar.
    /// </summary>
    /// <param name="targetValue">The target value this scroll bar is meant to get updated.</param>
    /// <param name="scrollBar">The scroll bar to update.</param>
    /// <param name="tween">The setting up tween.</param>
    /// <returns>The target argument this tween updates,
    /// this value will be use for fast-forwarding this scroll bar's state when interrupting the interpolation.</returns>
    protected abstract TCachedArgument InitializeTween(in (float targetValue, float targetPage) targetValue, ScrollBar scrollBar, Tween tween);
    
    /// <summary>
    /// Called before starting an interpolation, developer should use the specified value
    /// to fast-forward the state of the affected properties for the provided scroll bar.
    /// </summary>
    /// <param name="scrollBar">The scroll bar to have its affected properties fast-forwarded.</param>
    /// <param name="previousTarget">The cached argument for this scroll bar.</param>
    protected abstract void FastForwardState(ScrollBar scrollBar, TCachedArgument previousTarget);
}