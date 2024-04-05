using System;
using System.Collections.Generic;
using Godot;
using Bogus;
using GodotViews.Core.FocusFinder;
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

    [Export] private Button _grabByViewPosition;
    [Export] private Button _grabByMatching;
    [Export] private Button _grabByPattern;
    [Export] private Button _killFocus;

    [Export] private OptionButton _tweenType;
    [Export] private OptionButton _faderType;
    [Export] private OptionButton _tweenerType;
    [Export] private OptionButton _positionerType;
    [Export] private OptionButton _startPositionsType;
    [Export] private OptionButton _searchDirectionsType;
    [Export] private OptionButton _searchDataSet;
    [Export] private SpinBox _searchDataSetIndex;
    [Export] private LineEdit _matchPattern;
    [Export] private Slider _duration;
    [Export] private Label _durationText;

    [Export] private CheckButton _enableClipChildren;

    private IVirtualGridView<DataModel> _virtualGridView;

    private TweenSetup _currentTweenSetup;
    private IElementFader _currentFader;
    private IElementTweener _currentTweener;
    private IElementPositioner _currentPositioner;
    private float _currentDuration;
    private List<DataModel> _currentDataSet;

    private StartPositionHandler _currentStartPositionHandler;
    private SearchDirection _currentSearchDirection;
    
    private TweenSetup CurrentTweenSetup
    {
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
        const int startPositionHandlerDefaultSelection = 0;
        const int searchDirectionDefaultSelection = 0;
        const int searchDataSetIdDefaultSelection = 0;

        // Init Property Backings

        {
            using var lineEdit = _searchDataSetIndex.GetLineEdit();
            lineEdit.FocusMode = Control.FocusModeEnum.Click;
        }
        
        _currentDuration = 0.1f;
        _currentTweenSetup = _listOfTweens[tweenSetupDefaultSelection].TweenSetup;
        _currentFader = _listOfFaderTypes[faderDefaultSelection].Data;
        _currentTweener = _listOfTweenerTypes[tweenerDefaultSelection].Data;
        _currentPositioner = _listOfPositionerTypes[positionerDefaultSelection].Data;
        _currentStartPositionHandler = _listOfStartPositionsTypes[startPositionHandlerDefaultSelection].Data;
        _currentSearchDirection = _listOfSearchDirectionsTypes[searchDirectionDefaultSelection].Data;
        _currentDataSet = _listOfDataSets[searchDataSetIdDefaultSelection].Data switch
        {
            0 => dataSet1,
            1 => dataSet2,
            2 => dataSet3,
            3 => dataSet4,
            4 => dataSet5,
            _ => throw new ArgumentOutOfRangeException()
        };

        DataBindings.Bind(_duration, value => CurrentDuration = value, _currentDuration);
        DataBindings.Bind(_listOfTweens, _tweenType, x => CurrentTweenSetup = x, tweenSetupDefaultSelection);
        DataBindings.Bind(_listOfFaderTypes, _faderType, x => CurrentFader = x, faderDefaultSelection);
        DataBindings.Bind(_listOfTweenerTypes, _tweenerType, x => CurrentTweener = x, tweenerDefaultSelection);
        DataBindings.Bind(_listOfPositionerTypes, _positionerType, x => CurrentPositioner = x, positionerDefaultSelection);
        DataBindings.Bind(_listOfStartPositionsTypes, _startPositionsType, x => _currentStartPositionHandler = x, startPositionHandlerDefaultSelection);
        DataBindings.Bind(_listOfSearchDirectionsTypes, _searchDirectionsType, x => _currentSearchDirection = x, searchDirectionDefaultSelection);
        DataBindings.Bind(
            _listOfDataSets,
            _searchDataSet,
            x =>
            {
                _currentDataSet = GetDataSet(x);
                _searchDataSetIndex.MaxValue = _currentDataSet.Count - 1;
                _grabByMatching.Disabled = _currentDataSet.Count == 0;
            },
            searchDataSetIdDefaultSelection
        );
        DataBindings.Bind(_enableClipChildren, on => _container.ClipContents = on, true);

        DataBindings.Bind(
            _grabByViewPosition,
            () => _virtualGridView.GrabFocus(
                FocusFinders.ByPosition,
                _currentStartPositionHandler,
                _currentSearchDirection
            )
        );
        DataBindings.Bind(
            _grabByMatching,
            () => _virtualGridView.GrabFocus(
                FocusFinders.ByValue,
                _currentDataSet[(int)_searchDataSetIndex.Value]
            )
        );
        DataBindings.Bind(
            _grabByPattern,
            () => _virtualGridView.GrabFocus(
                FocusFinders.ByPredicate,
                x => x.Message.Contains(_matchPattern.Text, StringComparison.OrdinalIgnoreCase)
            )
        );
        
        DataBindings.Bind(_killFocus, () => GetViewport().GuiReleaseFocus());

        _virtualGridView = VirtualGridView
            .Create(7, 7)
            .WithHandlers(CurrentPositioner, CurrentTweener, CurrentFader)
            .WithVerticalDataLayout<DataModel>(reverseLocalLayout: false)
                .AddColumnDataSource(DataSetDefinition.Create(dataSet1, [0, 1]))
                .AddColumnDataSource(DataSetDefinition.Create(dataSet2, [2, 3]))
                .AddColumnDataSource(DataSetDefinition.Create(dataSet3, [4, 5]))
                .AddColumnDataSource(DataSetDefinition.Create(dataSet4, [6, 7]))
                .AddColumnDataSource(DataSetDefinition.Create(dataSet5, [8, 9]))
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

        NotifyUpdate();
        return;

        List<DataModel> GetDataSet(int index) => index switch
        {
            0 => dataSet1,
            1 => dataSet2,
            2 => dataSet3,
            3 => dataSet4,
            4 => dataSet5,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public DataModel CreateElement(int dataSetIndex, IReadOnlyList<DataModel> dataSet)
    {
        return new(dataSet.Count, dataSetIndex, _faker.Lorem.Word());
    }

    public void NotifyUpdate()
    {
        _virtualGridView.Redraw();
        _virtualGridView.GrabFocus();
        _searchDataSetIndex.MaxValue = _currentDataSet.Count - 1;
        _grabByMatching.Disabled = _currentDataSet.Count == 0;
    }
}

public static class DataBindings
{
    public static void Bind(Range range, Action<float> valueChangedHandler, float defaultValue)
    {
        range.ValueChanged += value => valueChangedHandler((float)value);
        range.SetValueNoSignal(defaultValue);
    }

    public static void Bind(Button button, Action onPressedHandler) => 
        button.Pressed += onPressedHandler;

    public static void Bind<T>((string, T)[] list, OptionButton optionButton, Action<T> setValueHandler, int defaultSelection)
    {
        foreach (var (name, _) in list) optionButton.AddItem(name);
        optionButton.ItemSelected += index => setValueHandler(list[index].Item2);
        optionButton.Selected = defaultSelection;
    }
        
    public static void Bind(CheckButton button, BaseButton.ToggledEventHandler onToggleHandler, bool defaultValue)
    {
        button.Toggled += onToggleHandler;
        button.ButtonPressed = defaultValue;
    }
}