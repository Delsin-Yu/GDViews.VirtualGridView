using GodotViews.VirtualGrid.Builder;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Use the <see cref="Create"/> method to initiate a build process of the <see cref="IVirtualGridView{TDataType}"/> instance.
/// </summary>
public static class VirtualGridView
{
    /// <summary>
    /// Initiate a build process of the <see cref="IVirtualGridView{TDataType}"/> instance
    /// by setting up the viewport metrics, or the amount of elements displayed concurrently by the control.
    /// </summary>
    /// <param name="viewportColumns">The number of columns for the concurrently displayed virtualized grid items.</param>
    /// <param name="viewportRows">The number of rows for the concurrently displayed virtualized grid items.</param>
    /// <returns>A builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    public static IViewHandlerBuilder Create(int viewportColumns, int viewportRows) => new ViewHandlerBuilder(viewportColumns, viewportRows);
}