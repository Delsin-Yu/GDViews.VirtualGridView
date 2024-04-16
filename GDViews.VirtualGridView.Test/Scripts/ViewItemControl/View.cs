using Godot;

namespace GodotViews.VirtualGrid.Examples;

public partial class View : VirtualGridViewItem<DataModel>
{
    protected override void _OnGridItemDraw(DataModel data, Vector2I viewPosition)
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