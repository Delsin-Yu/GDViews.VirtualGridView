namespace GodotViews.VirtualGrid.Examples;

public partial class Main
{
    private readonly (string TweenName, TweenSetup TweenSetup)[] _listOfTweens =
    {
        ("Linear", TweenSetups.Linear),
        ("Ease In Sine", TweenSetups.EaseInSine),
        ("Ease Out Sine", TweenSetups.EaseOutSine),
        ("Ease In & Out Sine", TweenSetups.EaseInOutSine),
        ("Ease In Quad", TweenSetups.EaseInQuad),
        ("Ease Out Quad", TweenSetups.EaseOutQuad),
        ("Ease In & Out Quad", TweenSetups.EaseInOutQuad),
        ("Ease In Cubic", TweenSetups.EaseInCubic),
        ("Ease Out Cubic", TweenSetups.EaseOutCubic),
        ("Ease In & Out Cubic", TweenSetups.EaseInOutCubic),
        ("Ease In Quart", TweenSetups.EaseInQuart),
        ("Ease Out Quart", TweenSetups.EaseOutQuart),
        ("Ease In & Out Quart", TweenSetups.EaseInOutQuart),
        ("Ease In Quint", TweenSetups.EaseInQuint),
        ("Ease Out Quint", TweenSetups.EaseOutQuint),
        ("Ease In & Out Quint", TweenSetups.EaseInOutQuint),
        ("Ease In Expo", TweenSetups.EaseInExpo),
        ("Ease Out Expo", TweenSetups.EaseOutExpo),
        ("Ease In & Out Expo", TweenSetups.EaseInOutExpo),
        ("Ease In Circ", TweenSetups.EaseInCirc),
        ("Ease Out Circ", TweenSetups.EaseOutCirc),
        ("Ease In & Out Circ", TweenSetups.EaseInOutCirc),
        ("Ease In Back", TweenSetups.EaseInBack),
        ("Ease Out Back", TweenSetups.EaseOutBack),
        ("Ease In & Out Back", TweenSetups.EaseInOutBack),
        ("Ease In Elastic", TweenSetups.EaseInElastic),
        ("Ease Out Elastic", TweenSetups.EaseOutElastic),
        ("Ease In & Out Elastic", TweenSetups.EaseInOutElastic),
        ("Ease In Bounce", TweenSetups.EaseInBounce),
        ("Ease Out Bounce", TweenSetups.EaseOutBounce),
        ("Ease In & Out Bounce", TweenSetups.EaseInOutBounce),
    };

    private readonly (string FaderName, IElementFader Fader)[] _listOfFaderTypes =
    {
        ("None", ElementFaders.None),
        ("Fade", ElementFaders.CreateFade(0f)),
        ("Scale", ElementFaders.CreateScale(0f)),
        ("Scale & Rotate", ElementFaders.CreateScaleRotate(0f)),
    };

    private readonly (string TweenerName, IElementTweener Tweener)[] _listOfTweenerTypes =
    {
        ("None", ElementTweeners.None),
        ("Positional", ElementTweeners.CreatePositional(0f)),
    };

    private readonly (string PositionerName, IElementPositioner Positioner)[] _listOfPositionerTypes =
    {
        ("Side", ElementPositioners.CreateSide()),
        ("Centered", ElementPositioners.CreateCentered()),
    };
}