using Godot;

namespace GodotViews.VirtualGrid;

public class FadeFader(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedElementFader, IGodotTweenFader
{
    public float Duration { get; set; } = duration;
    public TweenSetup TweenSetup { get; set; } = tweenSetup;

    private static readonly NodePath ModulatePath = new(Control.PropertyName.Modulate);
    private static readonly Color _showColor = Colors.White;
    private static readonly Color _hideColor = Colors.Transparent;

    public override void InitializeTween(FadeType fadeType, in Vector2? targetPosition, Control control, Tween tween)
    {
        var show = fadeType is FadeType.Appear;
        control.Modulate = show ? _hideColor : _showColor;
        tween
            .TweenProperty(control, ModulatePath, show ? _showColor : _hideColor, Duration)
            .SetTrans(TweenSetup.TransitionType)
            .SetEase(TweenSetup.EaseType)
            .Dispose();
    }

    public override bool IsTweenSupported(FadeType fadeType) => true;

    protected override void OnKillTween(Control control) => control.Modulate = _showColor;
}