using System;
using System.Linq;

namespace GodotViews.VirtualGrid.Builder;

internal partial class DataLayoutBuilder<TDataType>
{
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