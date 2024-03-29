using Godot;

namespace GodotViews.VirtualGrid;

public interface IFinishingArgumentBuilder<TDataType, TButtonType>
{
    IVirtualGridView<TDataType, TButtonType> Build();
    IFinishingArgumentBuilder<TDataType, TButtonType> ConfigureHorizontalScrollBar(ScrollBar scrollBar);
    IFinishingArgumentBuilder<TDataType, TButtonType> ConfigureVerticalScrollBar(ScrollBar scrollBar);
}