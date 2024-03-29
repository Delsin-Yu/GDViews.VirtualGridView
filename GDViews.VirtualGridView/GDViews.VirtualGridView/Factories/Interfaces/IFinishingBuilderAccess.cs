using Godot;

namespace GodotViews.VirtualGrid;

public interface IFinishingBuilderAccess<TDataType>
{
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> WithArgument<TButtonType, TExtraArgument>(
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid) where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>;

    IFinishingArgumentBuilder<TDataType, TButtonType, NoExtraArgument> WithArgument<TButtonType>(PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid) where TButtonType : VirtualGridViewItem<TDataType, NoExtraArgument> =>
        WithArgument<TButtonType, NoExtraArgument>(
            itemPrefab,
            itemContainer,
            layoutGrid
        );
}