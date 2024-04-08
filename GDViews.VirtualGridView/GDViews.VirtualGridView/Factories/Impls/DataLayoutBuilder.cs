using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Godot;

namespace GodotViews.VirtualGrid;

internal interface IDataInspector<T>
{
    void GetDataSetCurrentMetrics(out int rows, out int columns);
    ReadOnlySpan<NullableData<T>> InspectViewColumn(int rowIndex, int columnOffset, int rowOffset);
    NullableData<T> InspectViewCell(int rowIndex, int columnOffset, int rowOffset, int columnIndex);
}

internal class DataLayoutBuilder<TDataType>(DataLayoutSelectionBuilder dataLayoutSelectionBuilder, IEqualityComparer<TDataType>? equalityComparer, bool reverseLocalLayout, bool isHorizontalDataLayout) : IHorizontalDataLayoutBuilder<TDataType>, IVerticalDataLayoutBuilder<TDataType>
{
    private readonly List<DataSetDefinition<TDataType>> _dataSetDefinitions = [];
    public DataLayoutSelectionBuilder DataLayoutSelectionBuilder { get; } = dataLayoutSelectionBuilder;
    public IEqualityComparer<TDataType> EqualityComparer { get; } = equalityComparer ?? EqualityComparer<TDataType>.Default;

    public IHorizontalDataLayoutBuilder<TDataType> AddRowDataSource(DataSetDefinition<TDataType> dataSetDefinition)
    {
        _dataSetDefinitions.Add(dataSetDefinition);
        return this;
    }


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
            for (var spanIndex = 0; spanIndex < dataSetDefinition.DataSpan.Count; spanIndex++)
            {
                var dataIndex = dataSetDefinition.DataSpan[spanIndex];

                ref var currentCount = ref CollectionsMarshal.GetValueRefOrAddDefault(dataSetCounter, dataSetDefinition.DataSet, out var exists);
                if (!exists) currentCount = 0;
                dataMap[dataIndex] = new(dataSetDefinition.DataSet, currentCount++);
            }

        if (reverseLocalLayout)
            foreach (ref var annotatedDataSet in dataMap.AsSpan())
            {
                var count = dataSetCounter[annotatedDataSet.DataSet] - 1;
                annotatedDataSet.LocalIndex = count - annotatedDataSet.LocalIndex;
            }

        var viewColumns = DataLayoutSelectionBuilder.ViewHandlerBuilder.ViewportColumns;
        var viewRows = DataLayoutSelectionBuilder.ViewHandlerBuilder.ViewportRows;

        IDataInspector<TDataType> dataInspector = isHorizontalDataLayout ?
            new HorizontalDataInspector<TDataType>(dataMap, viewColumns, viewRows) :
            new VerticalDataInspector<TDataType>(dataMap, viewColumns, viewRows);

        return new FinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument>(this, dataInspector, itemPrefab, itemContainer, layoutGrid);
    }

    public IVerticalDataLayoutBuilder<TDataType> AddColumnDataSource(DataSetDefinition<TDataType> dataSetDefinition)
    {
        _dataSetDefinitions.Add(dataSetDefinition);
        return this;
    }

    private record struct AnnotatedDataSet<T>(IDynamicGridViewer<T> DataSet, int LocalIndex);

    private class HorizontalDataInspector<T>(AnnotatedDataSet<T>[] dataMap, int viewColumns, int viewRows) : IDataInspector<T>
    {
        private readonly bool[] _rowCalculationBuffer = new bool[dataMap.Length];
        private readonly NullableData<T>[] _view = new NullableData<T>[viewColumns];

        public void GetDataSetCurrentMetrics(out int rows, out int columns)
        {
            columns = dataMap.Max(x => x.DataSet.GetDynamicMetric());

            var dataMapSpan = dataMap.AsSpan();
            var bufferSpan = _rowCalculationBuffer.AsSpan();
            for (var index = 0; index < dataMapSpan.Length; index++)
            {
                var mapRow = dataMapSpan[index];
                bufferSpan[index] = mapRow.DataSet.TryGetGridElement(mapRow.LocalIndex, 0, out _);
            }

            rows = bufferSpan.LastIndexOf(true);
            if (rows != -1) rows++;
            else rows = 0;
            bufferSpan.Clear();
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
                    current = NullableData.Create(element);
                else
                    current = NullableData.Null<T>();
            }

            return viewSpan;
        }

        public NullableData<T> InspectViewCell(int rowIndex, int columnOffset, int rowOffset, int columnIndex)
        {
            var viewSpan = _view.AsSpan();
            NullableData<T>.Clear(ref viewSpan);

            var actualRow = rowOffset + rowIndex * viewRows;

            if (actualRow < 0 || dataMap.Length <= actualRow)
                return NullableData.Null<T>();

            var (dataSet, localIndex) = dataMap[actualRow];

            var i = columnOffset * viewColumns + columnIndex;

            if (i >= 0 && dataSet.TryGetGridElement(localIndex, i, out var element))
                return NullableData.Create(element);

            return NullableData.Null<T>();
        }
    }

    private class VerticalDataInspector<T>(AnnotatedDataSet<T>[] dataMap, int viewColumns, int viewRows) : IDataInspector<T>
    {
        private readonly bool[] _columnCalculationBuffer = new bool[dataMap.Length];
        private readonly NullableData<T>[] _view = new NullableData<T>[viewColumns];

        public void GetDataSetCurrentMetrics(out int rows, out int columns)
        {
            rows = dataMap.Max(x => x.DataSet.GetDynamicMetric());
            var dataMapSpan = dataMap.AsSpan();
            var bufferSpan = _columnCalculationBuffer.AsSpan();
            for (var index = 0; index < dataMapSpan.Length; index++)
            {
                var mapColumn = dataMapSpan[index];
                bufferSpan[index] = mapColumn.DataSet.TryGetGridElement(mapColumn.LocalIndex, 0, out _);
            }

            columns = bufferSpan.LastIndexOf(true);
            if (columns != -1) columns++;
            else columns = 0;
            bufferSpan.Clear();
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
                    current = NullableData.Null<T>();
                    continue;
                }

                var (dataSet, localIndex) = dataMap[i];

                if (actualRow >= 0 && dataSet.TryGetGridElement(localIndex, actualRow, out var element))
                    current = NullableData.Create(element);
                else
                    current = NullableData.Null<T>();
            }

            return viewSpan;
        }

        public NullableData<T> InspectViewCell(int rowIndex, int columnOffset, int rowOffset, int columnIndex)
        {
            var viewSpan = _view.AsSpan();
            NullableData<T>.Clear(ref viewSpan);

            if (dataMap.Length <= columnOffset)
                return NullableData.Null<T>();

            var actualRow = rowOffset + rowIndex * viewRows;

            var i = columnOffset * viewColumns + columnIndex;

            if (i >= dataMap.Length || i < 0 || dataMap.Length <= i) return NullableData.Null<T>();

            var (dataSet, localIndex) = dataMap[i];

            if (actualRow >= 0 && dataSet.TryGetGridElement(localIndex, actualRow, out var element))
                return NullableData.Create(element);

            return NullableData.Null<T>();
        }
    }
}