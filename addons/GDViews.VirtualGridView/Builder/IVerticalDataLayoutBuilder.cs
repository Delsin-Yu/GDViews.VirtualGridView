using GodotViews.VirtualGrid.Viewer;

namespace GodotViews.VirtualGrid.Builder;

/// <summary>
/// The builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.<br/>
/// Use the <see cref="AppendColumnDataSet"/> method to build up the vertical datasets,
/// and call the <see cref="IFinishingBuilderAccess{TDataType}.WithArgument{TButtonType, TExtraArgument}"/> method when finished building the dataset.
/// </summary>
/// <typeparam name="TDataType">The type for the data the building <see cref="IVirtualGridView{TDataType}"/> focuses on.</typeparam>
public interface IVerticalDataLayoutBuilder<TDataType> : IFinishingBuilderAccess<TDataType>
{
    /// <summary>
    /// Append the specified <paramref name="dataSet"/> to the building <see cref="IVirtualGridView{TDataType}"/>.
    /// Call the <see cref="IFinishingBuilderAccess{TDataType}.WithArgument{TButtonType, TExtraArgument}"/> method when finished building the dataset.
    /// </summary>
    /// <param name="dataSet">The <see cref="IDynamicGridViewer{T}"/> that gets appended to the datasets.</param>
    /// <param name="repeatCount">The columns this dataset takes, increase this value is equatable for calling this API multiple times.</param>
    /// <returns>The same <see cref="IVerticalDataLayoutBuilder{TDataType}"/> for continuing the building of the datasets.</returns>
    /// <remarks><code>
    ///  [Column 0] [Column 1]
    ///  [DataSet0] [DataSet1]
    /// |    00    |    00    |
    /// |    01    |    01    |
    /// |    02    |    02    |
    /// |    03    |    03    |
    /// |    04    |    04    |
    ///             ^^ +New ^^
    /// </code></remarks>
    IVerticalDataLayoutBuilder<TDataType> AppendColumnDataSet(IDynamicGridViewer<TDataType> dataSet, int repeatCount = 1);
}