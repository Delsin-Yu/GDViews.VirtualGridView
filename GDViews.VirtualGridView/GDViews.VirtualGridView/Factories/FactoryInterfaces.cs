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
    /// <typeparam name="TDataType">The type for the data</typeparam>
    /// <returns>A builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    /// <remarks><code>
    /// Preview of the horizontal data layout, each data set is allowed to occupy more than one row:
    /// --------------------------------------->
    ///   [Row 0] [DataSet0: 0, 2, 4, 6, 8]
    ///   [Row 1] [DataSet0: 1, 3, 5, 7, 9]
    ///   [Row 2] [DataSet1: 0, 2, 4, 6, 8]
    ///   [Row 3] [DataSet1: 1, 3, 5, 7, 9]
    ///   [Row 4] [DataSet2: 0, 1, 2, 3, 4, 5]
    ///   [Row 5] [DataSet3: 0, 1, 2, 3, 4, 5]
    ///  
    /// When the reverseLocalLayout is set to true:
    /// --------------------------------------->
    ///   [Row 0] [DataSet0: 1, 3, 5, 7, 9]
    ///   [Row 1] [DataSet0: 0, 2, 4, 6, 8]
    ///   [Row 2] [DataSet1: 1, 3, 5, 7, 9]
    ///   [Row 3] [DataSet1: 0, 2, 4, 6, 8]
    ///   [Row 4] [DataSet2: 0, 1, 2, 3, 4, 5]
    ///   [Row 5] [DataSet3: 0, 1, 2, 3, 4, 5]
    /// </code></remarks>
    IHorizontalDataLayoutBuilder<TDataType> WithHorizontalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false);
    
    
    /// <summary>
    /// Instruct the view controller to layout the datasets vertically. 
    /// </summary>
    /// <param name="equalityComparer">The <see cref="IEqualityComparer{T}"/> used to
    /// determine if the data associated to certain grid element has changed,
    /// setting to null will fallback to the <see cref="EqualityComparer{T}.Default"/></param>
    /// <param name="reverseLocalLayout">When set to true, the view controller will reverse the layout of the provided datasets.</param>
    /// <typeparam name="TDataType">The type for the data</typeparam>
    /// <returns>A builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    /// <remarks><code>
    /// Preview of the vertical data layout, each data set is allowed to occupy more than one column:
    /// ---------------------------------------------------------------->
    /// [Column 0] [Column 1] [Column 2] [Column 3] [Column 4] [Column 5]
    /// [DataSet0] [DataSet0] [DataSet1] [DataSet1] [DataSet2] [DataSet3]
    ///  [          [          [          [          [          [
    ///      0,         1,         0,         1,         0,         0,
    ///      2,         3,         2,         3,         1,         1,
    ///      4,         5,         4,         5,         2,         2,
    ///      6,         7,         6,         7,         3,         3,
    ///      8,         9,         8,         9,         4,         4,
    ///         ]          ]          ]          ]          ]          ]
    ///  
    /// When the reverseLocalLayout is set to true:
    /// --------------------------------------->
    /// [Column 0] [Column 1] [Column 2] [Column 3] [Column 4] [Column 5]
    /// [DataSet0] [DataSet0] [DataSet1] [DataSet1] [DataSet2] [DataSet3]
    ///  [          [          [          [          [          [
    ///      1,         0,         1,         0,         0,         0,
    ///      3,         2,         3,         2,         1,         1,
    ///      5,         4,         5,         4,         2,         2,
    ///      7,         6,         7,         6,         3,         3,
    ///      9,         8,         9,         8,         4,         4,
    ///         ]          ]          ]          ]          ]          ]
    /// </code></remarks>
    IVerticalDataLayoutBuilder<TDataType> WithVerticalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false);
}

/// <summary>
/// The builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.<br/>
/// Use the <see cref="WithArgument{TDataType}"/> or the <see cref="WithArgument{TButtonType,TExtraArgument}"/> method
/// to pass in the necessary arguments.
/// </summary>
/// <typeparam name="TDataType"></typeparam>
public interface IFinishingBuilderAccess<TDataType>
{
    /// <summary>
    /// Pass in the necessary arguments to the building <see cref="IVirtualGridView{TDataType}"/>
    /// </summary>
    /// <param name="itemPrefab">The <see cref="PackedScene"/> used for the virtualized grid element
    /// that have a script inherits <see cref="VirtualGridViewItem{TDataType,TExtraArgument}"/> attached.</param>
    /// <param name="itemContainer">The <see cref="Control"/> used for the container of all virtualized grid elements.</param>
    /// <param name="layoutGrid">The <see cref="InfinitLayoutGrids"/> used to handle the layout positioning of all virtualized grid elements.</param>
    /// <typeparam name="TButtonType">The type of the script attached to the <paramref name="itemPrefab"/>.</typeparam>
    /// <typeparam name="TExtraArgument">The extra argument passed to the script attached to the virtualized grid elements.</typeparam>
    /// <returns>A builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
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
    /// <param name="layoutGrid">The <see cref="InfinitLayoutGrids"/> used to handle the layout positioning of all virtualized grid elements.</param>
    /// <typeparam name="TButtonType">The type of the script attached to the <paramref name="itemPrefab"/>.</typeparam>
    /// <returns>A builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
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

public interface IHorizontalDataLayoutBuilder<TDataType> : IFinishingBuilderAccess<TDataType>
{
    IHorizontalDataLayoutBuilder<TDataType> AddRowDataSource(IDynamicGridViewer<TDataType> dataSet, int repeatCount = 1);
}

public interface IVerticalDataLayoutBuilder<TDataType> : IFinishingBuilderAccess<TDataType>
{
    IVerticalDataLayoutBuilder<TDataType> AddColumnDataSource(IDynamicGridViewer<TDataType> dataSet, int repeatCount = 1);
}

public interface IFinishingArgumentBuilder<TDataType, TButtonType, in TExtraArgument> where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    IVirtualGridView<TDataType> Build();

    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureHorizontalScrollBar(
        ScrollBar horizontalScrollBar,
        IScrollBarTweener? tweener = null,
        IElementFader? fader = null,
        bool autoHide = false
    );

    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureVerticalScrollBar(
        ScrollBar verticalScrollBar,
        IScrollBarTweener? tweener = null,
        IElementFader? fader = null,
        bool autoHide = false
    );

    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureExtraArgument(TExtraArgument extraArgument);
}