using System;
using Godot;

namespace GodotViews.VirtualGrid;

public abstract class GodotTweenBasedElementTweener : IElementTweener, ITweenUser<GodotTweenBasedElementTweener.TweenType>
{
    public enum TweenType
    {
        MoveTo,
        MoveIn,
        MoveOut
    }

    private readonly GodotTweenCore<TweenType> _tweenCore;

    protected GodotTweenBasedElementTweener()
    {
        _tweenCore = new(this);
    }

    /// <inheritdoc/> 
    public void KillTween(Control control)
    {
        _tweenCore.KillTween(control);
        OnKillTween(control);
    }

    /// <inheritdoc/> 
    public void MoveTo(Control control, Vector2 targetPosition) => _tweenCore.KillAndCreateNewTween(TweenType.MoveTo, control, targetPosition, null, "Move To");

    /// <inheritdoc/> 
    public void MoveIn(Control control, Vector2 targetPosition) => _tweenCore.KillAndCreateNewTween(TweenType.MoveIn, control, targetPosition, null, "Move In");

    /// <inheritdoc/> 
    public void MoveOut(Control control, Vector2 targetPosition, Action<Control> onFinish) => _tweenCore.KillAndCreateNewTween(TweenType.MoveOut, control, targetPosition, onFinish, "Move Out");

    public abstract void InitializeTween(TweenType tweenType, in Vector2? targetPosition, Control control, Tween tween);
    public abstract bool IsTweenSupported(TweenType tweenType);
    protected virtual void OnKillTween(Control control) { }
}