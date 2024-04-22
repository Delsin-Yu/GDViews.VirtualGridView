using System.Collections.Generic;

namespace GodotViews.VirtualGrid.Builder;

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