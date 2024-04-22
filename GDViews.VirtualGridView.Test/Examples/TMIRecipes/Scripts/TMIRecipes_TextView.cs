using Godot;

namespace GDViews.VirtualGrid.Example.TMI.Recipes;

public partial class TMIRecipes_TextView : Control
{
    [Export] private Label _label;

    public void Print(string text) => _label.Text = text;
}