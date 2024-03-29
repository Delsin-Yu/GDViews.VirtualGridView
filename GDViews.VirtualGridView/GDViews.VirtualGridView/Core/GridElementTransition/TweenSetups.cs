using Godot;

namespace GodotViews.VirtualGrid;

public record TweenSetup(Tween.EaseType EaseType, Tween.TransitionType TransitionType);

/// <summary>
/// https://easings.net/
/// </summary>
public static class TweenSetups
{
    public static readonly TweenSetup Linear = new(Tween.EaseType.InOut, Tween.TransitionType.Linear);
    
    public static readonly TweenSetup EaseInSine = new(Tween.EaseType.In, Tween.TransitionType.Sine);
    public static readonly TweenSetup EaseOutSine = new(Tween.EaseType.Out, Tween.TransitionType.Sine);
    public static readonly TweenSetup EaseInOutSine = new(Tween.EaseType.InOut, Tween.TransitionType.Sine);

    public static readonly TweenSetup EaseInQuad = new(Tween.EaseType.In, Tween.TransitionType.Quad);
    public static readonly TweenSetup EaseOutQuad = new(Tween.EaseType.Out, Tween.TransitionType.Quad);
    public static readonly TweenSetup EaseInOutQuad = new(Tween.EaseType.InOut, Tween.TransitionType.Quad);

    public static readonly TweenSetup EaseInCubic = new(Tween.EaseType.In, Tween.TransitionType.Cubic);
    public static readonly TweenSetup EaseOutCubic = new(Tween.EaseType.Out, Tween.TransitionType.Cubic);
    public static readonly TweenSetup EaseInOutCubic = new(Tween.EaseType.InOut, Tween.TransitionType.Cubic);

    public static readonly TweenSetup EaseInQuart = new(Tween.EaseType.In, Tween.TransitionType.Quart);
    public static readonly TweenSetup EaseOutQuart = new(Tween.EaseType.Out, Tween.TransitionType.Quart);
    public static readonly TweenSetup EaseInOutQuart = new(Tween.EaseType.InOut, Tween.TransitionType.Quart);

    public static readonly TweenSetup EaseInQuint = new(Tween.EaseType.In, Tween.TransitionType.Quint);
    public static readonly TweenSetup EaseOutQuint = new(Tween.EaseType.Out, Tween.TransitionType.Quint);
    public static readonly TweenSetup EaseInOutQuint = new(Tween.EaseType.InOut, Tween.TransitionType.Quint);

    public static readonly TweenSetup EaseInExpo = new(Tween.EaseType.In, Tween.TransitionType.Expo);
    public static readonly TweenSetup EaseOutExpo = new(Tween.EaseType.Out, Tween.TransitionType.Expo);
    public static readonly TweenSetup EaseInOutExpo = new(Tween.EaseType.InOut, Tween.TransitionType.Expo);

    public static readonly TweenSetup EaseInCirc = new(Tween.EaseType.In, Tween.TransitionType.Circ);
    public static readonly TweenSetup EaseOutCirc = new(Tween.EaseType.Out, Tween.TransitionType.Circ);
    public static readonly TweenSetup EaseInOutCirc = new(Tween.EaseType.InOut, Tween.TransitionType.Circ);

    public static readonly TweenSetup EaseInBack = new(Tween.EaseType.In, Tween.TransitionType.Back);
    public static readonly TweenSetup EaseOutBack = new(Tween.EaseType.Out, Tween.TransitionType.Back);
    public static readonly TweenSetup EaseInOutBack = new(Tween.EaseType.InOut, Tween.TransitionType.Back);

    public static readonly TweenSetup EaseInElastic = new(Tween.EaseType.In, Tween.TransitionType.Elastic);
    public static readonly TweenSetup EaseOutElastic = new(Tween.EaseType.Out, Tween.TransitionType.Elastic);
    public static readonly TweenSetup EaseInOutElastic = new(Tween.EaseType.InOut, Tween.TransitionType.Elastic);

    public static readonly TweenSetup EaseInBounce = new(Tween.EaseType.In, Tween.TransitionType.Bounce);
    public static readonly TweenSetup EaseOutBounce = new(Tween.EaseType.Out, Tween.TransitionType.Bounce);
    public static readonly TweenSetup EaseInOutBounce = new(Tween.EaseType.InOut, Tween.TransitionType.Bounce);
}