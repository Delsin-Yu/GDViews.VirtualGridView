using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid;

public interface IViewHandlerBuilder
{
    IDataLayoutBuilder WithHandlers(IElementPositioner elementPositioner, IElementTweener elementTweener, IElementFader elementFader);
}

public interface IDataLayoutBuilder
{
    IHorizontalDataLayoutBuilder<TDataType> WithHorizontalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false);
    IVerticalDataLayoutBuilder<TDataType> WithVerticalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false);
}

public interface IFinishingBuilderAccess<TDataType>
{
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> WithArgument<TButtonType, TExtraArgument>(
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid
    ) where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>;

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
    IHorizontalDataLayoutBuilder<TDataType> AddRowDataSource(DataSetDefinition<TDataType> dataSetDefinition);
}

public interface IVerticalDataLayoutBuilder<TDataType> : IFinishingBuilderAccess<TDataType>
{
    IVerticalDataLayoutBuilder<TDataType> AddColumnDataSource(DataSetDefinition<TDataType> dataSetDefinition);
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