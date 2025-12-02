using Godot;
using System;
using System.Collections.Generic;
using System.Globalization;
using Bogus;
using GodotViews.VirtualGrid;

namespace GDViews.VirtualGrid.Example.TMI.Storage;

public record struct DataModel(
    string Name,
    string Description,
    string[] Tags,
    int Price,
    int Level,
    int Count
);

public partial class TMIStorage_Main : Node
{
    [ExportGroup("List")] [Export] private PackedScene _itemPrefab;
    [Export] private Control _itemContainer;
    [Export] private Vector2 _itemSize;
    [Export] private Vector2 _itemSeparation;
    [Export] private Vector2I _viewportItemCount;
    [Export] private ScrollBar _scrollBar;
    
    
    [ExportGroup("Detail")] [Export] private Label _name;
    [Export] private Label _description;
    [Export] private Label _level;
    [Export] private Label _price;
    [Export] private Control _tagContainer;
    [Export] private PackedScene _tagPrefab;
    
    private TextPrinter _tagsPrinter;
    
    private readonly List<DataModel> _dataSet1 = [];
    private readonly List<DataModel> _dataSet2 = [];
    private readonly List<DataModel> _dataSet3 = [];
    private readonly List<DataModel> _dataSet4 = [];

    private IVirtualGridView<DataModel> _virtualGridView;

    public override void _Ready()
    {
        var gridViewer1 = DynamicGridViewers.CreateList(_dataSet1);
        var gridViewer2 = DynamicGridViewers.CreateList(_dataSet2);
        var gridViewer3 = DynamicGridViewers.CreateList(_dataSet3);
        var gridViewer4 = DynamicGridViewers.CreateList(_dataSet4);

        _tagsPrinter = new(_tagContainer, _tagPrefab);
        _virtualGridView =
            VirtualGridView
                .Create(_viewportItemCount.X, _viewportItemCount.Y)
                .WithHandlers(
                    ElementPositioners.Side,
                    ElementTweeners.CreatePan(0.1f, TweenSetups.EaseOut.Quad),
                    ElementFaders.None
                )
                .WithVerticalDataLayout<DataModel>()
                    .AppendDataSet(gridViewer1)
                    .AppendDataSet(gridViewer2)
                    .AppendDataSet(gridViewer3)
                    .AppendDataSet(gridViewer4)
                    .AppendDataSet(gridViewer4)
                .WithArgument<TMIStorage_GridItem, TMIStorage_Main>(
                    _itemPrefab,
                    _itemContainer,
                    InfiniteLayoutGrids.CreateSimple(_itemSize, new(_itemSeparation.X, _itemSeparation.Y)),
                    this
                )
                    .ConfigureVerticalScrollBar(_scrollBar, ScrollBarTweeners.CreateLerp(0.1f, TweenSetups.EaseOut.Quad))
                .Build();
        
        PopulateDataSet(_dataSet1, Random.Shared.Next(5, 30));
        PopulateDataSet(_dataSet2, Random.Shared.Next(5, 30));
        PopulateDataSet(_dataSet3, Random.Shared.Next(5, 30));
        PopulateDataSet(_dataSet4, Random.Shared.Next(5, 30));

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
            var price = faker.Random.Int(15, 500);
            var level = faker.Random.Int(1, 6);
            var count = faker.Random.Int(0, 200);
            dataModels.Add(
                new(
                    name,
                    description,
                    tags,
                    price,
                    level,
                    count
                )
            );
        }
    }
    
    
    public void Print(ref readonly DataModel data)
    {
        _name.Text = data.Name;
        _description.Text = data.Description;
        _level.Text = $"Lv {data.Level}";
        _price.Text = data.Price.ToString("C0", CultureInfo.CurrentCulture);
        _tagsPrinter.Draw(data.Tags);
    }
}
