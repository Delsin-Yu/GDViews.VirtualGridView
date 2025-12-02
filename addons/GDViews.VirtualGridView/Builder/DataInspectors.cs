using System;
using System.Linq;

namespace GodotViews.VirtualGrid.Builder;

partial class DataLayoutBuilder<TDataType>
{
    private class HorizontalDataInspector<T>(AnnotatedDataSet<T>[] dataMap, int viewXCount, int viewYCount) : IDataInspector<T>
    {
        private readonly bool[] _yCalculationBuffer = new bool[dataMap.Length];
        private readonly NullableData<T>[] _view = new NullableData<T>[viewXCount];

        public void GetDataSetCurrentMetrics(out int xCount, out int yCount)
        {
            xCount = dataMap.Max(x => x.DataSet.GetDynamicMetric());

            var dataMapSpan = dataMap.AsSpan();
            var bufferSpan = _yCalculationBuffer.AsSpan();

            for (var index = 0; index < dataMapSpan.Length; index++)
            {
                var mapY = dataMapSpan[index];
                bufferSpan[index] = mapY.DataSet.TryGetGridElement(mapY.LocalIndex, 0, out _);
            }

            yCount = bufferSpan.LastIndexOf(true);
            if (yCount != -1) yCount++;
            else yCount = 0;
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

        public ReadOnlySpan<NullableData<T>> InspectViewX(int yIndex, int xOffset, int yOffset)
        {
            var viewSpan = _view.AsSpan();
            NullableData<T>.Clear(ref viewSpan);

            var actualY = yOffset + yIndex;

            if (actualY < 0 || dataMap.Length <= actualY)
                return viewSpan;

            var dstXIndex = xOffset + viewXCount;

            var (dataSet, localIndex) = dataMap[actualY];

            for (var i = xOffset; i < dstXIndex; i++)
            {
                ref var current = ref viewSpan[i - xOffset];

                if (i >= 0 && dataSet.TryGetGridElement(localIndex, i, out var element))
                    current = NullableData.Create(element);
                else
                    current = NullableData.Null<T>();
            }

            return viewSpan;
        }

        public NullableData<T> InspectViewCell(int yIndex, int xOffset, int yOffset, int xIndex)
        {
            var viewSpan = _view.AsSpan();
            NullableData<T>.Clear(ref viewSpan);

            var actualY = yOffset + yIndex * viewYCount;

            if (actualY < 0 || dataMap.Length <= actualY)
                return NullableData.Null<T>();

            var (dataSet, localIndex) = dataMap[actualY];

            var i = xOffset * viewXCount + xIndex;

            if (i >= 0 && dataSet.TryGetGridElement(localIndex, i, out var element))
                return NullableData.Create(element);

            return NullableData.Null<T>();
        }
    }

    private class VerticalDataInspector<T>(AnnotatedDataSet<T>[] dataMap, int viewXCount, int viewYCount) : IDataInspector<T>
    {
        private readonly bool[] _xCalculationBuffer = new bool[dataMap.Length];
        private readonly NullableData<T>[] _view = new NullableData<T>[viewXCount];

        public void GetDataSetCurrentMetrics(out int xCount, out int yCount)
        {
            yCount = dataMap.Max(x => x.DataSet.GetDynamicMetric());
            var dataMapSpan = dataMap.AsSpan();
            var bufferSpan = _xCalculationBuffer.AsSpan();

            for (var index = 0; index < dataMapSpan.Length; index++)
            {
                var mapX = dataMapSpan[index];
                bufferSpan[index] = mapX.DataSet.TryGetGridElement(mapX.LocalIndex, 0, out _);
            }

            xCount = bufferSpan.LastIndexOf(true);
            if (xCount != -1) xCount++;
            else xCount = 0;
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

        public ReadOnlySpan<NullableData<T>> InspectViewX(int yIndex, int xOffset, int yOffset)
        {
            var viewSpan = _view.AsSpan();
            NullableData<T>.Clear(ref viewSpan);

            if (dataMap.Length <= xOffset)
                return viewSpan;

            var actualY = yOffset + yIndex;

            var dstXIndex = xOffset + viewXCount;

            for (var i = xOffset; i < dstXIndex; i++)
            {
                ref var current = ref viewSpan[i - xOffset];

                if (i < 0 || dataMap.Length <= i)
                {
                    current = NullableData.Null<T>();
                    continue;
                }

                var (dataSet, localIndex) = dataMap[i];

                if (actualY >= 0 && dataSet.TryGetGridElement(localIndex, actualY, out var element))
                    current = NullableData.Create(element);
                else
                    current = NullableData.Null<T>();
            }

            return viewSpan;
        }

        public NullableData<T> InspectViewCell(int yIndex, int xOffset, int yOffset, int xIndex)
        {
            var viewSpan = _view.AsSpan();
            NullableData<T>.Clear(ref viewSpan);

            if (dataMap.Length <= xOffset)
                return NullableData.Null<T>();

            var actualY = yOffset + yIndex * viewYCount;

            var i = xOffset * viewXCount + xIndex;

            if (i >= dataMap.Length || i < 0 || dataMap.Length <= i) return NullableData.Null<T>();

            var (dataSet, localIndex) = dataMap[i];

            if (actualY >= 0 && dataSet.TryGetGridElement(localIndex, actualY, out var element))
                return NullableData.Create(element);

            return NullableData.Null<T>();
        }
    }
}