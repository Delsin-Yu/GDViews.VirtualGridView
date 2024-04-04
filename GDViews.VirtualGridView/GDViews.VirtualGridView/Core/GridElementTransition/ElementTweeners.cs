namespace GodotViews.VirtualGrid;

public static partial class ElementTweeners
{
    public static IElementTweener None { get; } = new NoneTweenerImpl();
    public static IGodotTweenTweener CreatePan(float duration, TweenSetup? tweenSetup = null) => 
        new PanTweenerImpl(duration, TweenSetups.CurrentOrDefault(tweenSetup));
}