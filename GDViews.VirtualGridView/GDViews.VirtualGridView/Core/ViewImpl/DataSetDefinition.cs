using System.Collections.Generic;

namespace GodotViews.VirtualGrid;

public record struct DataSetDefinition<TDataType>(IDynamicGridViewer<TDataType> DataSet, IReadOnlyList<int> DataSpan);