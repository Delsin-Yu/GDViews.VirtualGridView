using System.Collections.Generic;

namespace GodotViews.VirtualGrid.Examples;

public interface IDataSetHandler
{
    public Main.DataModel CreateElement(int dataSetIndex, IReadOnlyList<Main.DataModel> dataSet);
    public void NotifyUpdate();
}