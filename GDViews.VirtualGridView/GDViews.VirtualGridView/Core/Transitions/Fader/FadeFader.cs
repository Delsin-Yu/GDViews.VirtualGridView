using Godot;

namespace GodotViews.VirtualGrid;

public class FadeFader(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedElementFader<Color>, IGodotTweenFader
{
    private static readonly NodePath ModulatePath = new(Control.PropertyName.Modulate);
    private static readonly Color _showColor = Colors.White;
    private static readonly Color _hideColor = Colors.Transparent;
    public float Duration { get; set; } = duration;
    public TweenSetup TweenSetup { get; set; } = tweenSetup;

    public override void Show(Control control) => control.Modulate = _showColor;

    public override void ResetControl(Control control, Color previousTarget) => control.Modulate = previousTarget;

    public override Color InitializeTween(FadeType fadeType, in Vector2 targetValue, Control control, Tween tween)
    {
        var show = fadeType is FadeType.Appear;
        control.Modulate = show ? _hideColor : _showColor;
        var targetModulate = show ? _showColor : _hideColor;

        tween
            .TweenProperty(control, ModulatePath, targetModulate, Duration)
            .SetTrans(TweenSetup.TransitionType)
            .SetEase(TweenSetup.EaseType)
            .Dispose();
        return targetModulate;
    }

    public override bool IsTweenSupported(FadeType fadeType) => true;
}