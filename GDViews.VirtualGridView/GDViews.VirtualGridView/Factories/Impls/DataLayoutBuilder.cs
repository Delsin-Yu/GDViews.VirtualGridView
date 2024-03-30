using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Godot;

namespace GodotViews.VirtualGrid;

internal interface IDataInspector<T>
{
    void GetDataSetMetrics(out int rows, out int columns);
    ReadOnlySpan<NullableData<T>> InspectViewColumn(int rowIndex, int columnOffset, int rowOffset);
}

internal class DataLayoutBuilder<TDataType>(DataLayoutSelectionBuilder dataLayoutSelectionBuilder, DataLayoutDirection dataLayoutDirection, IEqualityComparer<TDataType>? equalityComparer, bool reverseLocalLayout) : IHorizontalDataLayoutBuilder<TDataType>, IVerticalDataLayoutBuilder<TDataType>
{
    public DataLayoutDirection DataLayoutDirection { get; } = dataLayoutDirection;
    public DataLayoutSelectionBuilder DataLayoutSelectionBuilder { get; } = dataLayoutSelectionBuilder;
    public IEqualityComparer<TDataType> EqualityComparer { get; } = equalityComparer ?? EqualityComparer<TDataType>.Default;

    private readonly List<DataSetDefinition<TDataType>> _dataSetDefinitions = [];

    public IHorizontalDataLayoutBuilder<TDataType> AddRowDataSource(DataSetDefinition<TDataType> dataSetDefinition)
    {
        _dataSetDefinitions.Add(dataSetDefinition);
        return this;
    }

    public IVerticalDataLayoutBuilder<TDataType> AddColumnDataSource(DataSetDefinition<TDataType> dataSetDefinition)
    {
        _dataSetDefinitions.Add(dataSetDefinition);
        return this;
    }

    private record struct AnnotatedDataSet<T>(IDynamicGridViewer<T> DataSet, int LocalIndex);

    
    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> WithArgument<TButtonType, TExtraArgument>(PackedScene itemPrefab, Control itemContainer, IInfiniteLayoutGrid layoutGrid) where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
    {
        HashSet<int> existingIndexes = [];

        var maxIndex = -1;
        var dataSetDefinitions = CollectionsMarshal.AsSpan(_dataSetDefinitions);
        foreach (ref readonly var dataSetDefinition in dataSetDefinitions)
        {
            if (dataSetDefinition.DataSet.FixedMetric != dataSetDefinition.DataSpan.Count)
                throw new ArgumentException("A data set's fixed metric differs from the declared data span count!");

            if (dataSetDefinition.DataSpan.Any(x => x < 0))
                throw new ArgumentException("A data set's declared data span contains index(es) value less than 0!");

            if (dataSetDefinition.DataSpan.Distinct().Count() != dataSetDefinition.DataSpan.Count)
                throw new ArgumentException("A data set's declared data span contains duplicate index(es)!");

            for (var spanIndex = 0; spanIndex < dataSetDefinition.DataSpan.Count; spanIndex++)
            {
                var dataIndex = dataSetDefinition.DataSpan[spanIndex];
                if (!existingIndexes.Add(dataIndex))
                    throw new ArgumentException($"A data set has already occupied the specified index {dataIndex}");
                maxIndex = Math.Max(dataIndex, maxIndex);
            }
        }

        var diff = existingIndexes.Count - 1 - maxIndex;

        if (diff > 0)
        {
            int[] missingIndexes = [diff];
            var localCounter = 0;
            for (var i = 0; i <= maxIndex; i++)
            {
                if (existingIndexes.Contains(i)) continue;
                missingIndexes[localCounter++] = i;
            }

            throw new ArgumentException($"One or more index(es) has not yet occupied by any data set(s): [{string.Join(", ", missingIndexes)}]");
        }

        var dataMap = new AnnotatedDataSet<TDataType>[existingIndexes.Count];

        Dictionary<IDynamicGridViewer<TDataType>, int> dataSetCounter = new();

        foreach (ref readonly var dataSetDefinition in dataSetDefinitions)
        {
            for (var spanIndex = 0; spanIndex < dataSetDefinition.DataSpan.Count; spanIndex++)
            {
                var dataIndex = dataSetDefinition.DataSpan[spanIndex];

                ref var currentCount = ref CollectionsMarshal.GetValueRefOrAddDefault(dataSetCounter, dataSetDefinition.DataSet, out var exists);
                if (!exists) currentCount = 0;
                dataMap[dataIndex] = new(dataSetDefinition.DataSet, currentCount++);
            }
        }

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

        IDataInspector<TDataType> dataInspector = DataLayoutDirection == DataLayoutDirection.Horizontal ?
            new HorizontalDataInspector<TDataType>(dataMap, viewColumns, viewRows) :
            new VerticalDataInspector<TDataType>(dataMap, viewColumns, viewRows);

        return new FinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument>(this, dataInspector, itemPrefab, itemContainer, layoutGrid);
    }

