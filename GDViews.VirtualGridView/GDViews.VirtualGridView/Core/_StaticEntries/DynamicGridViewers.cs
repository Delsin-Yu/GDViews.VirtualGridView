using System.Collections.Generic;

namespace GodotViews.VirtualGrid;

public static partial class DynamicGridViewers
{
    public static IDynamicGridViewer<T> CreateList<T>(IReadOnlyList<T> list) => 
        new CollectionGridViewerImpl<T>(list);
}