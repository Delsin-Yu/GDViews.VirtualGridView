using System;
using Godot;

namespace GodotViews.VirtualGrid;

public static class ElementTweenerFactory
{
    public static IElementTweener None { get; } = new NoneTweenerImpl();
    public static IGodotTweenTweener CreatePositional(float duration, TweenSetup tweenSetup) => new Positional(duration, tweenSetup);
        
    private class Positional(float duration, TweenSetup tweenSetup) : GodotTweenBasedElementTweener, IGodotTweenTweener
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

    private class NoneTweenerImpl : IElementTweener
    {
        public void MoveTo(Control control, Vector2 targetPosition) => control.Position = targetPosition;

        public void MoveIn(Control control, Vector2 targetPosition) => control.Position = targetPosition;

        public void MoveOut(Control control, Vector2 targetPosition, Action<Control> onFinish)
        {
            control.Position = targetPosition;
            onFinish.Invoke(control);
        }

        public void KillTween(Control control)
        {
        }
    }
}