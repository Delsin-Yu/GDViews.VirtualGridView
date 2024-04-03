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
        DataBindings.Bind(_add1, Add1);
        DataBindings.Bind(_add10, Add10);
        DataBindings.Bind(_remove1, Remove1);
        DataBindings.Bind(_remove10, Remove10);
        DataBindings.Bind(_scramble, Scramble);
        DataBindings.Bind(_clear, Clear);
    }

    private void Clear()
    {
        _backingSet.Clear();
        _handler.NotifyUpdate();
    }

    private void Scramble()
    {
        var rawData = _backingSet.ToArray();
        _backingSet.Clear();
        _backingSet.AddRange(rawData.OrderBy(x => Guid.NewGuid()));
        _handler.NotifyUpdate();
    }

    private void Remove10()
    {
        if (_backingSet.Count == 0) return;
        for (var i = 0; i < 10; i++)
        {
            if (_backingSet.Count == 0) break;
            _backingSet.RemoveAt(Random.Shared.Next(0, _backingSet.Count));
        }

        _handler.NotifyUpdate();
    }

    private void Remove1()
    {
        if (_backingSet.Count == 0) return;
        _backingSet.RemoveAt(_backingSet.Count - 1);
        _handler.NotifyUpdate();
    }

    private void Add10()
    {
        for (var i = 0; i < 10; i++)
        {
            var newElement = _handler.CreateElement(_dataSetIndex, _backingSet);
            _backingSet.Add(newElement);
        }

        _handler.NotifyUpdate();
    }

    private void Add1()
    {
        var newElement = _handler.CreateElement(_dataSetIndex, _backingSet);
        _backingSet.Add(newElement);
        _handler.NotifyUpdate();
    }

    public void Initialize(List<Main.DataModel> set, IDataSetHandler handler, int setId)
    {
        _num.Text = $"数据集{setId}";
        _dataSetIndex = setId;
        _backingSet = set;
        _handler = handler;
    }
}