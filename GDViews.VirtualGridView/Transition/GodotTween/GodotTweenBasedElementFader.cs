using System;
using Godot;

namespace GodotViews.VirtualGrid.Transition.GodotTween;

/// <summary>
/// This implementation of the <see cref="IElementFader"/> leverages the <see cref="Godot.Tween"/> system for handling transitions,
/// this type handles necessary initialization and interruptions, and leaves blank the actual tween logic abstract.
/// The developer may inherit this type for creating their customized element fader. 
/// </summary>
/// <typeparam name="TCachedArgument">The argument that gets cached for each running tween action, per control,
/// it is useful for restoring or fast-forwarding the controls' state when interrupting an existing tween or starting a new one.</typeparam>
public abstract class GodotTweenCoreBasedElementFader<TCachedArgument> : IGodotTweenFader, ITweenCoreUser<GodotTweenCoreBasedElementFader<TCachedArgument>.FadeType, Vector2, TCachedArgument>
{
    /// <summary>
    /// The interpolation type that's going to execute.
    /// </summary>
    public enum FadeType
    {
        /// <summary> Instruct this <see cref="IGodotTweenFader"/> to hide the specified control </summary>
        Disappear,
        /// <summary> Instruct this <see cref="IGodotTweenFader"/> to show the specified control </summary>
        Appear
    }

    private readonly GodotTweenCore<FadeType, Vector2, TCachedArgument> _tweenCore;

    private TweenSetup _tweenSetup;
    
    /// <summary> The duration takes to complete the interpolation. </summary>
    public float Duration { get; set; }
    
    /// <summary> ZThe <see cref="TweenSetup"/> used for doing the interpolation. </summary>
    public TweenSetup TweenSetup { get => _tweenSetup; set => _tweenSetup = TweenSetups.CurrentOrDefault(value); }
    
    /// <summary>
    /// Construct an instance of this <see cref="GodotTweenCoreBasedElementFader{TCachedArgument}"/>
    /// </summary>
    /// <param name="duration">The duration takes to complete the interpolation.</param>
    /// <param name="tweenSetup">The <see cref="TweenSetup"/> used for doing the interpolation.</param>
    protected GodotTweenCoreBasedElementFader(float duration, TweenSetup tweenSetup)
    {
        _tweenCore = new(this);
        Duration = duration;
        TweenSetup = tweenSetup;
    }

    /// <inheritdoc/> 
    public void Disappear(Control control, Action<Control> onFinish) => _tweenCore.KillAndCreateNewTween(FadeType.Disappear, control, Vector2.Zero, onFinish, "Disappear");

    /// <inheritdoc/> 
    public void Appear(Control control) => _tweenCore.KillAndCreateNewTween(FadeType.Appear, control, Vector2.Zero, null, "Appear");

    /// <inheritdoc/> 
    public void KillTween(Control control) => _tweenCore.KillTween(control);

    /// <inheritdoc/> 
    public abstract void Reinitialize(Control control);

    /// <summary>
    /// Invoked before starting an interpolation, developer should use the specified value
    /// to fast-forward the state of the affected properties for the provided control.
    /// </summary>
    /// <param name="control">The control to have its affected properties fast-forwarded.</param>
    /// <param name="previousTarget">The cached argument for this control.</param>
    public abstract void FastForwardState(Control control, TCachedArgument previousTarget);

    /// <summary>
    /// Invoked for setting up the tween for the target control.
    /// </summary>
    /// <param name="type">The type of the tween.</param>
    /// <param name="targetValue">The target position this control is meant to get moved to.</param>
    /// <param name="control">The control to move.</param>
    /// <param name="tween">The setting up tween.</param>
    /// <returns>The target argument this tween updates,
    /// this value will be use for fast-forwarding this control's state when interrupting the interpolation.</returns>
    public abstract TCachedArgument InitializeTween(FadeType type, in Vector2 targetValue, Control control, Tween tween);
    
    /// <summary>
    /// Use to check if the specified type of interpolation is supported by this fader.
    /// </summary>
    /// <param name="type">The type for the checking interpolation.</param>
    /// <returns><see langword="true" /> if supports the specified type;
    /// otherwise, <see langword="false" />.</returns>
    public abstract bool IsTweenSupported(FadeType type);
}