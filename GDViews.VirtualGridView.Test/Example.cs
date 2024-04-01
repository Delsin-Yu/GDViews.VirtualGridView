using System.Collections.Generic;
using System.Linq;
using Godot;
using Bogus;

namespace GodotViews.VirtualGrid.Examples;

public partial class Example : Node
{
    [Export] private PackedScene _packedScene;
    [Export] private Control _container;
    [Export] private Vector2 _size;
    [Export] private Vector2 _padding;

    [Export] private Button _up;
    [Export] private Button _down;
    [Export] private Button _left;
    [Export] private Button _right;

    [Export] private Button _add;
    [Export] private Button _scramble;
    [Export] private Button _grabData;
    [Export] private Button _grabView;
    [Export] private Button _killFocus;

    [Export] private OptionButton _optionButton;
    [Export] private Slider _duration;
    [Export] private Label _durationText;

    [Export] private CheckButton _enableClipChildren;

    private IVirtualGridView<string, View, NoExtraArgument> _virtualGridView;

    public override void _Ready()
    {
        var faker = new Faker();

        var dataList1 = new List<string>();
        var dataList2 = new List<string>();
        var dataList3 = new List<string>();
        var dataList4 = new List<string>();
        var dataList5 = new List<string>();

        var count = 0;

        string Add()
        {
            var currentCount = count++;

            var countStr = $"({currentCount % 100}, {currentCount / 100})";

            dataList1.Add(countStr);
            dataList2.Add(countStr);
            dataList3.Add(countStr);
            dataList4.Add(countStr);
            dataList5.Add(countStr);

            return countStr;
        }


        for (var i = 0; i < 100 * 100; i++)
        {
            Add();
        }

        // Scramble(dataList1);
        // Scramble(dataList2);

        _enableClipChildren.Toggled += on => { _container.ClipContents = on; };

        _enableClipChildren.ButtonPressed = true;

        var listOfEases = new[]
        {
            ("Linear", TweenSetups.Linear),
            ("EaseInSine", TweenSetups.EaseInSine),
            ("EaseOutSine", TweenSetups.EaseOutSine),
            ("EaseInOutSine", TweenSetups.EaseInOutSine),
            ("EaseInQuad", TweenSetups.EaseInQuad),
            ("EaseOutQuad", TweenSetups.EaseOutQuad),
            ("EaseInOutQuad", TweenSetups.EaseInOutQuad),
            ("EaseInCubic", TweenSetups.EaseInCubic),
            ("EaseOutCubic", TweenSetups.EaseOutCubic),
            ("EaseInOutCubic", TweenSetups.EaseInOutCubic),
            ("EaseInQuart", TweenSetups.EaseInQuart),
            ("EaseOutQuart", TweenSetups.EaseOutQuart),
            ("EaseInOutQuart", TweenSetups.EaseInOutQuart),
            ("EaseInQuint", TweenSetups.EaseInQuint),
            ("EaseOutQuint", TweenSetups.EaseOutQuint),
            ("EaseInOutQuint", TweenSetups.EaseInOutQuint),
            ("EaseInExpo", TweenSetups.EaseInExpo),
            ("EaseOutExpo", TweenSetups.EaseOutExpo),
            ("EaseInOutExpo", TweenSetups.EaseInOutExpo),
            ("EaseInCirc", TweenSetups.EaseInCirc),
            ("EaseOutCirc", TweenSetups.EaseOutCirc),
            ("EaseInOutCirc", TweenSetups.EaseInOutCirc),
            ("EaseInBack", TweenSetups.EaseInBack),
            ("EaseOutBack", TweenSetups.EaseOutBack),
            ("EaseInOutBack", TweenSetups.EaseInOutBack),
            ("EaseInElastic", TweenSetups.EaseInElastic),
            ("EaseOutElastic", TweenSetups.EaseOutElastic),
            ("EaseInOutElastic", TweenSetups.EaseInOutElastic),
            ("EaseInBounce", TweenSetups.EaseInBounce),
            ("EaseOutBounce", TweenSetups.EaseOutBounce),
            ("EaseInOutBounce", TweenSetups.EaseInOutBounce),
        };

        foreach (var ease in listOfEases)
        {
            _optionButton.AddItem(ease.Item1);
        }

        var positionalTweener = ElementTweeners.CreatePositional(0.25f, TweenSetups.EaseOutSine);

        _optionButton.ItemSelected += index => { positionalTweener.TweenSetup = listOfEases[index].Item2; };

        _optionButton.Selected = 2;

        _duration.ValueChanged += value =>
        {
            positionalTweener.Duration = (float)value;
            _durationText.Text = value.ToString("F2");
        };

        _duration.Value = 0.25f;

        _virtualGridView = VirtualGridView
            .Create(7, 7)
            .WithHandlers(
                ViewPositioners.CreateCentered(),
                //ViewPositioners.CreateSide(),
                //ElementTweeners.None,
                //ElementFaders.None
                positionalTweener,
                ElementFaders.CreateScaleRotate(0.25f, TweenSetups.EaseOutSine)
            )
            .WithVerticalDataLayout<string>()
            .AddColumnDataSource(DataSetDefinition.Create(dataList1, Enumerable.Range(0, 100).ToArray()))
            //.WithHorizontalDataLayout<string>()
            //    .AddRowDataSource(DataSetDefinition.Create(dataList1, Enumerable.Range(0, 12).ToArray()))
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
                    _size,
                    _padding
                )
            )
            .Build();

        _virtualGridView.Redraw();

        static void SimulateInput(string name, bool pressed)
        {
            var action = new InputEventAction { Action = name, Pressed = pressed };
            Input.ParseInputEvent(action);
        }

        _up.Pressed += () => SimulateInput("ui_up", true);
        _down.Pressed += () => SimulateInput("ui_down", true);
        _left.Pressed += () => SimulateInput("ui_left", true);
        _right.Pressed += () => SimulateInput("ui_right", true);

        _grabData.Pressed += () => _virtualGridView.GrabLastFocus(LastFocusType.LastDataFocus);
        _grabView.Pressed += () => _virtualGridView.GrabLastFocus(LastFocusType.LastViewFocus);
        _killFocus.Pressed += () => GetViewport().GuiReleaseFocus();
        
        _add.Pressed += () =>
        {
            // dataList1.Add(faker.Lorem.Word());
            // dataList2.Add(faker.Lorem.Word());
            // dataList3.Add(faker.Lorem.Word());
            // dataList4.Add(faker.Lorem.Word());
            // dataList5.Add(faker.Lorem.Word());

            var newValue = Add();

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

        _virtualGridView.GrabLastFocus(LastFocusType.LastViewFocus);

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