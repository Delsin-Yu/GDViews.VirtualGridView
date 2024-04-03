using System;
using System.Collections.Generic;
using Godot;
using Bogus;
using Range = Godot.Range;

namespace GodotViews.VirtualGrid.Examples;

public partial class Main : Node, IDataSetHandler
{
    public record struct DataModel(int Index, int DataSetIndex, string Message);
    
    [Export] private PackedScene _packedScene;
    [Export] private Control _container;
    [Export] private Vector2 _size;
    [Export] private Vector2 _padding;

    [Export] private DataSetController _dataSetController1;
    [Export] private DataSetController _dataSetController2;
    [Export] private DataSetController _dataSetController3;
    [Export] private DataSetController _dataSetController4;
    [Export] private DataSetController _dataSetController5;

    [Export] private Button _grabData;
    [Export] private Button _grabView;
    [Export] private Button _killFocus;

    [Export] private OptionButton _tweenType;
    [Export] private OptionButton _faderType;
    [Export] private OptionButton _tweenerType;
    [Export] private OptionButton _positionerType;
    [Export] private Slider _duration;
    [Export] private Label _durationText;

    [Export] private CheckButton _enableClipChildren;

    private IVirtualGridView<DataModel, View, NoExtraArgument> _virtualGridView;

    private TweenSetup _currentTweenSetup;
    private IElementFader _currentFader;
    private IElementTweener _currentTweener;
    private IElementPositioner _currentPositioner;
    private float _currentDuration;

    private TweenSetup CurrentTweenSetup
    {
        get => _currentTweenSetup;
        set
        {
            _currentTweenSetup = value;
            if (CurrentFader is IGodotTweenFader currentGodotFader) currentGodotFader.TweenSetup = value;
            if (CurrentTweener is IGodotTweenTweener currentGodotTweener) currentGodotTweener.TweenSetup = value;
            _virtualGridView.Redraw();
        }
    }

    private IElementFader CurrentFader
    {
        get => _currentFader;
        set
        {
            _currentFader = value;
            _virtualGridView.ElementFader = value;
            CurrentDuration = _currentDuration;
            CurrentTweenSetup = _currentTweenSetup;
            _virtualGridView.Redraw();
        }
    }

    private IElementTweener CurrentTweener
    {
        get => _currentTweener;
        set
        {
            _currentTweener = value;
            _virtualGridView.ElementTweener = value;
            CurrentDuration = _currentDuration;
            CurrentTweenSetup = _currentTweenSetup;
            _virtualGridView.Redraw();
        }
    }

    private IElementPositioner CurrentPositioner
    {
        get => _currentPositioner;
        set
        {
            _currentPositioner = value;
            _virtualGridView.ElementPositioner = value;
            _virtualGridView.Redraw();
        }
    }

    private float CurrentDuration
    {
        get => _currentDuration;
        set
        {
            _currentDuration = value;
            if (CurrentFader is IGodotTweenFader currentGodotFader) currentGodotFader.Duration = value;
            if (CurrentTweener is IGodotTweenTweener currentGodotTweener) currentGodotTweener.Duration = value;
            _durationText.Text = value.ToString("F2");
        }
    }

    private readonly Faker _faker = new();

    public override void _Ready()
    {
        var dataSet1 = new List<DataModel>();
        var dataSet2 = new List<DataModel>();
        var dataSet3 = new List<DataModel>();
        var dataSet4 = new List<DataModel>();
        var dataSet5 = new List<DataModel>();

        _dataSetController1.Initialize(dataSet1, this, 1);
        _dataSetController2.Initialize(dataSet2, this, 2);
        _dataSetController3.Initialize(dataSet3, this, 3);
        _dataSetController4.Initialize(dataSet4, this, 4);
        _dataSetController5.Initialize(dataSet5, this, 5);

        const int tweenSetupDefaultSelection = 2;
        const int faderDefaultSelection = 1;
        const int tweenerDefaultSelection = 1;
        const int positionerDefaultSelection = 0;

        // Init Property Backings
        _currentDuration = 0.1f;
        _currentTweenSetup = _listOfTweens[tweenSetupDefaultSelection].TweenSetup;
        _currentFader = _listOfFaderTypes[faderDefaultSelection].Fader;
        _currentTweener = _listOfTweenerTypes[tweenerDefaultSelection].Tweener;
        _currentPositioner = _listOfPositionerTypes[positionerDefaultSelection].Positioner;

        HookRange(_duration, value => CurrentDuration = value);
        HookOptionButton(_listOfTweens, _tweenType, x => CurrentTweenSetup = x, tweenSetupDefaultSelection);
        HookOptionButton(_listOfFaderTypes, _faderType, x => CurrentFader = x, faderDefaultSelection);
        HookOptionButton(_listOfTweenerTypes, _tweenerType, x => CurrentTweener = x, tweenerDefaultSelection);
        HookOptionButton(_listOfPositionerTypes, _positionerType, x => CurrentPositioner = x, positionerDefaultSelection);
        HookCheckButton(_enableClipChildren, on => _container.ClipContents = on, true);
        HookButton(_grabData, () => _virtualGridView.GrabLastFocus(LastFocusType.LastDataFocus));
        HookButton(_grabView, () => _virtualGridView.GrabLastFocus(LastFocusType.LastViewFocus));
        HookButton(_killFocus, () => GetViewport().GuiReleaseFocus());

        _virtualGridView = VirtualGridView
            .Create(7, 7)
            .WithHandlers(CurrentPositioner, CurrentTweener, CurrentFader)
            .WithVerticalDataLayout<DataModel>()
                .AddColumnDataSource(DataSetDefinition.Create(dataSet1, [0, 1]))
                .AddColumnDataSource(DataSetDefinition.Create(dataSet2, [2, 3]))
                .AddColumnDataSource(DataSetDefinition.Create(dataSet3, [4]))
                .AddColumnDataSource(DataSetDefinition.Create(dataSet4, [5]))
                .AddColumnDataSource(DataSetDefinition.Create(dataSet5, [6]))
            .WithArgument<View>(
                _packedScene,
                _container,
                InfinitLayoutGrids.CreateSimple(
                    _size,
                    _padding
                )
            )
            .Build();

        CurrentDuration = _currentDuration;
        CurrentTweenSetup = _currentTweenSetup;
        
        _virtualGridView.Redraw();
        _virtualGridView.GrabLastFocus(LastFocusType.LastViewFocus);
        return;

        static void HookRange(Range range, Action<float> valueChangedHandler) => range.ValueChanged += value => valueChangedHandler((float)value);

        static void HookButton(Button button, Action onPressedHandler) => button.Pressed += onPressedHandler;

        static void HookOptionButton<T>((string, T)[] list, OptionButton optionButton, Action<T> setValueHandler, int defaultSelection)
        {
            foreach (var (name, _) in list) optionButton.AddItem(name);
            optionButton.ItemSelected += index => setValueHandler(list[index].Item2);
            optionButton.Selected = defaultSelection;
        }
        
        static void HookCheckButton(CheckButton button, BaseButton.ToggledEventHandler onToggleHandler, bool defaultValue)
        {
            button.Toggled += onToggleHandler;
            button.ButtonPressed = defaultValue;
        }
    }

    public DataModel CreateElement(int dataSetIndex, IReadOnlyList<DataModel> dataSet)
    {
        return new(dataSet.Count, dataSetIndex, _faker.Lorem.Word());
    }

    public void NotifyUpdate()
    {
        _virtualGridView.Redraw();
    }
}