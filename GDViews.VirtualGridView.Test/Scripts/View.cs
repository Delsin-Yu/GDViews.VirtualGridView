using System;
using System.Linq;
using System.Text;
using Godot;

namespace GodotViews.VirtualGrid.Examples;

public partial class View : VirtualGridViewItem<Main.DataModel>
{
    protected override void _OnGridItemDraw(Main.DataModel data, Vector2I gridPosition)
    {
        Text = $"{data.Index:D2}\n{data.Message}";

        var hue = data.DataSetIndex / 5f;
        var saturation = Mathf.Lerp(0.2f, 1f, Flip(data.Index, 10));
        SelfModulate = Color.FromHsv(
            hue,
            saturation,
            1f
        );
    }


    private static float Flip(float value, float pageSize)
    {
        var result = value / pageSize;
        result -= (int)result;
        return result;
    }
}