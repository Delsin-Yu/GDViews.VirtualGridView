using System.Collections.Generic;
using Bogus;
using Godot;

namespace GodotViews.VirtualGrid.Examples;

public partial class LogView : Node
{
    [Export] private PackedScene _prefab;
    [Export] private Control _container;
    [Export] private int _displayAmount;
    [Export] private Vector2 _size;
    [Export] private Vector2 _padding;
    [Export] private Button _add;
    [Export] private Button _add10000;
    [Export] private Label _total;

    private IVirtualGridView<LogInfo, LogViewItem, LogView> _virtualGridView;

    private readonly List<LogInfo> _logs = [];

    public override void _Ready()
    {
        var faker = new Faker();
        LogInfo CreateItem()
        {
            return new(
                faker.Date.RecentDateOnly().ToShortDateString(),
                faker.Address.Country(),
                faker.Lorem.Sentence()
            );
        }

        for (int i = 0; i < 5; i++)
        {
            _logs.Add(CreateItem());
        }

        _total.Text = _logs.Count.ToString();
        
        _add.Pressed += () =>
        {
            _logs.Add(
                CreateItem()
            );
            _virtualGridView.Redraw();
            _total.Text = _logs.Count.ToString();
        };
               
        _add10000.Pressed += () =>
        {
            for (int i = 0; i < 10000; i++)
            {
                _logs.Add(
                    CreateItem()
                );    
            }
            _virtualGridView.Redraw();
            _total.Text = _logs.Count.ToString();
        };
        
        _virtualGridView = VirtualGridView.Create(1, _displayAmount)
            .WithHandlers(
                ViewPositioners.CreateSide(),
                ElementTweeners.CreatePositional(0.15f, TweenSetups.EaseOutSine),
                ElementFaders.CreateScale(0.25f, TweenSetups.EaseInOutSine)
            ).WithVerticalDataLayout<LogInfo>()
            .AddColumnDataSource(DataSetDefinition.Create(_logs, [0]))
            .WithArgument<LogViewItem, LogView>(_prefab, _container, InfinitLayoutGrids.CreateSimple(_size, _padding))
            .ConfigureExtraArgument(this)
            .Build();
        
        _virtualGridView.Redraw();

        _virtualGridView.GrabLastFocus(LastFocusType.LastViewFocus);
    }
}