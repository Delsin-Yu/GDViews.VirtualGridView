using Godot;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid;

public static partial class ScrollBarTweeners
{
    private class NoneImpl : IScrollBarTweener
    {
        public void UpdateValue(ScrollBar scrollBar, float targetValue, float targetPage)
        {
            scrollBar.Value = targetValue;
            scrollBar.Page = targetPage;
        }

        public void KillTween(ScrollBar scrollBar) { }
    }
}