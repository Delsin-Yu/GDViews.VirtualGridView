using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid;

public static partial class ElementTweeners
{
    private class PositionalTweenerImpl(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedElementTweener, IGodotTweenTweener
    {
        public float Duration { get; set; } = duration;
        public TweenSetup TweenSetup { get; set; } = tweenSetup;

        private static readonly NodePath PositionPath = new(Control.PropertyName.Position);

        private readonly Dictionary<Control, Vector2> _cachedPosition = [];
        
        public override void InitializeTween(TweenType tweenType, in Vector2? targetPosition, Control control, Tween tween)
        {
            if (_cachedPosition.TryGetValue(control, out var previousTarget))
            {
                control.Position = previousTarget;
            }

            var targetPositionValue = targetPosition!.Value;
            _cachedPosition[control] = targetPositionValue;
            
            tween
                .TweenProperty(control, PositionPath, targetPositionValue, Duration)
                .SetTrans(TweenSetup.TransitionType)
                .SetEase(TweenSetup.EaseType)
                .Dispose();
        }

        protected override void OnTweenFinish(Control control) => _cachedPosition.Remove(control);

        protected override void OnKillTween(Control control) => _cachedPosition.Remove(control);

        public override bool IsTweenSupported(TweenType tweenType) => true;
    }
}