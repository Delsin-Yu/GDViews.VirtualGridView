using System;
using System.Collections.Generic;
using Godot;
using Bogus;
using GodotViews.VirtualGrid.FocusFinding;
using GodotViews.VirtualGrid.Transition.GodotTween;
using GodotViews.VirtualGrid.Positioner;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid.Examples.Playground;

public partial class Playground_Main : Node
{

    #region Exports

    [Export] private PackedScene _packedScene;
    [Export] private Control _container;
    [Export] private ScrollBar _verticalScrollBar;
    [Export] private ScrollBar _horizontalScrollBar;
    [Export] private Vector2 _size;
    [Export] private Vector2 _padding;

    [Export] private Playground_DataSetController _dataSetController1;
    [Export] private Playground_DataSetController _dataSetController2;
    [Export] private Playground_DataSetController _dataSetController3;
    [Export] private Playground_DataSetController _dataSetController4;
    [Export] private Playground_DataSetController _dataSetController5;

    [Export] private Button _grabByViewPosition;
    [Export] private Button _grabByDataPosition;
    [Export] private Button _grabByMatching;
    [Export] private Button _grabByPattern;
    [Export] private Button _killFocus;

    [Export] private OptionButton _tweenType;
    [Export] private OptionButton _faderType;
    [Export] private OptionButton _tweenerType;
    [Export] private OptionButton _scrollBarTweenerType;
    [Export] private OptionButton _positionerType;
    [Export] private OptionButton _startPositionsType;
    [Export] private OptionButton _searchDirectionsType;
    [Export] private OptionButton _searchDataSet;
    [Export] private SpinBox _searchDataSetIndex;
    [Export] private LineEdit _matchPattern;
    [Export] private Slider _duration;
    [Export] private Label _durationText;

    [Export] private CheckButton _enableClipChildren;
    [Export] private CheckButton _autoHideScrollBar;

    #endregion

    #region Fields

    private IVirtualGridView<DataModel> _virtualGridView;
    private TweenSetup _currentTweenSetup;
    private IElementFader _currentFader;
    private IElementTweener _currentTweener;
    private IScrollBarTweener _currentScrollBarTweener;
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
            if (CurrentFader is IGodotTween godotTweenFader) godotTweenFader.TweenSetup = value;
            if (CurrentTweener is IGodotTween godotTweenTweener) godotTweenTweener.TweenSetup = value;
            if (CurrentScrollBarTweener is IGodotTween godotTweenScrollBarTweener) godotTweenScrollBarTweener.TweenSetup = value;
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
            _virtualGridView.HScrollBarFader = value;
            _virtualGridView.VScrollBarFader = value;
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

