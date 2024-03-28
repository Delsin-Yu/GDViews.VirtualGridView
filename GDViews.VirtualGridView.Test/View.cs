using Godot;

namespace GodotViews.VirtualGrid.Examples;

public partial class View : Button
{
    public void Print(string value)
    {
        Text = value;
    }
}