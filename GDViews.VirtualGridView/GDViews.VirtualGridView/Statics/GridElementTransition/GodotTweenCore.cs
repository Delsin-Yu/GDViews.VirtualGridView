using System;
using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid;

internal class GodotTweenCore<TTweenType>(ITweenUser<TTweenType> tweenUser)
{
    private readonly Dictionary<Control, Tween> _activeTween = new();

    public void KillAndCreateNewTween(TTweenType tweenType, Control control, in Vector2? targetPosition, Action<Control>? onFinish, string methodName)
    {
        KillTween(control);
        
        if (!tweenUser.IsTweenSupported(tweenType))
        {
            onFinish?.Invoke(control);
            return;
        }

        var runningTween = control.CreateTween();
        _activeTween[control] = runningTween;
        
        tweenUser.InitializeTween(tweenType, targetPosition, control, runningTween);

        runningTween
            .TweenCallback(
                Callable.From(
                    () =>
                    {
                        if (onFinish != null) DelegateRunner.RunProtected(onFinish, control, "OnFinish", control.Name, methodName);
                        if (!_activeTween.Remove(control, out var tween)) return;
                        tween.Kill();
                    }
                )
            )
            .Dispose();
    }
    
    public void KillTween(Control control)
    {
        if (!_activeTween.Remove(control, out var runningTween)) return;
        
        runningTween.Kill();
        runningTween.Dispose();
    }
}