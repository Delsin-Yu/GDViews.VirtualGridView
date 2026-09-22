namespace GodotViews.VirtualGrid.FocusFinding;

public static partial class FocusFinders
{
    private class DataLastDataImpl : IEqualityDataFocusFinder
    {
        public bool TryResolveFocus<TDataType>(
            ref readonly TDataType matchingArgument,
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            out int dataSetXIndex,
            out int dataSetYIndex)
        {
            for (var y = currentView.DataSetYCount - 1; y >= 0; y--)
            for (var x = currentView.DataSetXCount - 1; x >= 0; x--)
            {
                if (!currentView.TryGetData(x, y, out _)) continue;
                dataSetXIndex = x;
                dataSetYIndex = y;
                return true;
            }

            dataSetXIndex = -1;
            dataSetYIndex = -1;
            return false;
        }
    }
}