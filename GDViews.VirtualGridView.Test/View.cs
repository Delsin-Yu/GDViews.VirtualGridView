using System;
using System.Linq;
using System.Text;
using Godot;

namespace GodotViews.VirtualGrid.Examples;

public partial class View : VirtualGridViewItem<string>
{
    protected override void _OnGridItemDraw(string data, Vector2I gridPosition)
    {
        Text = data;
        var length = Encoding.UTF8.GetBytes(data).Sum(x => x);
        SelfModulate = Color.FromHsv(
            length / 60f,
            0.6f,
            0.6f
        );
    }
}