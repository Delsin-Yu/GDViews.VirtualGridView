using System;
using Godot;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid;

public static partial class ElementFaders
{
    private class NonFaderImpl : IElementFader
    {
        public void Disappear(Control control, Action<Control> onFinish) => onFinish(control);

        public void Appear(Control control) { }

        public void KillTween(Control control) { }

        public void Reinitialize(Control control) { }
    }
}