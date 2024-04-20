using Godot;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using GodotViews.VirtualGrid;

namespace GDViews.VirtualGrid.Example.TMI.Recipes;

public record struct DataModel(
    string Name,
    string Description,
    string[] Tags,
    string[] Ingredients,
    string Cooker,
    float CookTime,
    int Price,
    int Level
);

public partial class TMIRecipes_Main : Control
{
    [ExportGroup("List")]
    [Export] private PackedScene _itemPrefab;
    [Export] private Control _itemContainer;
    [Export] private Vector2 _itemSize;
    [Export] private float _itemSeparation;
    [Export] private int _viewportItemCount;
    [Export] private ScrollBar _scrollBar;

    [ExportGroup("Detail")] 
    [Export] private Label _name;
    [Export] private Label _description;
    [Export] private Label _level;
    [Export] private Label _tags;

    private readonly List<DataModel> _dataSet = [];

    private IVirtualGridView<DataModel> _virtualGridView;

    public override void _Ready()
    {
        _virtualGridView =
            VirtualGridView
                .Create(1, _viewportItemCount)
                .WithHandlers(
                    ElementPositioners.Centered,
                    ElementTweeners.CreatePan(0.1f, TweenSetups.EaseOut.Quad),
                    ElementFaders.None
                ).WithVerticalDataLayout<DataModel>()
                .AppendColumnDataSet(DynamicGridViewers.CreateList(_dataSet))
                .WithArgument<TMIRecipes_GridItem, TMIRecipes_Main>(
                    _itemPrefab,
                    _itemContainer,
                    InfiniteLayoutGrids.CreateSimple(_itemSize, new(0, _itemSeparation))
                )
                .ConfigureVerticalScrollBar(
                    _scrollBar,
                    ScrollBarTweeners.CreateLerp(0.1f, TweenSetups.EaseOut.Quad)
                )
                .ConfigureExtraArgument(this)
                .Build();
        
        PopulateDataSet(_dataSet, 50);
        
        _virtualGridView.Redraw();
        _virtualGridView.GrabFocus();
    }

    public static void PopulateDataSet(IList<DataModel> dataModels, int populateAmount)
    {
        dataModels.Clear();
        var faker = new Faker();
        for (var i = 0; i < populateAmount; i++)
        {
            var name = faker.Lorem.Sentence(1, 3);
            var description = faker.Lorem.Paragraphs();
            var tags = faker.Lorem.Words(10);
            var ingredients = faker.Lorem.Words(5);
            var cooker = faker.Lorem.Word();
            var cookTime = faker.Random.Float(1, 20);
            var price = faker.Random.Int(15, 500);
            var level = faker.Random.Int(1, 6);
            dataModels.Add(
                new(
                    name,
                    description,
                    tags,
                    ingredients,
                    cooker,
                    cookTime,
                    price,
                    level
                )
            );
        }
    }

    public void Print(ref readonly DataModel data)
    {
        _name.Text = data.Name;
        _description.Text = data.Description;
        _level.Text = $"Lv {data.Level}";
        _tags.Text = string.Join("    ", data.Tags.Select(x => $"[{x}]"));
    }
}