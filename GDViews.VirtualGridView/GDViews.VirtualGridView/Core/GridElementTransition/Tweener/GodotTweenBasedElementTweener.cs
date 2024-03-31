using System;
using Godot;

namespace GodotViews.VirtualGrid;

public abstract class GodotTweenCoreBasedElementTweener : IElementTweener, ITweenCoreUser<GodotTweenCoreBasedElementTweener.TweenType>
{
    public enum TweenType
    {
        MoveTo,
        MoveIn,
        MoveOut
    }

    private readonly GodotTweenCore<TweenType> _tweenCore;
    private readonly Action<Control> _tweenFinishDelegate;

    protected GodotTweenCoreBasedElementTweener()
    {
        _tweenCore = new(this);
        _tweenFinishDelegate = OnTweenFinish;
    }

    /// <inheritdoc/> 
    public void KillTween(Control control)
    {
        _tweenCore.KillTween(control);
        OnKillTween(control);
    }

    /// <inheritdoc/> 
    public void MoveTo(Control control, Vector2 targetPosition) => _tweenCore.KillAndCreateNewTween(TweenType.MoveTo, control, targetPosition, null, "Move To", _tweenFinishDelegate);

    /// <inheritdoc/> 
    public void MoveIn(Control control, Vector2 targetPosition) => _tweenCore.KillAndCreateNewTween(TweenType.MoveIn, control, targetPosition, null, "Move In", _tweenFinishDelegate);

    /// <inheritdoc/> 
    public void MoveOut(Control control, Vector2 targetPosition, Action<Control> onFinish) => _tweenCore.KillAndCreateNewTween(TweenType.MoveOut, control, targetPosition, onFinish, "Move Out", _tweenFinishDelegate);

    public abstract void InitializeTween(TweenType tweenType, in Vector2? targetPosition, Control control, Tween tween);
    public abstract bool IsTweenSupported(TweenType tweenType);
    protected virtual void OnKillTween(Control control) { }
    protected virtual void OnTweenFinish(Control control) { }
}