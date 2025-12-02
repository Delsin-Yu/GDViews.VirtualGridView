using System.Globalization;
using Godot;
using GodotViews.VirtualGrid;

namespace GDViews.VirtualGrid.Example.TMI.Recipes;

public partial class TMIRecipes_GridItem : VirtualGridViewItemArg<DataModel, TMIRecipes_Main>
{
    [Export] private Label _name;
    [Export] private Label _price;

    protected override void _OnGridItemDraw(DataModel data, Vector2I viewPosition, TMIRecipes_Main extraArgument)
    {
        _name.Text = data.Name;
        _price.Text = data.Price.ToString("C0", CultureInfo.CurrentCulture);
    }

    protected override void _OnGridItemFocusEntered(DataModel data, Vector2I viewPosition, TMIRecipes_Main extraArgument)
    {
        extraArgument.Print(in data);
    }
}