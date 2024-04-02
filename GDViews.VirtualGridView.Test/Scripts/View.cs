using System;
using System.Linq;
using System.Text;
using Godot;

namespace GodotViews.VirtualGrid.Examples;

public partial class View : VirtualGridViewItem<Main.DataModel>
{
    private const float _saturation = 0.8f;
    private const float _value = 1.0f;
    
    private static readonly Color[] _colors =
    {
        Color.FromHsv(0 / 7f, _saturation, _value),
        Color.FromHsv(1 / 7f, _saturation, _value),
        Color.FromHsv(2 / 7f, _saturation, _value),
        Color.FromHsv(3 / 7f, _saturation, _value),
        Color.FromHsv(4 / 7f, _saturation, _value),
        Color.FromHsv(5 / 7f, _saturation, _value),
        Color.FromHsv(6 / 7f, _saturation, _value),
    };
    
    protected override void _OnGridItemDraw(Main.DataModel data, Vector2I gridPosition)
    {
        Text = $"{data.Index:D3}\n{data.Message}";
        SelfModulate = _colors[data.DataSetIndex];
    }
}