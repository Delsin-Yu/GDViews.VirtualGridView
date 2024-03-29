namespace GodotViews.VirtualGrid;

public static partial class ElementFaders
{
    public static IElementFader None { get; } = new NonFaderImpl();
    public static IGodotTweenFader CreateFade(float duration, TweenSetup tweenSetup) => new FadeFader(duration, tweenSetup);
    public static IGodotTweenFader CreateScale(float duration, TweenSetup tweenSetup) => new ScaleFader(duration, tweenSetup);
    public static IGodotTweenFader CreateScaleRotate(float duration, TweenSetup tweenSetup) => new ScaleRotateFader(duration, tweenSetup);
}