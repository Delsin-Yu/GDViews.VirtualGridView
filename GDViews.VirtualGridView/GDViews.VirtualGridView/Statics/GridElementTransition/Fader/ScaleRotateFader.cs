using Godot;

namespace GodotViews.VirtualGrid;

public class ScaleRotateFader(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedElementFader, IGodotTweenFader
{
    public float Duration { get; set; } = duration;
    public TweenSetup TweenSetup { get; set; } = tweenSetup;

    private static readonly NodePath ScalePath = new(Control.PropertyName.Scale);
    private static readonly NodePath RotationPath = new(Control.PropertyName.RotationDegrees);
    
    private static readonly Vector2 _showScale = Vector2.One;
    private static readonly Vector2 _HideScale = Vector2.Zero;
    
    private const float _showRotation = 0f;
    private const float _hideRotation = 360f;

    public override void InitializeTween(FadeType fadeType, in Vector2? targetPosition, Control control, Tween tween)
    {
        var show = fadeType is FadeType.Appear;
        control.Scale = show ? _HideScale : _showScale;
        control.RotationDegrees = show ? _hideRotation : _showRotation;

        tween.SetParallel();
        tween
            .TweenProperty(control, ScalePath, show ? _showScale : _HideScale, Duration)
            .SetTrans(TweenSetup.TransitionType)
            .SetEase(TweenSetup.EaseType)
            .Dispose();
        
        tween
            .TweenProperty(control, RotationPath, show ? _showRotation : _hideRotation, Duration)
            .SetTrans(TweenSetup.TransitionType)
            .SetEase(TweenSetup.EaseType)
            .Dispose();
        
        tween.SetParallel(false);
    }

    public override bool IsTweenSupported(FadeType fadeType) => true;

    protected override void OnKillTween(Control control)
    {
        control.Scale = _showScale;
        control.RotationDegrees = _showRotation;
    }
}