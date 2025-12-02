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
    /// <param name="viewportXCount">The number of xs for the concurrently displayed virtualized grid items.</param>
    /// <param name="viewportYCount">The number of ys for the concurrently displayed virtualized grid items.</param>
    /// <returns>A builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    public static IViewHandlerBuilder Create(int viewportXCount, int viewportYCount) => new ViewHandlerBuilder(viewportXCount, viewportYCount);
}