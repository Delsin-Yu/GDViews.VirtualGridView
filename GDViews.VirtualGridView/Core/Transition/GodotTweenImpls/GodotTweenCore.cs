using System;
using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid;

internal interface ITweenCoreUser<in TTweenType, TTweenArgument, TCachedArgument>
{
    bool IsTweenSupported(TTweenType type);
    void FastForwardState(Control control, TCachedArgument previousTarget);
    TCachedArgument InitializeTween(TTweenType type, in TTweenArgument targetValue, Control control, Tween tween);
}

internal class GodotTweenCore<TTweenType, TTweenArgument, TCachedArgument>(ITweenCoreUser<TTweenType, TTweenArgument, TCachedArgument> tweenCoreUser)
{
    private readonly Dictionary<Control, Tween> _activeTween = new();
    private readonly Dictionary<Control, TCachedArgument> _cachedArguments = [];

    public void KillAndCreateNewTween(
        TTweenType tweenType,
        Control control,
        in TTweenArgument targetArgument,
        Action<Control>? onFinish,
        string methodName
    )
    {
        KillTween(control);

        if (!tweenCoreUser.IsTweenSupported(tweenType))
        {
            onFinish?.Invoke(control);
            return;
        }

        var runningTween = control.CreateTween();
        _activeTween[control] = runningTween;

        _cachedArguments[control] = tweenCoreUser.InitializeTween(tweenType, targetArgument, control, runningTween);

        runningTween
            .TweenCallback(
                Callable.From(
                    () =>
                    {
                        var controlName = control.Name;
                        if (onFinish != null) DelegateRunner.RunProtected(onFinish, control, "On Finish #1", controlName, methodName);
                        if (_activeTween.Remove(control, out var tween)) tween.Kill();
                        if (_cachedArguments.Remove(control, out var previousTarget)) tweenCoreUser.FastForwardState(control, previousTarget);
                    }
                )
            )
            .Dispose();
    }

    public void KillTween(Control control)
    {
        NullableData<TCachedArgument> cachedArgument;
        bool ret;
        if (!_activeTween.Remove(control, out var runningTween))
        {
            cachedArgument = NullableData.Null<TCachedArgument>();
            ret = false;
        }
        else
        {
            runningTween.Kill();
            runningTween.Dispose();

            if (!_cachedArguments.Remove(control, out var previousTarget))
                cachedArgument = NullableData.Null<TCachedArgument>();
            else
                cachedArgument = NullableData.Create(previousTarget);

            ret = true;
        }

        if (ret && cachedArgument.TryUnwrap(out var data))
            tweenCoreUser.FastForwardState(control, data);
    }
}