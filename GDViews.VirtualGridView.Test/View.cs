using Godot;

namespace GodotViews.VirtualGrid.Examples;

public partial class View : VirtualGridViewItem<string>
{
    protected override void _OnGridItemDraw(string data, Vector2I gridPosition)
    {
        Text = data;
    }
}