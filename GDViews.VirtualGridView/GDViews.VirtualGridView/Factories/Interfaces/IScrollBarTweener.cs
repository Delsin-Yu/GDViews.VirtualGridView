using Godot;

namespace GodotViews.VirtualGrid;

public interface IScrollBarTweener
{
    void UpdateValue(ScrollBar scrollBar, float targetValue, float targetPage);
    void KillTween(ScrollBar scrollBar);
}