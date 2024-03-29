using Godot;

namespace GodotViews.VirtualGrid;

public static partial class ElementTweeners
{
    private class PositionalTweenerImpl(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedElementTweener, IGodotTweenTweener
    {
        public float Duration { get; set; } = duration;
        public TweenSetup TweenSetup { get; set; } = tweenSetup;

        private static readonly NodePath PositionPath = new(Control.PropertyName.Position);

        public override void InitializeTween(TweenType tweenType, in Vector2? targetPosition, Control control, Tween tween) => tween
            .TweenProperty(control, PositionPath, targetPosition!.Value, Duration)
            .SetTrans(TweenSetup.TransitionType)
            .SetEase(TweenSetup.EaseType)
            .Dispose();

        public override bool IsTweenSupported(TweenType tweenType) => true;
    }
}