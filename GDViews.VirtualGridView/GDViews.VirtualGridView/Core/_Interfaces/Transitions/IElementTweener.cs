using System;
using Godot;

namespace GodotViews.VirtualGrid;

public interface IElementTweener
{
    void MoveTo(Control control, Vector2 targetPosition);
    void MoveIn(Control control, Vector2 targetPosition);
    void MoveOut(Control control, Vector2 targetPosition, Action<Control> onFinish);
    void KillTween(Control control);
}