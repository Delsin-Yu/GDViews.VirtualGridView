using Godot;
using GodotViews.VirtualGrid;

namespace GDViews.VirtualGrid.Example.TMI.Storage;

public partial class TMIStorage_GridItem : VirtualGridViewItem<DataModel, TMIStorage_Main>
{
    [Export] private Label _count;

    protected override void _OnGridItemDraw(DataModel data, Vector2I viewPosition, TMIStorage_Main extraArgument)
    {
        Text = data.Name;
        _count.Text = data.Count.ToString();
    }
    
    protected override void _OnGridItemFocusEntered(DataModel data, Vector2I viewPosition, TMIStorage_Main extraArgument)
    {
        extraArgument.Print(in data);
    }
}