using Godot;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid.Builder;

/// <summary>
/// The builder that concludes the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.<br/>
/// </summary>
/// <typeparam name="TDataType">The type for the data the building <see cref="IVirtualGridView{TDataType}"/> focuses on.</typeparam>
/// <typeparam name="TButtonType">The type of the script attached to the virtualized grid element.</typeparam>
/// <typeparam name="TExtraArgument">The extra argument passed to the script attached to the virtualized grid elements.</typeparam>
public interface IFinishingArgumentBuilder<TDataType, TButtonType, in TExtraArgument> where TButtonType : VirtualGridViewItemArg<TDataType, TExtraArgument>
{
    /// <summary>
    /// Finish the final building configuration, and instantiate the <see cref="IVirtualGridView{TDataType}"/> instance. 
    /// </summary>
    /// <returns>The instantiated <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    IVirtualGridView<TDataType> Build();

    /// <summary>
    /// Sets the same <see cref="FocusEdgeBehavior"/> for both horizontal and vertical dataset edges.
    /// </summary>
    /// <param name="behavior">The edge behavior applied to both axes.</param>
    /// <returns>The same builder for further configuration.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> SetFocusEdgeBehavior(FocusEdgeBehavior behavior);

    /// <summary>
    /// Sets <see cref="FocusEdgeBehavior"/> independently for horizontal and vertical dataset edges.
    /// </summary>
    /// <param name="horizontal">Behavior for left/right edges.</param>
    /// <param name="vertical">Behavior for up/down edges.</param>
    /// <returns>The same builder for further configuration.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> SetFocusEdgeBehavior(
        FocusEdgeBehavior horizontal,
        FocusEdgeBehavior vertical
    );

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
    /// Assign a <see cref="ScrollBar"/> to the building <see cref="IVirtualGridView{TDataType}"/>
    /// as an interactive horizontal progress indicator that can also drive the viewport position.
    /// Scroll-bar value tweening is disabled in interactive mode.
    /// </summary>
    /// <param name="horizontalScrollBar">The <see cref="ScrollBar"/> to associate to.</param>
    /// <param name="fader">The <see cref="IElementFader"/> used for hiding or showing the scroll bar.</param>
    /// <param name="autoHide">Instructs the <see cref="IVirtualGridView{TDataType}"/> to hide the scroll bar
    /// when the current viewport is horizontally sufficient for showing every element of the datasets.</param>
    /// <returns>The same <see cref="IFinishingArgumentBuilder{TDataType,TButtonType,TExtraArgument}"/>
    /// for continuing the configuration of this <see cref="IVirtualGridView{TDataType}"/>.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureInteractiveHorizontalScrollBar(
        ScrollBar horizontalScrollBar,
        IElementFader? fader = null,
        bool autoHide = false
    );

    /// <summary>
    /// Assign a <see cref="ScrollBar"/> to the building <see cref="IVirtualGridView{TDataType}"/>
    /// as an interactive vertical progress indicator that can also drive the viewport position.
    /// Scroll-bar value tweening is disabled in interactive mode.
    /// </summary>
    /// <param name="verticalScrollBar">The <see cref="ScrollBar"/> to associate to.</param>
    /// <param name="fader">The <see cref="IElementFader"/> used for hiding or showing the scroll bar.</param>
    /// <param name="autoHide">Instructs the <see cref="IVirtualGridView{TDataType}"/> to hide the scroll bar
    /// when the current viewport is vertically sufficient for showing every element of the datasets.</param>
    /// <returns>The same <see cref="IFinishingArgumentBuilder{TDataType,TButtonType,TExtraArgument}"/>
    /// for continuing the configuration of this <see cref="IVirtualGridView{TDataType}"/>.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureInteractiveVerticalScrollBar(
        ScrollBar verticalScrollBar,
        IElementFader? fader = null,
        bool autoHide = false
    );

    /// <summary>
    /// Registers extra <see cref="Control"/>s that also receive the same <c>GuiInput</c>
    /// scroll/drag handler as the item container. A consumed wheel marks the viewport input
    /// as handled so it does not propagate to other listeners.
    /// </summary>
    /// <param name="controls">One or more controls whose <c>GuiInput</c> should also drive scrolling.</param>
    /// <returns>The same <see cref="IFinishingArgumentBuilder{TDataType,TButtonType,TExtraArgument}"/>
    /// for continuing the configuration of this <see cref="IVirtualGridView{TDataType}"/>.</returns>
    IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureAdditionalScrollInput(params ReadOnlySpan<Control> controls);
}