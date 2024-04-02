using System;
using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid;

public abstract class GodotTweenCoreBasedElementTweener<TCachedArgument> : IElementTweener, ITweenCoreUser<GodotTweenCoreBasedElementTweener<TCachedArgument>.TweenType, TCachedArgument>
{
    public enum TweenType
    {
        MoveTo,
        MoveIn,
        MoveOut
    }

    private readonly GodotTweenCore<TweenType, TCachedArgument> _tweenCore;

    protected GodotTweenCoreBasedElementTweener()
    {
        _tweenCore = new(this);
    }

    
    /// <inheritdoc/> 
    public void KillTween(Control control) => _tweenCore.KillTween(control);

    /// <inheritdoc/> 
    public void MoveTo(Control control, Vector2 targetPosition) => _tweenCore.KillAndCreateNewTween(TweenType.MoveTo, control, targetPosition, null, "Move To");

    /// <inheritdoc/> 
    public void MoveIn(Control control, Vector2 targetPosition) => _tweenCore.KillAndCreateNewTween(TweenType.MoveIn, control, targetPosition, null, "Move In");

    /// <inheritdoc/> 
    public void MoveOut(Control control, Vector2 targetPosition, Action<Control> onFinish) => _tweenCore.KillAndCreateNewTween(TweenType.MoveOut, control, targetPosition, onFinish, "Move Out");

    public abstract TCachedArgument InitializeTween(TweenType tweenType, in Vector2? targetPosition, Control control, Tween tween);
    public abstract void ResetControl(Control control, TCachedArgument previousTarget);
    public abstract bool IsTweenSupported(TweenType tweenType);


}