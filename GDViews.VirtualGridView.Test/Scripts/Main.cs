using System;
using System.Collections.Generic;
using Godot;
using Bogus;

namespace GodotViews.VirtualGrid.Examples;

public partial class Main : Node, IDataSetHandler
{
    public record struct DataModel(int Index, int DataSetIndex, string Message);

    #region Exports

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
    [Export] private Button _grabByDataPosition;
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

    #endregion

    #region Fields

    private IVirtualGridView<DataModel> _virtualGridView;
    private TweenSetup _currentTweenSetup;
    private IElementFader _currentFader;
    private IElementTweener _currentTweener;
    private IElementPositioner _currentPositioner;
    private float _currentDuration;
    private List<DataModel> _currentDataSet;
    private Vector2I _currentStartPosition;
    private SearchDirection _currentSearchDirection;
    private readonly Faker _faker = new();

    #endregion

    #region DataSets Field

    private readonly List<DataModel> dataSet1 = [];
    private readonly List<DataModel> dataSet2 = [];
    private readonly List<DataModel> dataSet3 = [];
    private readonly List<DataModel> dataSet4 = [];
    private readonly List<DataModel> dataSet5 = [];

    #endregion

    #region Properties

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

    #endregion

    public override void _Ready()
    {
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
        _currentStartPosition = _listOfStartPositionsTypes[startPositionHandlerDefaultSelection].Data;
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

        DataBindings.Bind(_duration, (value, instance) => instance.CurrentDuration = value, _currentDuration, this);
        DataBindings.Bind(_listOfTweens, _tweenType, (x, instance) => instance.CurrentTweenSetup = x, tweenSetupDefaultSelection, this);
        DataBindings.Bind(_listOfFaderTypes, _faderType, (x, instance) => instance.CurrentFader = x, faderDefaultSelection, this);
        DataBindings.Bind(_listOfTweenerTypes, _tweenerType, (x, instance) => instance.CurrentTweener = x, tweenerDefaultSelection, this);
        DataBindings.Bind(_listOfPositionerTypes, _positionerType, (x, instance) => instance.CurrentPositioner = x, positionerDefaultSelection, this);
        DataBindings.Bind(_listOfStartPositionsTypes, _startPositionsType, (x, instance) => instance._currentStartPosition = x, startPositionHandlerDefaultSelection, this);
        DataBindings.Bind(_listOfSearchDirectionsTypes, _searchDirectionsType, (x, instance) => instance._currentSearchDirection = x, searchDirectionDefaultSelection, this);
        DataBindings.Bind(
            _listOfDataSets,
            _searchDataSet,
            (x, instance) =>
            {
                instance._currentDataSet = instance.GetDataSet(x);
                instance._searchDataSetIndex.MaxValue = instance._currentDataSet.Count - 1;
                instance._grabByMatching.Disabled = instance._currentDataSet.Count == 0;
            },
            searchDataSetIdDefaultSelection,
            this
        );
        DataBindings.Bind(_enableClipChildren, on => _container.ClipContents = on, true);

        DataBindings.Bind(_grabByViewPosition, instance => instance._virtualGridView.GrabFocus(FocusPresets.ViewPosition, instance._currentStartPosition, instance._currentSearchDirection), this);
        DataBindings.Bind(_grabByDataPosition, instance => instance._virtualGridView.GrabFocus(FocusPresets.DataPosition, instance._currentStartPosition, instance._currentSearchDirection), this);
        DataBindings.Bind(_grabByMatching, instance => instance._virtualGridView.GrabFocus(FocusFiners.Value, instance._currentDataSet[(int)instance._searchDataSetIndex.Value]), this);
        DataBindings.Bind(_grabByPattern, instance => instance._virtualGridView.GrabFocus(FocusFiners.Predicate, x => x.Message.Contains(instance._matchPattern.Text, StringComparison.OrdinalIgnoreCase)), this);
        DataBindings.Bind(_killFocus, instance => instance.GetViewport().GuiReleaseFocus(), this);

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
    }

    private List<DataModel> GetDataSet(int index) =>
        index switch
        {
            0 => dataSet1,
            1 => dataSet2,
            2 => dataSet3,
            3 => dataSet4,
            4 => dataSet5,
            _ => throw new ArgumentOutOfRangeException()
        };

    public DataModel CreateElement(int dataSetIndex, IReadOnlyList<DataModel> dataSet) => new(dataSet.Count, dataSetIndex, _faker.Lorem.Word());

    public void NotifyUpdate()
    {
        _virtualGridView.Redraw();
        _virtualGridView.GrabFocus();
        _searchDataSetIndex.MaxValue = _currentDataSet.Count - 1;
        _grabByMatching.Disabled = _currentDataSet.Count == 0;
    }
}