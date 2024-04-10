using System;
using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// This implementation of the <see cref="IElementTweener"/> leverages the <see cref="Godot.Tween"/> system for handling transitions,
/// this type handles necessary initialization and interruptions, and leaves blank the actual tween logic abstract.
/// The developer may inherit this type for creating their customized element tweener. 
/// </summary>
/// <typeparam name="TCachedArgument">The argument that gets cached for each running tween action, per control,
/// it is useful for restoring or fast-forwarding the controls' state when interrupting an existing tween or starting a new one.</typeparam>
public abstract class GodotTweenCoreBasedElementTweener<TCachedArgument> : IGodotTweenTweener, ITweenCoreUser<GodotTweenCoreBasedElementTweener<TCachedArgument>.TweenType, Vector2, TCachedArgument>
{
    /// <inheritdoc cref="GodotTweenCoreBasedElementFader{TCachedArgument}.FadeType"/>
    public enum TweenType
    {
        /// <summary> Instruct this <see cref="IGodotTweenTweener"/> to move the specified control inside the viewport.</summary>
        MoveTo,
        /// <summary> Instruct this <see cref="IGodotTweenTweener"/> to move the specified control from outside into the viewport.</summary>
        MoveIn,
        /// <summary> Instruct this <see cref="IGodotTweenTweener"/> to move the specified control out from viewport.</summary>
        MoveOut
    }

    private readonly GodotTweenCore<TweenType, Vector2, TCachedArgument> _tweenCore;
  
    /// <inheritdoc cref="GodotTweenCoreBasedElementFader{TCachedArgument}.Duration"/>
    public float Duration { get; set; }
    
    /// <inheritdoc cref="GodotTweenCoreBasedElementFader{TCachedArgument}.TweenSetup"/>
    public TweenSetup TweenSetup { get; set; }
    
    /// <summary>
    /// Construct an instance of this <see cref="GodotTweenCoreBasedElementTweener{TCachedArgument}"/>
    /// </summary>
    /// <param name="duration">The duration takes to complete the interpolation.</param>
    /// <param name="tweenSetup">The <see cref="TweenSetup"/> used for doing the interpolation.</param>
    protected GodotTweenCoreBasedElementTweener(float duration, TweenSetup tweenSetup)
    {
        _tweenCore = new(this);
        Duration = duration;
        TweenSetup = tweenSetup;
    }
    
    /// <inheritdoc/> 
    public void KillTween(Control control) => _tweenCore.KillTween(control);

    /// <inheritdoc/> 
    public void MoveTo(Control control, Vector2 targetPosition) => _tweenCore.KillAndCreateNewTween(TweenType.MoveTo, control, targetPosition, null, "Move To");

    /// <inheritdoc/> 
    public void MoveIn(Control control, Vector2 targetPosition) => _tweenCore.KillAndCreateNewTween(TweenType.MoveIn, control, targetPosition, null, "Move In");

    /// <inheritdoc/> 
    public void MoveOut(Control control, Vector2 targetPosition, Action<Control> onFinish) => _tweenCore.KillAndCreateNewTween(TweenType.MoveOut, control, targetPosition, onFinish, "Move Out");

    /// <inheritdoc cref="GodotTweenCoreBasedElementFader{TCachedArgument}.InitializeTween"/>
    public abstract TCachedArgument InitializeTween(TweenType type, in Vector2 targetValue, Control control, Tween tween);
    
    /// <inheritdoc cref="GodotTweenCoreBasedElementFader{TCachedArgument}.FastForwardState"/>
    public abstract void FastForwardState(Control control, TCachedArgument previousTarget);

    /// <inheritdoc cref="GodotTweenCoreBasedElementFader{TCachedArgument}.IsTweenSupported"/>
    public abstract bool IsTweenSupported(TweenType type);
}