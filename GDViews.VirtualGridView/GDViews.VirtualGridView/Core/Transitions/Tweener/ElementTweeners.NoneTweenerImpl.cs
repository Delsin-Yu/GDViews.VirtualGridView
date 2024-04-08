using System;
using Godot;

namespace GodotViews.VirtualGrid;

public static partial class ElementTweeners
{
    private class NoneTweenerImpl : IElementTweener
    {
        public void MoveTo(Control control, Vector2 targetPosition) => control.Position = targetPosition;

        public void MoveIn(Control control, Vector2 targetPosition) => control.Position = targetPosition;

        public void MoveOut(Control control, Vector2 targetPosition, Action<Control> onFinish)
        {
            control.Position = targetPosition;
            onFinish.Invoke(control);
        }

        public void KillTween(Control control) { }
    }
}