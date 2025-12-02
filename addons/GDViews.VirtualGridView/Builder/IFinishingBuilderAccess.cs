using Godot;
using GodotViews.VirtualGrid.Layout;

namespace GodotViews.VirtualGrid.Builder;

/// <summary>
/// The builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.<br/>
/// Use the <see cref="WithArgument{TDataType}"/> or the <see cref="WithArgument{TButtonType,TExtraArgument}"/> method
/// to pass in the necessary arguments.
/// </summary>
/// <typeparam name="TDataType">The type for the data the building <see cref="IVirtualGridView{TDataType}"/> focuses on.</typeparam>
public interface IFinishingBuilderAccess<TDataType>
{
    /// <summary>
    /// Pass in the necessary arguments to the building <see cref="IVirtualGridView{TDataType}"/>
    /// </summary>
    /// <param name="itemPrefab">The <see cref="PackedScene"/> used for the virtualized grid element
    /// that have a script inherits <see cref="VirtualGridViewItemArg{TDataType,TExtraArgument}"/> attached.</param>
    /// <param name="itemContainer">The <see cref="Control"/> used for the container of all virtualized grid elements.</param>
    /// <param name="layoutGrid">The <see cref="InfiniteLayoutGrids"/> used to handle the layout positioning of all virtualized grid elements.</param>
    /// <param name="extraArgument">Sets the extra argument that will passed to the script attached to the virtualized grid elements.</param>
    /// <typeparam name="TButtonType">The type of the script attached to the <paramref name="itemPrefab"/>.</typeparam>
    /// <typeparam name="TExtraArgument">The extra argument passed to the script attached to the virtualized grid elements.</typeparam>
    /// <returns>A builder that concludes the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> WithArgument<TButtonType, TExtraArgument>(
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid,
        TExtraArgument extraArgument
    ) where TButtonType : VirtualGridViewItemArg<TDataType, TExtraArgument>;

    /// <summary>
    /// Pass in the necessary arguments to the building <see cref="IVirtualGridView{TDataType}"/>
    /// </summary>
    /// <param name="itemPrefab">The <see cref="PackedScene"/> used for the virtualized grid element
    /// that have a script inherits <see cref="VirtualGridViewItemArg{TDataType,TExtraArgument}"/> attached.</param>
    /// <param name="itemContainer">The <see cref="Control"/> used for the container of all virtualized grid elements.</param>
    /// <param name="layoutGrid">The <see cref="InfiniteLayoutGrids"/> used to handle the layout positioning of all virtualized grid elements.</param>
    /// <typeparam name="TButtonType">The type of the script attached to the <paramref name="itemPrefab"/>.</typeparam>
    /// <returns>A builder that concludes the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, NoExtraArgument> WithArgument<TButtonType>(
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid
    ) where TButtonType : VirtualGridViewItemArg<TDataType, NoExtraArgument> =>
        WithArgument<TButtonType, NoExtraArgument>(
            itemPrefab,
            itemContainer,
            layoutGrid,
            NoExtraArgument.Default
        );
}