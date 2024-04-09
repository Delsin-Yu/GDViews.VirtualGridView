using Godot;

namespace GodotViews.VirtualGrid;

public static partial class ElementTweeners
{
    private class PanTweenerImpl(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedElementTweener<Vector2>(duration, tweenSetup)
    {
        private static readonly NodePath PositionPath = new(Control.PropertyName.Position);

        public override Vector2 InitializeTween(TweenType type, in Vector2 targetValue, Control control, Tween tween)
        {
            tween
                .TweenProperty(control, PositionPath, targetValue, Duration)
                .SetTrans(TweenSetup.TransitionType)
                .SetEase(TweenSetup.EaseType)
                .Dispose();

            return targetValue;
        }

        public override void FastForwardState(Control control, Vector2 previousTarget) => control.Position = previousTarget;

        public override bool IsTweenSupported(TweenType type) => true;
    }
}