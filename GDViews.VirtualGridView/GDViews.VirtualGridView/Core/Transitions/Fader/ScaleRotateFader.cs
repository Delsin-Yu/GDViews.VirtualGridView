using System;
using Godot;

namespace GodotViews.VirtualGrid;

public class ScaleRotateFader(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedElementFader<(Vector2 Scale, float Rotation)>, IGodotTweenFader
{
    private const float _showRotation = 0f;
    private const float _hideRotation = 180f;

    private static readonly NodePath ScalePath = new(Control.PropertyName.Scale);
    private static readonly NodePath RotationPath = new(Control.PropertyName.RotationDegrees);

    private static readonly Vector2 _showScale = Vector2.One;
    private static readonly Vector2 _hideScale = Vector2.Zero;
    public float Duration { get; set; } = duration;
    public TweenSetup TweenSetup { get; set; } = tweenSetup;

    public override void Show(Control control)
    {
        control.Scale = _showScale;
        control.RotationDegrees = _showRotation;
    }

    public override void ResetControl(Control control, (Vector2 Scale, float Rotation) previousTarget)
    {
        control.Scale = previousTarget.Scale;
        control.RotationDegrees = previousTarget.Rotation;
    }

    public override (Vector2 Scale, float Rotation) InitializeTween(FadeType fadeType, in Vector2 targetValue, Control control, Tween tween)
    {
        tween.SetParallel();

        Vector2 startScale;
        float startRotation;

        Vector2 targetScale;
        float targetRotation;

        switch (fadeType)
        {
            case FadeType.Disappear:
                startScale = _showScale;
                targetScale = _hideScale;
                startRotation = _showRotation;
                targetRotation = _hideRotation;
                break;
            case FadeType.Appear:
                startScale = _hideScale;
                targetScale = _showScale;
                startRotation = -_hideRotation;
                targetRotation = _showRotation;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(fadeType), fadeType, null);
        }

        control.Scale = startScale;
        control.RotationDegrees = startRotation;

        tween
            .TweenProperty(control, ScalePath, targetScale, Duration)
            .SetTrans(TweenSetup.TransitionType)
            .SetEase(TweenSetup.EaseType)
            .Dispose();

        tween
            .TweenProperty(control, RotationPath, targetRotation, Duration)
            .SetTrans(TweenSetup.TransitionType)
            .SetEase(TweenSetup.EaseType)
            .Dispose();

        tween.SetParallel(false);

        return (targetScale, targetRotation);
    }

    public override bool IsTweenSupported(FadeType fadeType) => true;
}