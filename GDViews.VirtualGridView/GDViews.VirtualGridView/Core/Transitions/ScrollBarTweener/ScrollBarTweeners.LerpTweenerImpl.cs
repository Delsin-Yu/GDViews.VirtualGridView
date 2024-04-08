using Godot;

namespace GodotViews.VirtualGrid;

public static partial class ScrollBarTweeners
{
    private class LerpTweenerImpl(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedScrollBarTweener<(float targetValue, float targetPage)>, IGodotTweenScrollBarTweener
    {
        public float Duration { get; set; } = duration;
        public TweenSetup TweenSetup { get; set; } = tweenSetup;

        private static readonly NodePath ValuePath = new(ScrollBar.PropertyName.Value);
        private static readonly NodePath PagePath = new(ScrollBar.PropertyName.Page);

        public override (float targetValue, float targetPage) InitializeTween(in (float targetValue, float targetPage) targetValue, ScrollBar control, Tween tween)
        {
            tween.SetParallel();

            tween
                .TweenProperty(control, ValuePath, targetValue.targetValue, Duration)
                .SetTrans(TweenSetup.TransitionType)
                .SetEase(TweenSetup.EaseType)
                .Dispose();
            tween
                .TweenProperty(control, PagePath, targetValue.targetPage, Duration)
                .SetTrans(TweenSetup.TransitionType)
                .SetEase(TweenSetup.EaseType)
                .Dispose();
            
            tween.SetParallel(false);
            
            return targetValue;
        }

        protected override void ResetScrollBar(ScrollBar scrollBar, (float targetValue, float targetPage) previousTarget)
        {
            scrollBar.Value = previousTarget.targetValue;
            scrollBar.Page = previousTarget.targetPage;
        }
    }
}