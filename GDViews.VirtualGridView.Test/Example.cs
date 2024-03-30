using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
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

    [Export] private Button _add;
    [Export] private Button _scramble;
    [Export] private Button _grabData;
    [Export] private Button _grabView;

    private IVirtualGridView<string, View, NoExtraArgument> _virtualGridView;

    public override void _Ready()
    {
        var faker = new Faker();

        var dataList1 = new List<string>();
        var dataList2 = new List<string>();
        var dataList3 = new List<string>();
        var dataList4 = new List<string>();
        var dataList5 = new List<string>();
        
        // Scramble(dataList1);
        // Scramble(dataList2);

        _virtualGridView = VirtualGridView
            .Create(10, 10)
            .WithHandlers(
                ViewPositioners.CreateSide(),
                //ElementTweeners.None,
                //ElementFaders.None
                ElementTweeners.CreatePositional(0.25f, TweenSetups.EaseOutSine),
                ElementFaders.CreateScaleRotate(0.25f, TweenSetups.EaseOutSine)
            )
            .WithHorizontalDataLayout<string>()
                .AddRowDataSource(DataSetDefinition.Create(dataList1, Enumerable.Range(0, 20).ToArray()))
            //.WithVerticalDataLayout<string>()
            //     .AddColumnDataSource(DataSetDefinition.Create(dataList1, [0]))
            //     .AddColumnDataSource(DataSetDefinition.Create(dataList2, [1]))
            //     .AddColumnDataSource(DataSetDefinition.Create(dataList3, [2]))
            //     .AddColumnDataSource(DataSetDefinition.Create(dataList4, [3]))
            //     .AddColumnDataSource(DataSetDefinition.Create(dataList5, [4]))   
            // .WithHorizontalDataLayout<string>()
            //     .AddRowDataSource(DataSetDefinition.Create(dataList1, [0]))
            //     .AddRowDataSource(DataSetDefinition.Create(dataList2, [1]))
            //     .AddRowDataSource(DataSetDefinition.Create(dataList3, [2]))
            //     .AddRowDataSource(DataSetDefinition.Create(dataList4, [3]))
            //     .AddRowDataSource(DataSetDefinition.Create(dataList5, [4]))
            .WithArgument<View>(
                _packedScene,
                _container,
                InfinitLayoutGrids.CreateSimple(
                    new(64, 64),
                    new(10, 10)
                )
            )
            .Build();

        _virtualGridView.Redraw();

        
        _up.Pressed += () => _virtualGridView.Move(MoveDirection.Up);
        _down.Pressed += () => _virtualGridView.Move(MoveDirection.Down);
        _left.Pressed += () => _virtualGridView.Move(MoveDirection.Left);
        _right.Pressed += () => _virtualGridView.Move(MoveDirection.Right);

        var count = 0;
        
        _grabData.Pressed += () => _virtualGridView.GrabLastFocus(LastFocusType.LastDataFocus);
        _grabView.Pressed += () => _virtualGridView.GrabLastFocus(LastFocusType.LastViewFocus);
        
        _add.Pressed += () =>
        {
            // dataList1.Add(faker.Lorem.Word());
            // dataList2.Add(faker.Lorem.Word());
            // dataList3.Add(faker.Lorem.Word());
            // dataList4.Add(faker.Lorem.Word());
            // dataList5.Add(faker.Lorem.Word());

            var currentCount = count++;
            
            var countStr = $"({currentCount % 10}, {currentCount / 10})";
            
            dataList1.Add(countStr);
            dataList2.Add(countStr);
            dataList3.Add(countStr);
            dataList4.Add(countStr);
            dataList5.Add(countStr);
                
            _virtualGridView.Redraw();
        };
        _scramble.Pressed += () =>
        {
            Scramble(dataList1);
            Scramble(dataList2);
            Scramble(dataList3);
            Scramble(dataList4);
            Scramble(dataList5);
            _virtualGridView.Redraw();
        };
        
        for (int i = 0; i < 4000; i++)
        {
            _add.EmitSignal(Button.SignalName.Pressed);
        }
        
        return;

        void Scramble(List<string> list)
        {
            var length = list.Count;
            list.Clear();
            list.AddRange(GetRandom(length));
            
            return;
            
            IEnumerable<string> GetRandom(int amount)
            {
                for (var i = 0; i < amount; i++)
                {
                    yield return faker.Lorem.Word();
                }
            }
        }
    }
}