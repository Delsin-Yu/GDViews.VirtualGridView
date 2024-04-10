using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// The builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.<br/>
/// Use the <see cref="WithHandlers"/> method to set up the visual transition behavior for the grid elements.
/// </summary>
public interface IViewHandlerBuilder
{
    /// <summary>
    /// Sets the visual transition behavior for the grid elements.
    /// </summary>
    /// <param name="elementPositioner">The Positioner assigned to the <see cref="IVirtualGridView{TDataType}"/>.</param>
    /// <param name="elementTweener">The Tweener assigned to the <see cref="IVirtualGridView{TDataType}"/>.</param>
    /// <param name="elementFader">The Fader assigned to the <see cref="IVirtualGridView{TDataType}"/>.</param>
    /// <returns>A builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    IDataLayoutBuilder WithHandlers(IElementPositioner elementPositioner, IElementTweener elementTweener, IElementFader elementFader);
}

/// <summary>
/// The builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.<br/>
/// Use the <see cref="WithHorizontalDataLayout{TDataType}"/> or the <see cref="WithVerticalDataLayout{TDataType}"/> method to choose between the layout of the data sets.
/// </summary>
public interface IDataLayoutBuilder
{
    /// <summary>
    /// Instruct the view controller to layout the datasets horizontally. 
    /// </summary>
    /// <param name="equalityComparer">The <see cref="IEqualityComparer{T}"/> used to
    /// determine if the data associated to certain grid element has changed,
    /// setting to null will fallback to the <see cref="EqualityComparer{T}.Default"/></param>
    /// <param name="reverseLocalLayout">When set to true, the view controller will reverse the layout of the provided datasets.</param>
    /// <typeparam name="TDataType">The type for the data the building <see cref="IVirtualGridView{TDataType}"/> focuses on.</typeparam>
    /// <returns>A builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    /// <remarks><code>
    /// Preview of the horizontal data layout, each data set is allowed to occupy more than one row:
    ///   [Row 0] [DataSet0: 00, 02, 04, 06, 08]
    ///   [Row 1] [DataSet0: 01, 03, 05, 07, 09]
    ///   [Row 2] [DataSet1: 00, 02, 04, 06, 08]
    ///   [Row 3] [DataSet1: 01, 03, 05, 07, 09]
    ///   [Row 4] [DataSet2: 00, 01, 02, 03, 04, 05]
    ///   [Row 5] [DataSet3: 00, 01, 02, 03, 04, 05]
    ///  
    /// When the reverseLocalLayout is set to true:
    ///   [Row 0] [DataSet0: 01, 03, 05, 07, 09]
    ///   [Row 1] [DataSet0: 00, 02, 04, 06, 08]
    ///   [Row 2] [DataSet1: 01, 03, 05, 07, 09]
    ///   [Row 3] [DataSet1: 00, 02, 04, 06, 08]
    ///   [Row 4] [DataSet2: 00, 01, 02, 03, 04, 05]
    ///   [Row 5] [DataSet3: 00, 01, 02, 03, 04, 05]
    /// </code></remarks>
    IHorizontalDataLayoutBuilder<TDataType> WithHorizontalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false);
    
    
    /// <summary>
    /// Instruct the view controller to layout the datasets vertically. 
    /// </summary>
    /// <param name="equalityComparer">The <see cref="IEqualityComparer{T}"/> used to
    /// determine if the data associated to certain grid element has changed,
    /// setting to null will fallback to the <see cref="EqualityComparer{T}.Default"/></param>
    /// <param name="reverseLocalLayout">When set to true, the view controller will reverse the layout of the provided datasets.</param>
    /// <typeparam name="TDataType">The type for the data the building <see cref="IVirtualGridView{TDataType}"/> focuses on.</typeparam>
    /// <returns>A builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    /// <remarks><code>
    /// Preview of the vertical data layout, each data set is allowed to occupy more than one column:
    ///  [Column 0] [Column 1] [Column 2] [Column 3] [Column 4] [Column 5]
    ///  [DataSet0] [DataSet0] [DataSet1] [DataSet1] [DataSet2] [DataSet3]
    /// |    00    |    01    |    00    |    01    |    00    |    00    |
    /// |    02    |    03    |    02    |    03    |    01    |    01    |
    /// |    04    |    05    |    04    |    05    |    02    |    02    |
    /// |    06    |    07    |    06    |    07    |    03    |    03    |
    /// |    08    |    09    |    08    |    09    |    04    |    04    |
    ///  
    /// When the reverseLocalLayout is set to true:
    ///  [Column 0] [Column 1] [Column 2] [Column 3] [Column 4] [Column 5]
    ///  [DataSet0] [DataSet0] [DataSet1] [DataSet1] [DataSet2] [DataSet3]
    /// |    01    |    00    |    01    |    00    |    00    |    00    |
    /// |    03    |    02    |    03    |    02    |    01    |    01    |
    /// |    05    |    04    |    05    |    04    |    02    |    02    |
    /// |    07    |    06    |    07    |    06    |    03    |    03    |
    /// |    09    |    08    |    09    |    08    |    04    |    04    |
    /// </code></remarks>
    IVerticalDataLayoutBuilder<TDataType> WithVerticalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false);
}

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
    /// that have a script inherits <see cref="VirtualGridViewItem{TDataType,TExtraArgument}"/> attached.</param>
    /// <param name="itemContainer">The <see cref="Control"/> used for the container of all virtualized grid elements.</param>
    /// <param name="layoutGrid">The <see cref="InfiniteLayoutGrids"/> used to handle the layout positioning of all virtualized grid elements.</param>
    /// <typeparam name="TButtonType">The type of the script attached to the <paramref name="itemPrefab"/>.</typeparam>
    /// <typeparam name="TExtraArgument">The extra argument passed to the script attached to the virtualized grid elements.</typeparam>
    /// <returns>A builder that concludes the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> WithArgument<TButtonType, TExtraArgument>(
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid
    ) where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>;

    /// <summary>
    /// Pass in the necessary arguments to the building <see cref="IVirtualGridView{TDataType}"/>
    /// </summary>
    /// <param name="itemPrefab">The <see cref="PackedScene"/> used for the virtualized grid element
    /// that have a script inherits <see cref="VirtualGridViewItem{TDataType,TExtraArgument}"/> attached.</param>
    /// <param name="itemContainer">The <see cref="Control"/> used for the container of all virtualized grid elements.</param>
    /// <param name="layoutGrid">The <see cref="InfiniteLayoutGrids"/> used to handle the layout positioning of all virtualized grid elements.</param>
    /// <typeparam name="TButtonType">The type of the script attached to the <paramref name="itemPrefab"/>.</typeparam>
    /// <returns>A builder that concludes the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, NoExtraArgument> WithArgument<TButtonType>(
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid
    ) where TButtonType : VirtualGridViewItem<TDataType, NoExtraArgument> =>
        WithArgument<TButtonType, NoExtraArgument>(
            itemPrefab,
            itemContainer,
            layoutGrid
        );
}

