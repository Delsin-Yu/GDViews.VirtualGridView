using Godot;

namespace GodotViews.VirtualGrid;

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