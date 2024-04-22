using Godot;

namespace GDViews.VirtualGrid.Example.TMI;

public partial class TextView : Control
{
    [Export] private Label _label;

    public void Print(string text) => _label.Text = text;
}