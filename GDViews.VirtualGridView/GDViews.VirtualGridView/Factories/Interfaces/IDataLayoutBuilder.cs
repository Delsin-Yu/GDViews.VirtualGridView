using System.Collections.Generic;

namespace GodotViews.VirtualGrid;

public interface IDataLayoutBuilder
{
    IHorizontalDataLayoutBuilder<TDataType> WithHorizontalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false);
    IVerticalDataLayoutBuilder<TDataType> WithVerticalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false);
}