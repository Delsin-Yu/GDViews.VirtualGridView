using Godot;

namespace GodotViews.VirtualGrid;

public abstract class GodotTweenCoreBasedScrollBarTweener<TCachedArgument> : IScrollBarTweener, ITweenCoreUser<NoExtraArgument, (float targetValue, float targetPage), TCachedArgument>
{
    private readonly GodotTweenCore<NoExtraArgument, (float targetValue, float targetPage), TCachedArgument> _tweenCore;

    protected GodotTweenCoreBasedScrollBarTweener()
    {
        _tweenCore = new(this);
    }


    /// <inheritdoc/>
    public void UpdateValue(ScrollBar scrollBar, float targetValue, float targetPage) => _tweenCore.KillAndCreateNewTween(NoExtraArgument.Default, scrollBar, (targetValue, targetPage), null, "Update Scroll Bar Value");

    public void KillTween(ScrollBar scrollBar) => _tweenCore.KillTween(scrollBar);

    public bool IsTweenSupported(NoExtraArgument tweenType) => true;
    public void ResetControl(Control control, TCachedArgument previousTarget) => ResetScrollBar((ScrollBar)control, previousTarget);
    public TCachedArgument InitializeTween(NoExtraArgument tweenType, in (float targetValue, float targetPage) targetValue, Control control, Tween tween) => InitializeTween(in targetValue, (ScrollBar)control, tween);

    public abstract TCachedArgument InitializeTween(in (float targetValue, float targetPage) targetValue, ScrollBar scrollBar, Tween tween);
    protected abstract void ResetScrollBar(ScrollBar scrollBar, TCachedArgument previousTarget);
}