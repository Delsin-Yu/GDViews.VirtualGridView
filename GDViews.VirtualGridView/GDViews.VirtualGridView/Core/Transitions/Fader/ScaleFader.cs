using Godot;

namespace GodotViews.VirtualGrid;

public class ScaleFader(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedElementFader<Vector2>, IGodotTweenFader
{
    private static readonly NodePath ScalePath = new(Control.PropertyName.Scale);
    private static readonly Vector2 _showScale = Vector2.One;
    private static readonly Vector2 _HideScale = Vector2.Zero;
    public float Duration { get; set; } = duration;
    public TweenSetup TweenSetup { get; set; } = tweenSetup;

    public override void Reset(Control control) => control.Scale = _showScale;

    public override void ResetControl(Control control, Vector2 previousTarget) => control.Scale = previousTarget;

    public override Vector2 InitializeTween(FadeType fadeType, in Vector2 targetValue, Control control, Tween tween)
    {
        var show = fadeType is FadeType.Appear;
        control.Scale = show ? _HideScale : _showScale;
        var targetScale = show ? _showScale : _HideScale;
        tween
            .TweenProperty(control, ScalePath, targetScale, Duration)
            .SetTrans(TweenSetup.TransitionType)
            .SetEase(TweenSetup.EaseType)
            .Dispose();

        return targetScale;
    }

    public override bool IsTweenSupported(FadeType fadeType) => true;
}