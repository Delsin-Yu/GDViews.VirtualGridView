using System.Collections.Generic;

namespace GodotViews.VirtualGrid.Examples;

public interface IDataSetHandler
{
    public DataModel CreateElement(int dataSetIndex, IReadOnlyList<DataModel> dataSet);
    public void NotifyUpdate();
}