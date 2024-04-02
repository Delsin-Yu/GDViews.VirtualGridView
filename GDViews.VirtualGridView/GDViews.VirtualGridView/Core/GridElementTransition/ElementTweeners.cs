namespace GodotViews.VirtualGrid;

public static partial class ElementTweeners
{
    public static IElementTweener None { get; } = new NoneTweenerImpl();
    public static IGodotTweenTweener CreatePositional(float duration, TweenSetup? tweenSetup = null) => 
        new PositionalTweenerImpl(duration, TweenSetups.CurrentOrDefault(tweenSetup));
}