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

    private List<DataModel> _backingSet;
    private IDataSetHandler _handler;
    private int _dataSetIndex;

    public override void _Ready()
    {
        DataBindings.Bind(_add1, instance => instance.Add1(), this);
        DataBindings.Bind(_add10, instance => instance.Add10(), this);
        DataBindings.Bind(_remove1, instance => instance.Remove1(), this);
        DataBindings.Bind(_remove10, instance => instance.Remove10(), this);
        DataBindings.Bind(_scramble, instance => instance.Scramble(), this);
        DataBindings.Bind(_clear, instance => instance.Clear(), this);
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

    public void Initialize(List<DataModel> set, IDataSetHandler handler, int setId)
    {
        _num.Text = $"数据集{setId}";
        _dataSetIndex = setId;
        _backingSet = set;
        _handler = handler;
    }
}