/// <summary>
/// The builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.<br/>
/// Use the <see cref="AppendRowDataSet"/> method to build up the horizontal datasets,
/// and call the <see cref="IFinishingBuilderAccess{TDataType}.WithArgument{TButtonType, TExtraArgument}"/> method when finished building the dataset.
/// </summary>
/// <typeparam name="TDataType">The type for the data the building <see cref="IVirtualGridView{TDataType}"/> focuses on.</typeparam>
public interface IHorizontalDataLayoutBuilder<TDataType> : IFinishingBuilderAccess<TDataType>
{
    /// <summary>
    /// Append the specified <paramref name="dataSet"/> to the building <see cref="IVirtualGridView{TDataType}"/>.
    /// </summary>
    /// <param name="dataSet">The <see cref="IDynamicGridViewer{T}"/> that gets appended to the datasets.</param>
    /// <param name="repeatCount">The rows this dataset takes, increase this value is equatable for calling this API multiple times.</param>
    /// <returns>The same <see cref="IHorizontalDataLayoutBuilder{TDataType}"/> for continuing the building of the datasets.</returns>
    /// <remarks><code>
    ///   [Row 0] [DataSet0: 00, 02, 04, 06, 08]
    ///   [Row 1] [DataSet1: 00, 02, 04, 06, 08] &lt;---- +New
    /// </code></remarks>
    IHorizontalDataLayoutBuilder<TDataType> AppendRowDataSet(IDynamicGridViewer<TDataType> dataSet, int repeatCount = 1);
}

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

