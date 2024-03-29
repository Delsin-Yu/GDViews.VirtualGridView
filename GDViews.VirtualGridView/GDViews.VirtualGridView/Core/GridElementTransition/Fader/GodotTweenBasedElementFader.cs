using System;
using Godot;

namespace GodotViews.VirtualGrid;

public abstract class GodotTweenCoreBasedElementFader : IElementFader, ITweenCoreUser<GodotTweenCoreBasedElementFader.FadeType>
{
    public enum FadeType
    {
        Disappear,
        Appear
    }
    
    private readonly GodotTweenCore<FadeType> _tweenCore;

    protected GodotTweenCoreBasedElementFader()
    {
        _tweenCore = new(this);
    }
    
    /// <inheritdoc/> 
    public void Disappear(Control control, Action<Control> onFinish) => _tweenCore.KillAndCreateNewTween(FadeType.Disappear, control, Vector2.Zero, onFinish, "Disappear");

    /// <inheritdoc/> 
    public void Appear(Control control) => _tweenCore.KillAndCreateNewTween(FadeType.Appear, control, Vector2.Zero, null, "Appear");

    /// <inheritdoc/> 
    public void KillTween(Control control)
    {
        _tweenCore.KillTween(control);
        OnKillTween(control);
    }

    public abstract void InitializeTween(FadeType fadeType, in Vector2? targetPosition, Control control, Tween tween);
    public abstract bool IsTweenSupported(FadeType fadeType);
    protected virtual void OnKillTween(Control control) { }
}