using System;
using Godot;

namespace GodotViews.VirtualGrid;

public static class ElementFaderFactory
{
    public static IElementFader None { get; } = new NonFaderImpl();
    public static IGodotTweenFader CreateFade(float duration, TweenSetup tweenSetup) => new FadeFader(duration, tweenSetup);
    public static IGodotTweenFader CreateScale(float duration, TweenSetup tweenSetup) => new ScaleFader(duration, tweenSetup);

    private class NonFaderImpl : IElementFader
    {
        public void Disappear(Control control, Action<Control> onFinish) => onFinish(control);

        public void Appear(Control control)
        {
        }

        public void KillTween(Control control)
        {
        }
    }
}