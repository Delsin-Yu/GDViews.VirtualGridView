using System;

namespace GodotViews.VirtualGrid.Builder;

interface IDataInspector<T>
{
    void GetDataSetCurrentMetrics(out int xCount, out int yCount);
    ReadOnlySpan<NullableData<T>> InspectViewX(int yIndex, int xOffset, int yOffset);
    NullableData<T> InspectViewCell(int yIndex, int xOffset, int yOffset, int xIndex);
}