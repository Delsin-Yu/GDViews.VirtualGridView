using System;

namespace GodotViews.VirtualGrid.Builder;

internal interface IDataInspector<T>
{
    void GetDataSetCurrentMetrics(out int rows, out int columns);
    ReadOnlySpan<NullableData<T>> InspectViewColumn(int rowIndex, int columnOffset, int rowOffset);
    NullableData<T> InspectViewCell(int rowIndex, int columnOffset, int rowOffset, int columnIndex);
}