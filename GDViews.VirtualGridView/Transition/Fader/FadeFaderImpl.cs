using Godot;
using GodotViews.VirtualGrid.Transition.GodotTween;

namespace GodotViews.VirtualGrid;

public static partial class ElementFaders
{
    private class FadeFaderImpl(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedElementFader<Color>(duration, tweenSetup)
    {
        private static readonly NodePath ModulatePath = new(Control.PropertyName.Modulate);
        private static readonly Color _showColor = Colors.White;
        private static readonly Color _hideColor = Colors.Transparent;

        public override void Reinitialize(Control control) => control.Modulate = _showColor;

        public override void FastForwardState(Control control, Color previousTarget) => control.Modulate = previousTarget;

        public override Color InitializeTween(FadeType type, in Vector2 targetValue, Control control, Tween tween)
        {
            var show = type is FadeType.Appear;
            control.Modulate = show ? _hideColor : _showColor;
            var targetModulate = show ? _showColor : _hideColor;

            tween
                .TweenProperty(control, ModulatePath, targetModulate, Duration)
                .SetTrans(TweenSetup.TransitionType)
                .SetEase(TweenSetup.EaseType)
                .Dispose();
            return targetModulate;
        }

        public override bool IsTweenSupported(FadeType type) => true;
    }
}