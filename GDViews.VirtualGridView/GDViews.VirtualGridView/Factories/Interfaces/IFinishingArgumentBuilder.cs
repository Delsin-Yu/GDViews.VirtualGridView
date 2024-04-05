using Godot;

namespace GodotViews.VirtualGrid;

public interface IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    IVirtualGridView<TDataType> Build();
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureHorizontalScrollBar(ScrollBar scrollBar);
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureVerticalScrollBar(ScrollBar scrollBar);
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureExtraArgument(TExtraArgument extraArgument);
}