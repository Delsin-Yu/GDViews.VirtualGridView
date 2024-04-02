using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace GodotViews.VirtualGrid.Examples;

[GlobalClass]
public partial class DataSetController : Node
{
    [Export] private Label _num;
    [Export] private Button _add1;
    [Export] private Button _add10;
    [Export] private Button _remove1;
    [Export] private Button _remove10;
    [Export] private Button _scramble;
    [Export] private Button _clear;

    private List<Main.DataModel> _backingSet;
    private IDataSetHandler _handler;
    private int _dataSetIndex;

    public override void _Ready()
    {
        _add1.Pressed += () =>
        {
            var newElement = _handler.CreateElement(_dataSetIndex, _backingSet);
            _backingSet.Add(newElement);
            _handler.NotifyUpdate();
        };
        _add10.Pressed += () =>
        {
            for (var i = 0; i < 10; i++)
            {
                var newElement = _handler.CreateElement(_dataSetIndex, _backingSet);
                _backingSet.Add(newElement);
            }
            _handler.NotifyUpdate();
        };
        _remove1.Pressed += () =>
        {
            if(_backingSet.Count == 0) return;
            _backingSet.RemoveAt(_backingSet.Count - 1);
            _handler.NotifyUpdate();
        };
        _remove10.Pressed += () =>
        {
            if(_backingSet.Count == 0) return;
            for (var i = 0; i < 10; i++)
            {
                if(_backingSet.Count == 0) break;
                _backingSet.RemoveAt(Random.Shared.Next(0, _backingSet.Count));
            }
            _handler.NotifyUpdate();
        };
        _scramble.Pressed += () =>
        {
            var rawData = _backingSet.ToArray();
            _backingSet.Clear();
            _backingSet.AddRange(rawData.OrderBy(x => Guid.NewGuid()));
            _handler.NotifyUpdate();
        };
        _clear.Pressed += () =>
        {
            _backingSet.Clear();
            _handler.NotifyUpdate();
        };
    }

    public void Initialize(List<Main.DataModel> set, IDataSetHandler handler, int setId)
    {
        _num.Text = $"数据集{setId}";
        _dataSetIndex = setId;
        _backingSet = set;
        _handler = handler;
    }
}