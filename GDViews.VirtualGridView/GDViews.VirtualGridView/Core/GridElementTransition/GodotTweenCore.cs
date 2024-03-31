using System;
using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid;

internal interface ITweenCoreUser<in TTweenType>
{
    bool IsTweenSupported(TTweenType tweenType);
    void InitializeTween(TTweenType tweenType, in Vector2? targetPosition, Control control, Tween tween);
}

internal class GodotTweenCore<TTweenType>(ITweenCoreUser<TTweenType> tweenCoreUser)
{
    private readonly Dictionary<Control, Tween> _activeTween = new();

    public void KillAndCreateNewTween(
        TTweenType tweenType,
        Control control,
        in Vector2? targetPosition,
        Action<Control>? onFinish,
        string methodName,
        Action<Control>? onFinishSecondary = null
    )
    {
        KillTween(control);

        if (!tweenCoreUser.IsTweenSupported(tweenType))
        {
            onFinish?.Invoke(control);
            onFinishSecondary?.Invoke(control);
            return;
        }

        var runningTween = control.CreateTween();
        _activeTween[control] = runningTween;

        tweenCoreUser.InitializeTween(tweenType, targetPosition, control, runningTween);

        runningTween
            .TweenCallback(
                Callable.From(
                    () =>
                    {
                        var controlName = control.Name;
                        if (onFinish != null) DelegateRunner.RunProtected(onFinish, control, "On Finish #1", controlName, methodName);
                        if (onFinishSecondary != null) DelegateRunner.RunProtected(onFinishSecondary, control, "On Finish #2", controlName, methodName);
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