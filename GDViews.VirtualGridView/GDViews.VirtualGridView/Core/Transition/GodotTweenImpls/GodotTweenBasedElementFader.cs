using System;
using Godot;

namespace GodotViews.VirtualGrid;

public abstract class GodotTweenCoreBasedElementFader<TCachedArgument> : IElementFader, ITweenCoreUser<GodotTweenCoreBasedElementFader<TCachedArgument>.FadeType, Vector2, TCachedArgument>
{
    public enum FadeType
    {
        Disappear,
        Appear
    }

    private readonly GodotTweenCore<FadeType, Vector2, TCachedArgument> _tweenCore;

    protected GodotTweenCoreBasedElementFader()
    {
        _tweenCore = new(this);
    }

    /// <inheritdoc/> 
    public void Disappear(Control control, Action<Control> onFinish) => _tweenCore.KillAndCreateNewTween(FadeType.Disappear, control, Vector2.Zero, onFinish, "Disappear");

    /// <inheritdoc/> 
    public void Appear(Control control) => _tweenCore.KillAndCreateNewTween(FadeType.Appear, control, Vector2.Zero, null, "Appear");

    /// <inheritdoc/> 
    public void KillTween(Control control) => _tweenCore.KillTween(control);

    /// <inheritdoc/> 
    public abstract void Reset(Control control);

    public abstract void ResetControl(Control control, TCachedArgument previousTarget);

    public abstract TCachedArgument InitializeTween(FadeType fadeType, in Vector2 targetValue, Control control, Tween tween);
    public abstract bool IsTweenSupported(FadeType fadeType);
}