using GodotViews.VirtualGrid.Viewer;

namespace GodotViews.VirtualGrid.Builder;

/// <summary>
/// The builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.<br/>
/// Use the <see cref="AppendDataSet"/> method to build up the horizontal datasets,
/// and call the <see cref="IFinishingBuilderAccess{TDataType}.WithArgument{TButtonType, TExtraArgument}"/> method when finished building the dataset.
/// </summary>
/// <typeparam name="TDataType">The type for the data the building <see cref="IVirtualGridView{TDataType}"/> focuses on.</typeparam>
public interface IHorizontalDataLayoutBuilder<TDataType> : IFinishingBuilderAccess<TDataType>
{
    /// <summary>
    /// Append the specified <paramref name="dataSet"/> to the building <see cref="IVirtualGridView{TDataType}"/>.
    /// Call the <see cref="IFinishingBuilderAccess{TDataType}.WithArgument{TButtonType, TExtraArgument}"/> method when finished building the dataset.
    /// </summary>
    /// <param name="dataSet">The <see cref="IDynamicGridViewer{T}"/> that gets appended to the datasets.</param>
    /// <param name="repeatCount">The ys this dataset takes, increase this value is equatable for calling this API multiple times.</param>
    /// <returns>The same <see cref="IHorizontalDataLayoutBuilder{TDataType}"/> for continuing the building of the datasets.</returns>
    /// <remarks><code>
    ///   [Y 0] [DataSet0: 00, 02, 04, 06, 08]
    ///   [Y 1] [DataSet1: 00, 02, 04, 06, 08] &lt;---- +New
    /// </code></remarks>
    IHorizontalDataLayoutBuilder<TDataType> AppendDataSet(IDynamicGridViewer<TDataType> dataSet, int repeatCount = 1);
}