    private IScrollBarTweener CurrentScrollBarTweener
    {
        get => _currentScrollBarTweener;
        set
        {
            _currentScrollBarTweener = value;
            _virtualGridView.HScrollBarTweener = value;
            _virtualGridView.VScrollBarTweener = value;
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
            if (CurrentFader is IGodotTween godotTweenFader) godotTweenFader.Duration = value;
            if (CurrentTweener is IGodotTween godotTweenTweener) godotTweenTweener.Duration = value;
            if (CurrentScrollBarTweener is IGodotTween godotTweenScrollBarTweener) godotTweenScrollBarTweener.Duration = value;
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
        const int scrollBarTweenerDefaultSelection = 1;
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
        _currentScrollBarTweener = _listOfScrollBarTweenerTypes[scrollBarTweenerDefaultSelection].Data;
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

        DataBindingUtility.Bind(_duration, static (value, instance) => instance.CurrentDuration = value, _currentDuration, this);
        DataBindingUtility.Bind(_listOfTweens, _tweenType, static (x, instance) => instance.CurrentTweenSetup = x, tweenSetupDefaultSelection, this);
        DataBindingUtility.Bind(_listOfFaderTypes, _faderType, static (x, instance) => instance.CurrentFader = x, faderDefaultSelection, this);
        DataBindingUtility.Bind(_listOfTweenerTypes, _tweenerType, static (x, instance) => instance.CurrentTweener = x, tweenerDefaultSelection, this);
        DataBindingUtility.Bind(_listOfScrollBarTweenerTypes, _scrollBarTweenerType, static (x, instance) => instance.CurrentScrollBarTweener = x, scrollBarTweenerDefaultSelection, this);
        DataBindingUtility.Bind(_listOfPositionerTypes, _positionerType, static (x, instance) => instance.CurrentPositioner = x, positionerDefaultSelection, this);
        DataBindingUtility.Bind(_listOfStartPositionsTypes, _startPositionsType, static (x, instance) => instance._currentStartPosition = x, startPositionHandlerDefaultSelection, this);
        DataBindingUtility.Bind(_listOfSearchDirectionsTypes, _searchDirectionsType, static (x, instance) => instance._currentSearchDirection = x, searchDirectionDefaultSelection, this);
        DataBindingUtility.Bind(
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

        DataBindingUtility.Bind(_enableClipChildren, (on, instance) => instance._container.ClipContents = on, true, this);
        DataBindingUtility.Bind(
            _autoHideScrollBar,
            (on, instance) =>
            {
                instance._virtualGridView.AutoHideHScrollBar = on;
                instance._virtualGridView.AutoHideVScrollBar = on;
                instance._virtualGridView.Redraw();
            },
            false,
            this
        );

        DataBindingUtility.Bind(_grabByViewPosition, static instance => instance._virtualGridView.GrabFocus(FocusPresets.ViewPosition, instance._currentStartPosition, instance._currentSearchDirection), this);
        DataBindingUtility.Bind(_grabByDataPosition, static instance => instance._virtualGridView.GrabFocus(FocusPresets.DataPosition, instance._currentStartPosition, instance._currentSearchDirection), this);
        DataBindingUtility.Bind(_grabByMatching, static instance => instance._virtualGridView.GrabFocus(FocusPresets.Value, instance._currentDataSet[(int)instance._searchDataSetIndex.Value]), this);
        DataBindingUtility.Bind(_grabByPattern, static instance => instance._virtualGridView.GrabFocus(FocusPresets.Predicate, static (x, instance) => x.Message.Contains(instance._matchPattern.Text, StringComparison.OrdinalIgnoreCase), instance), this);
        DataBindingUtility.Bind(_killFocus, static instance => instance.GetViewport().GuiReleaseFocus(), this);

        var dataSet1GridView = DynamicGridViewers.CreateList(dataSet1);
        var dataSet2GridView = DynamicGridViewers.CreateList(dataSet2);
        var dataSet3GridView = DynamicGridViewers.CreateList(dataSet3);
        var dataSet4GridView = DynamicGridViewers.CreateList(dataSet4);
        var dataSet5GridView = DynamicGridViewers.CreateList(dataSet5);

        _virtualGridView = VirtualGridView
            .Create(7, 7)
            .WithHandlers(CurrentPositioner, CurrentTweener, CurrentFader)
            .WithVerticalDataLayout<DataModel>(reverseLocalLayout: false)
                .AppendColumnDataSet(dataSet1GridView, 2)
                .AppendColumnDataSet(dataSet2GridView, 2)
                .AppendColumnDataSet(dataSet3GridView, 2)
                .AppendColumnDataSet(dataSet4GridView, 2)
                .AppendColumnDataSet(dataSet5GridView, 2)
            .WithArgument<Playground_GridItem>(
                _packedScene,
                _container,
                InfiniteLayoutGrids.CreateSimple(
                    _size,
                    _padding
                )
            )
                .ConfigureHorizontalScrollBar(_horizontalScrollBar, CurrentScrollBarTweener, CurrentFader)
                .ConfigureVerticalScrollBar(_verticalScrollBar, CurrentScrollBarTweener, CurrentFader)
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