/// <summary>
/// The builder that concludes the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.<br/>
/// </summary>
/// <typeparam name="TDataType">The type for the data the building <see cref="IVirtualGridView{TDataType}"/> focuses on.</typeparam>
/// <typeparam name="TButtonType">The type of the script attached to the virtualized grid element.</typeparam>
/// <typeparam name="TExtraArgument">The extra argument passed to the script attached to the virtualized grid elements.</typeparam>
public interface IFinishingArgumentBuilder<TDataType, TButtonType, in TExtraArgument> where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    /// <summary>
    /// Finish the final building configuration, and instantiate the <see cref="IVirtualGridView{TDataType}"/> instance. 
    /// </summary>
    /// <returns>The instantiated <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    IVirtualGridView<TDataType> Build();

    /// <summary>
    /// Assign a <see cref="ScrollBar"/> to the building <see cref="IVirtualGridView{TDataType}"/>
    /// for it to become the horizontal progress indicator.
    /// </summary>
    /// <param name="horizontalScrollBar">The <see cref="ScrollBar"/> to associate to.</param>
    /// <param name="tweener">The <see cref="IScrollBarTweener"/> used to handle the value interpolation of the scroll bar.</param>
    /// <param name="fader">The <see cref="IElementTweener"/> used to hiding or showing the scroll bar.</param>
    /// <param name="autoHide">Instructs the <see cref="IVirtualGridView{TDataType}"/> to hide the scroll bar
    /// when the current viewport is horizontally sufficient for showing every element of the datasets.</param>
    /// <returns>The same <see cref="IFinishingArgumentBuilder{TDataType,TButtonType,TExtraArgument}"/>
    /// for continuing the configuration of this <see cref="IVirtualGridView{TDataType}"/>.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureHorizontalScrollBar(
        ScrollBar horizontalScrollBar,
        IScrollBarTweener? tweener = null,
        IElementFader? fader = null,
        bool autoHide = false
    );

    /// <summary>
    /// Assign a <see cref="ScrollBar"/> to the building <see cref="IVirtualGridView{TDataType}"/>
    /// for it to become the vertical progress indicator.
    /// </summary>
    /// <param name="verticalScrollBar">The <see cref="ScrollBar"/> to associate to.</param>
    /// <param name="tweener">The <see cref="IScrollBarTweener"/> used to handle the value interpolation of the scroll bar.</param>
    /// <param name="fader">The <see cref="IElementTweener"/> used to hiding or showing the scroll bar.</param>
    /// <param name="autoHide">Instructs the <see cref="IVirtualGridView{TDataType}"/> to hide the scroll bar
    /// when the current viewport is vertically sufficient for showing every element of the datasets.</param>
    /// <returns>The same <see cref="IFinishingArgumentBuilder{TDataType,TButtonType,TExtraArgument}"/>
    /// for continuing the configuration of this <see cref="IVirtualGridView{TDataType}"/>.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureVerticalScrollBar(
        ScrollBar verticalScrollBar,
        IScrollBarTweener? tweener = null,
        IElementFader? fader = null,
        bool autoHide = false
    );

    /// <summary>
    /// Sets the extra argument that will passed to the script attached to the virtualized grid elements.
    /// </summary>
    /// <param name="extraArgument">The value of the extra argument.</param>
    /// <returns>The same <see cref="IFinishingArgumentBuilder{TDataType,TButtonType,TExtraArgument}"/>
    /// for continuing the configuration of this <see cref="IVirtualGridView{TDataType}"/>.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureExtraArgument(TExtraArgument extraArgument);
}