    private class HorizontalDataInspector<T>(AnnotatedDataSet<T>[] dataMap, int viewColumns, int viewRows) : IDataInspector<T>
    {
        private readonly NullableData<T>[] _view = new NullableData<T>[viewColumns];

        public void GetDataSetMetrics(out int rows, out int columns)
        {
            rows = dataMap.Sum(x => x.DataSet.FixedMetric);
            columns = dataMap.Max(x => x.DataSet.GetDynamicMetric());
        }
        
        /*
         * [0] DataSet1 <====> [0]
         * [1] DataSet1 <====> [1]
         * [2] DataSet2 <====> [0]
         * [3] DataSet1 <====> [2]
         * [4] DataSet2 <====> [1]
         * [5] DataSet1 <====> [3]
         */
        
        public ReadOnlySpan<NullableData<T>> InspectViewColumn(int rowIndex, int columnOffset, int rowOffset)
        {
            var viewSpan = _view.AsSpan();
            NullableData<T>.Clear(ref viewSpan);

            var actualRow = rowOffset + rowIndex;

            if (actualRow < 0 || dataMap.Length <= actualRow)
                return viewSpan;

            var dstColumnIndex = columnOffset + viewColumns;

            var (dataSet, localIndex) = dataMap[actualRow];

            for (var i = columnOffset; i < dstColumnIndex; i++)
            {
                ref var current = ref viewSpan[i - columnOffset];
                
                if (i >= 0 && dataSet.TryGetGridElement(localIndex, i, out var element))
                    current = new(true, element);
                else
                    current = NullableData<T>.Null;
            }

            return viewSpan;
        }
    }

    private class VerticalDataInspector<T>(AnnotatedDataSet<T>[] dataMap, int viewColumns, int viewRows) : IDataInspector<T>
    {
        private readonly NullableData<T>[] _view = new NullableData<T>[viewColumns];

        public void GetDataSetMetrics(out int rows, out int columns)
        {
            rows = dataMap.Max(x => x.DataSet.GetDynamicMetric());
            columns = dataMap.Sum(x => x.DataSet.FixedMetric);
        }
        
        /*
         * [0] [1] [2] [3] [4] [5]
         * DS1 DS1 DS2 DS1 DS2 DS1
         *  ^   ^   ^   ^   ^   ^
         *  |   |   |   |   |   |
         *  v   v   v   v   v   v
         * [0] [1] [0] [2] [1] [3]
         */

        public ReadOnlySpan<NullableData<T>> InspectViewColumn(int rowIndex, int columnOffset, int rowOffset)
        {
            var viewSpan = _view.AsSpan();
            NullableData<T>.Clear(ref viewSpan);

            if (dataMap.Length <= columnOffset)
                return viewSpan;

            var actualRow = rowOffset + rowIndex;

            var dstColumnIndex = columnOffset + viewColumns;

            for (var i = columnOffset; i < dstColumnIndex; i++)
            {
                ref var current = ref viewSpan[i - columnOffset];

                if (i < 0 || dataMap.Length <= i)
                {
                    current = NullableData<T>.Null;
                    continue;
                }

                var (dataSet, localIndex) = dataMap[i];

                if (actualRow >= 0 && dataSet.TryGetGridElement(localIndex, actualRow, out var element))
                    current = new(true, element);
                else
                    current = NullableData<T>.Null;
            }

            return viewSpan;
        }
    }
}