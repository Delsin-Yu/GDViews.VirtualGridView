using Godot;
using GodotViews.VirtualGrid.Transition.GodotTween;

namespace GodotViews.VirtualGrid;

public static partial class ElementFaders
{
    private class ScaleFaderImpl(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedElementFader<Vector2>(duration, tweenSetup)
    {
        private static readonly NodePath ScalePath = new(Control.PropertyName.Scale);
        private static readonly Vector2 _showScale = Vector2.One;
        private static readonly Vector2 _HideScale = Vector2.Zero;

        public override void Reinitialize(Control control) => control.Scale = _showScale;

        public override void FastForwardState(Control control, Vector2 previousTarget) => control.Scale = previousTarget;

        public override Vector2 InitializeTween(FadeType type, in Vector2 targetValue, Control control, Tween tween)
        {
            var show = type is FadeType.Appear;
            control.Scale = show ? _HideScale : _showScale;
            var targetScale = show ? _showScale : _HideScale;
            tween
                .TweenProperty(control, ScalePath, targetScale, Duration)
                .SetTrans(TweenSetup.TransitionType)
                .SetEase(TweenSetup.EaseType)
                .Dispose();

            return targetScale;
        }

        public override bool IsTweenSupported(FadeType type) => true;
    }
}