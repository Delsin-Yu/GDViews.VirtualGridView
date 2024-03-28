using Godot;

namespace GodotViews.VirtualGrid;

public class ScaleFader(float duration, TweenSetup tweenSetup) : GodotTweenBasedElementFader, IGodotTweenFader
{
    public float Duration { get; set; } = duration;
    public TweenSetup TweenSetup { get; set; } = tweenSetup;

    private static readonly NodePath ScalePath = new(Control.PropertyName.Scale);
    private static readonly Vector2 _showScale = Vector2.One;
    private static readonly Vector2 _HideScale = Vector2.Zero;

    public override void InitializeTween(FadeType fadeType, in Vector2? targetPosition, Control control, Tween tween)
    {
        var show = fadeType is FadeType.Appear;
        control.Scale = show ? _HideScale : _showScale;
        tween
            .TweenProperty(control, ScalePath, show ? _showScale : _HideScale, Duration)
            .SetTrans(TweenSetup.TransitionType)
            .SetEase(TweenSetup.EaseType)
            .Dispose();
    }

    public override bool IsTweenSupported(FadeType fadeType) => true;

    protected override void OnKillTween(Control control) => control.Scale = _showScale;
}