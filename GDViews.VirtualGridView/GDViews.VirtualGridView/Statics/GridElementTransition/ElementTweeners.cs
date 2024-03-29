namespace GodotViews.VirtualGrid;

public static partial class ElementTweeners
{
    public static IElementTweener None { get; } = new NoneTweenerImpl();
    public static IGodotTweenTweener CreatePositional(float duration, TweenSetup tweenSetup) => new PositionalTweenerImpl(duration, tweenSetup);
}