using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Godot;
using GodotViews.VirtualGrid.Layout;
using GodotViews.VirtualGrid.Viewer;

namespace GodotViews.VirtualGrid.Builder;

internal partial class DataLayoutBuilder<TDataType>(
    DataLayoutSelectionBuilder dataLayoutSelectionBuilder,
    IEqualityComparer<TDataType>? equalityComparer,
    bool reverseLocalLayout,
    bool isHorizontalDataLayout) : IHorizontalDataLayoutBuilder<TDataType>, IVerticalDataLayoutBuilder<TDataType>
{
    private record struct AnnotatedDataSet<T>(IDynamicGridViewer<T> DataSet, int LocalIndex);

    private readonly List<IDynamicGridViewer<TDataType>> _dataSetDefinitions = [];
    public DataLayoutSelectionBuilder DataLayoutSelectionBuilder { get; } = dataLayoutSelectionBuilder;
    public IEqualityComparer<TDataType> EqualityComparer { get; } = equalityComparer ?? EqualityComparer<TDataType>.Default;

    public IHorizontalDataLayoutBuilder<TDataType> AppendRowDataSet(IDynamicGridViewer<TDataType> dataSetDefinition, int repeatCount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(repeatCount);
        for(var i = 0; i < repeatCount; i++)
            _dataSetDefinitions.Add(dataSetDefinition);
        return this;
    }

    public IVerticalDataLayoutBuilder<TDataType> AppendColumnDataSet(IDynamicGridViewer<TDataType> dataSetDefinition, int repeatCount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(repeatCount);
        for(var i = 0; i < repeatCount; i++)
            _dataSetDefinitions.Add(dataSetDefinition);
        return this;
    }
    
    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> WithArgument<TButtonType, TExtraArgument>(
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid,
        TExtraArgument extraArgument
    ) where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
    {
        var dataSetDefinitions = CollectionsMarshal.AsSpan(_dataSetDefinitions);

        var dataMap = new AnnotatedDataSet<TDataType>[dataSetDefinitions.Length];

        Dictionary<IDynamicGridViewer<TDataType>, int> dataSetCounter = new();

        for (var index = 0; index < dataSetDefinitions.Length; index++)
        {
            var dataSetDefinition = dataSetDefinitions[index];
            ref var currentCount = ref CollectionsMarshal.GetValueRefOrAddDefault(dataSetCounter, dataSetDefinition, out var exists);
            if (!exists) currentCount = 0;
            dataMap[index] = new(dataSetDefinition, currentCount++);
        }

        foreach (var (dynamicGridViewer, count) in dataSetCounter) 
            dynamicGridViewer.FixedMetric = count;
        
        if (reverseLocalLayout)
        {
            foreach (ref var annotatedDataSet in dataMap.AsSpan())
            {
                var count = dataSetCounter[annotatedDataSet.DataSet] - 1;
                annotatedDataSet.LocalIndex = count - annotatedDataSet.LocalIndex;
            }
        }

        var viewColumns = DataLayoutSelectionBuilder.ViewHandlerBuilder.ViewportColumns;
        var viewRows = DataLayoutSelectionBuilder.ViewHandlerBuilder.ViewportRows;

        IDataInspector<TDataType> dataInspector = isHorizontalDataLayout ?
            new HorizontalDataInspector<TDataType>(dataMap, viewColumns, viewRows) :
            new VerticalDataInspector<TDataType>(dataMap, viewColumns, viewRows);

        return new FinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument>(
            this,
            dataInspector,
            itemPrefab,
            itemContainer,
            layoutGrid,
            extraArgument
        );
    }
}