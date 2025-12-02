using Godot;
using GodotViews.VirtualGrid.Transition.GodotTween;

namespace GodotViews.VirtualGrid;

public static partial class ScrollBarTweeners
{
    private class LerpImpl(float duration, TweenSetup tweenSetup) : GodotTweenCoreBasedScrollBarTweener<(float targetValue, float targetPage)>(duration, tweenSetup)
    {
        private static readonly NodePath ValuePath = new(ScrollBar.PropertyName.Value);
        private static readonly NodePath PagePath = new(ScrollBar.PropertyName.Page);

        protected override (float targetValue, float targetPage) InitializeTween(in (float targetValue, float targetPage) targetValue, ScrollBar control, Tween tween)
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

        protected override void FastForwardState(ScrollBar scrollBar, (float targetValue, float targetPage) previousTarget)
        {
            scrollBar.Value = previousTarget.targetValue;
            scrollBar.Page = previousTarget.targetPage;
        }
    }
}