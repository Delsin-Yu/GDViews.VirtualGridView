using System;
using Godot;

namespace GodotViews.VirtualGrid;

public interface IElementFader
{
    void Disappear(Control control, Action<Control> onFinish);
    void Appear(Control control);
    void KillTween(Control control);
    void Show(Control control);
}