namespace GodotViews.VirtualGrid;

public static partial class ElementFaders
{
    public static IElementFader None = new NonFaderImpl();
    public static IGodotTweenFader CreateFade(float duration, TweenSetup? tweenSetup = null) => 
        new FadeFader(duration, TweenSetups.CurrentOrDefault(tweenSetup));
    public static IGodotTweenFader CreateScale(float duration, TweenSetup? tweenSetup = null) => 
        new ScaleFader(duration, TweenSetups.CurrentOrDefault(tweenSetup));
    public static IGodotTweenFader CreateScaleRotate(float duration, TweenSetup? tweenSetup = null) => 
        new ScaleRotateFader(duration, TweenSetups.CurrentOrDefault(tweenSetup));
}