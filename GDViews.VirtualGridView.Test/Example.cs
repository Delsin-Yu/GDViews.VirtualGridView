using System;
using System.Collections.Generic;
using Godot;
using System.Linq;
using Bogus;

namespace GodotViews.VirtualGrid.Examples;

public partial class Example : Node
{
    [Export] private PackedScene _packedScene;
    [Export] private Control _container;

    [Export] private Button _up;
    [Export] private Button _down;
    [Export] private Button _left;
    [Export] private Button _right;

    [Export] private Button _scramble;

    private IVirtualGridView<string, View> _virtualGridView;

    public override void _Ready()
    {
        var faker = new Faker();

        var dataList1 = new List<string>();
        var dataList2 = new List<string>();
        
        // Scramble(dataList1);
        // Scramble(dataList2);

        _virtualGridView = VirtualGridViewFactory
            .CreateView(5, 5)
            .WithViewHandler(
                ViewHandlerFactory.CreateCenterAlignedHandler(),
                ElementTweenerFactory.CreatePositional(0.25f, TweenSetups.EaseOutCubic),
                ElementFaderFactory.CreateScale(0.25f, TweenSetups.EaseInOutSine)
            )
            .WithVerticalDataLayout<string>()
                .AddColumnDataSource(DataSetDefinitionFactory.Create(dataList1, [0, 1]))
                .AddColumnDataSource(DataSetDefinitionFactory.Create(dataList2, [2, 3, 4]))
            .WithDelegate<View>()
                .ConfigureDrawHandler((data, view) => view.Print(data))
            .WithArgument(
                _packedScene,
                _container,
                InfinitLayoutGridFactory.CreateSimple(
                    new(64, 64),
                    new(10, 10)
                )
            )
            .Build();

        _virtualGridView.Redraw();

        _up.Pressed += () =>
        {
            if (_virtualGridView.ViewRowIndex > 0) _virtualGridView.Move(MoveDirection.Up);
        };
        _down.Pressed += () => { _virtualGridView.Move(MoveDirection.Down); };
        _left.Pressed += () =>
        {
            if (_virtualGridView.ViewColumnIndex > 0) _virtualGridView.Move(MoveDirection.Left);
        };
        _right.Pressed += () => { _virtualGridView.Move(MoveDirection.Right); };

        _scramble.Pressed += () =>
        {
            dataList1.Add(faker.Lorem.Word());
            dataList2.Add(faker.Lorem.Word());
            _virtualGridView.Redraw();
        };
        return;

        void Scramble(List<string> list)
        {
            list.Clear();
            list.AddRange(GetRandom());
            
            return;
            
            IEnumerable<string> GetRandom()
            {
                var randomNum = Random.Shared.Next(5, 500);
                for (var i = 0; i < randomNum; i++)
                {
                    yield return faker.Lorem.Word();
                }
            }
        }
    }
}