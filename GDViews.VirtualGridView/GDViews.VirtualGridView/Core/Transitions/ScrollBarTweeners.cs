namespace GodotViews.VirtualGrid;

public static partial class ScrollBarTweeners
{
    public static IScrollBarTweener None = new NoneTweenerImpl();
    public static IGodotTweenScrollBarTweener CreateLerp(float duration, TweenSetup? tweenSetup = null) => 
        new LerpTweenerImpl(duration, TweenSetups.CurrentOrDefault(tweenSetup));
}