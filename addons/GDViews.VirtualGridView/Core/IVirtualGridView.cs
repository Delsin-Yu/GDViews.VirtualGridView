using System;
using GodotViews.VirtualGrid.FocusFinding;
using GodotViews.VirtualGrid.Positioner;
using GodotViews.VirtualGrid.Transition;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Represents a controller that provides feature
/// to navigate through and customise the virtualized grid view.   
/// </summary>
/// <typeparam name="TDataType">The type for the data this controller focuses on.</typeparam>
public interface IVirtualGridView<TDataType> : IDisposable
{
    /// <summary> When sets to true, the user can drag the grid view to scroll through the content. </summary>
    bool EnableDragging { get; set; }

    /// <summary>
    /// Accessor for the currently active ElementPositioner,
    /// assigning null to it will automatically fallbacks to <see cref="ElementPositioners.Side"/>.
    /// </summary>
    IElementPositioner ElementPositioner { get; set; }

    /// <summary>
    /// Accessor for the currently active ElementTweener,
    /// assigning null to it will automatically fallbacks to <see cref="ElementTweeners.None"/>.
    /// </summary>
    IElementTweener ElementTweener { get; set; }

    /// <summary>
    /// Accessor for the currently active ElementFader,
    /// assigning null to it will automatically fallbacks to <see cref="ElementFaders.None"/>.
    /// </summary>
    IElementFader ElementFader { get; set; }

    /// <summary>
    /// Accessor for the currently active Horizontal ScrollBarTweener,
    /// assigning null to it will automatically fallbacks to <see cref="ScrollBarTweeners.None"/>.
    /// </summary>
    IScrollBarTweener HScrollBarTweener { get; set; }

    /// <summary>
    /// Accessor for the currently active Vertical ScrollBarTweener,
    /// assigning null to it will automatically fallbacks to <see cref="ScrollBarTweeners.None"/>.
    /// </summary>
    IScrollBarTweener VScrollBarTweener { get; set; }

    /// <summary>
    /// Accessor for the currently active Horizontal ScrollBarFader,
    /// assigning null to it will automatically fallbacks to <see cref="ElementFaders.None"/>.
    /// </summary>
    IElementFader HScrollBarFader { get; set; }

    /// <summary>
    /// Accessor for the currently active Vertical ScrollBarFader,
    /// assigning null to it will automatically fallbacks to <see cref="ElementFaders.None"/>.
    /// </summary>
    IElementFader VScrollBarFader { get; set; }

    /// <summary>
    /// When sets to true, the <see cref="IVirtualGridView{TDataType}"/> will hide the horizontal scroll bar
    /// when the current viewport is horizontally sufficient for showing every element of the datasets.
    /// </summary>
    bool AutoHideHScrollBar { get; set; }

    /// <summary>
    /// When sets to true, the <see cref="IVirtualGridView{TDataType}"/> will hide the vertical scroll bar
    /// when the current viewport is vertically sufficient for showing every element of the datasets.
    /// </summary>
    bool AutoHideVScrollBar { get; set; }

    /// <summary>
    /// The number of xs for the concurrently displayed virtualized grid items.
    /// </summary>
    int ViewXCount { get; }

    /// <summary>
    /// The number of ys for the concurrently displayed virtualized grid items.
    /// </summary>
    int ViewYCount { get; }


    /// <summary>
    /// Triggers a redraw of the current viewport, reflecting the external changes to the datasets.  
    /// </summary>
    void Redraw();

    /// <summary>
    /// Trying to create a focus on the first available element.
    /// </summary>
    /// <returns><see langword="true" /> if successfully grabs the focus; otherwise, <see langword="false" />.</returns>
    /// <remarks>
    /// Controller will try to establish a focus based on certain methods,
    /// and will automatically fallback to the alternative if the current method fails:<br/>
    /// <list type="number">
    ///     <item>
    ///         <description>Try to focus to the previous selected data value.</description>
    ///     </item>
    ///     <item>
    ///         <description>Try to focus to the previous selected view position.</description>
    ///     </item>
    ///     <item>
    ///         <description>Try to focus to the top left element of the viewport.</description>
    ///     </item>
    ///     <item>
    ///         <description>Try to focus to the top left element of the datasets.</description>
    ///     </item>
    ///     <item>
    ///         <description>Return false.</description>
    ///     </item>
    /// </list>
    /// </remarks>
    bool GrabFocus();

    /// <summary>
    /// Trying to create a focus on the element based on the specified <see cref="ViewFocusFinderPreset"/>.<br/>
    /// You may access a set of presets from the <see cref="FocusPresets"/> class.
    /// </summary>
    /// <param name="preset">The preset for specifying the focus-finding logic.</param>
    /// <returns><see langword="true" /> if successfully grabs the focus; otherwise, <see langword="false" />.</returns>
    bool GrabFocus(in ViewFocusFinderPreset preset) =>
        GrabFocus(
            preset.Preset.FocusFinder,
            preset.Preset.StartHandler,
            preset.Argument,
            preset.SearchDirection
        );

    /// <summary>
    /// Trying to create a focus on the element based on the specified <see cref="DataFocusFinderPreset"/>.
    /// You may access a set of presets from the <see cref="FocusPresets"/> class.
    /// </summary>
    /// <param name="preset">The preset for specifying the focus-finding logic.</param>
    /// <returns><see langword="true" /> if successfully grabs the focus; otherwise, <see langword="false" />.</returns>
    bool GrabFocus(in DataFocusFinderPreset preset) =>
        GrabFocus(
            preset.Preset.FocusFinder,
            preset.Preset.StartHandler,
            preset.Argument,
            preset.SearchDirection
        );

    /// <summary>
    /// Trying to create a focus on the element based on the specified <see cref="ViewFocusFinderPreset{TArgument}"/>.<br/>
    /// You may access a set of presets from the <see cref="FocusPresets"/> class.
    /// </summary>
    /// <param name="preset">The preset for specifying the focus-finding logic.</param>
    /// <param name="argument">The extra argument passes to the focus-finding logic.</param>
    /// <param name="searchDirection">The search direction for the focus-finding logic.</param>
    /// <typeparam name="TArgument">The type of the extra argument, specified by the <see cref="ViewFocusFinderPreset{TArgument}"/>.</typeparam>
    /// <returns><see langword="true" /> if successfully grabs the focus; otherwise, <see langword="false" />.</returns>
    bool GrabFocus<TArgument>(
        ViewFocusFinderPreset<TArgument> preset,
        TArgument argument,
        SearchDirection searchDirection
    ) =>
        GrabFocus(
            preset.FocusFinder,
            preset.StartHandler,
            argument,
            searchDirection
        );

    /// <summary>
    /// Trying to create a focus on the element based on the specified <see cref="DataFocusFinderPreset{TArgument}"/>.<br/>
    /// You may access a set of presets from the <see cref="FocusPresets"/> class.
    /// </summary>
    /// <param name="preset">The preset for specifying the focus-finding logic.</param>
    /// <param name="argument">The extra argument passes to the focus-finding logic.</param>
    /// <param name="searchDirection">The search direction for the focus-finding logic.</param>
    /// <typeparam name="TArgument">The type of the extra argument, specified by the <see cref="DataFocusFinderPreset{TArgument}"/>.</typeparam>
    /// <returns><see langword="true" /> if successfully grabs the focus; otherwise, <see langword="false" />.</returns>
    bool GrabFocus<TArgument>(
        DataFocusFinderPreset<TArgument> preset,
        TArgument argument,
        SearchDirection searchDirection
    ) =>
        GrabFocus(
            preset.FocusFinder,
            preset.StartHandler,
            argument,
            searchDirection
        );

    /// <summary>
    /// Trying to create a focus on the element based on the specified arguments.<br/>
    /// You may access a set of view focus finders from the <see cref="FocusFinders"/> class,
    /// and a set of view start handlers from the <see cref="StartHandlers"/> class.
    /// </summary>
    /// <param name="focusFinder">The <see cref="IViewFocusFinder{TArgument}"/> that provide the focus-finding logic.</param>
    /// <param name="startHandler">The <see cref="IViewStartHandler{TArgument}"/> that provide the search-starting position.</param>
    /// <param name="argument">The extra argument passes to the focus-finding logic.</param>
    /// <param name="searchDirection">The search direction for the focus-finding logic.</param>
    /// <typeparam name="TArgument">The type of the extra argument, specified by the
    /// <see cref="IViewFocusFinder{TArgument}"/> and <see cref="IViewStartHandler{TArgument}"/>.</typeparam>
    /// <returns><see langword="true" /> if successfully grabs the focus; otherwise, <see langword="false" />.</returns>
    bool GrabFocus<TArgument>(
        IViewFocusFinder<TArgument> focusFinder,
        IViewStartHandler<TArgument> startHandler,
        TArgument argument,
        SearchDirection searchDirection
    );

    /// <summary>
    /// Trying to create a focus on the element based on the specified arguments.<br/>
    /// You may access a set of data focus finders from the <see cref="FocusFinders"/> class,
    /// and a set of data start handlers from the <see cref="StartHandlers"/> class.
    /// </summary>
    /// <param name="focusFinder">The <see cref="IDataFocusFinder{TArgument}"/> that provide the focus-finding logic.</param>
    /// <param name="startHandler">The <see cref="IDataStartHandler{TArgument}"/> that provide the search-starting position.</param>
    /// <param name="argument">The extra argument passes to the focus-finding logic.</param>
    /// <param name="searchDirection">The search direction for the focus-finding logic.</param>
    /// <typeparam name="TArgument">The type of the extra argument, specified by the
    /// <see cref="IDataFocusFinder{TArgument}"/> and <see cref="IDataStartHandler{TArgument}"/>.</typeparam>
    /// <returns><see langword="true" /> if successfully grabs the focus; otherwise, <see langword="false" />.</returns>
    bool GrabFocus<TArgument>(
        IDataFocusFinder<TArgument> focusFinder,
        IDataStartHandler<TArgument> startHandler,
        TArgument argument,
        SearchDirection searchDirection
    );

    /// <summary>
    /// Trying to create a focus on the element based on the specified <see cref="IEqualityDataFocusFinder"/>.<br/>
    /// </summary>
    /// <param name="focusFinder">The <see cref="IEqualityDataFocusFinder"/> that provide the focus-finding logic.</param>
    /// <param name="matchingArgument">The argument passes to the focus-finding logic that uses for performing the matching.</param>
    /// <returns><see langword="true" /> if successfully grabs the focus; otherwise, <see langword="false" />.</returns>
    bool GrabFocus(
        IEqualityDataFocusFinder focusFinder,
        in TDataType matchingArgument
    );

    /// <summary>
    /// Trying to create a focus on the element based on the specified <see cref="IPredicateDataFocusFinder"/>.<br/>
    /// </summary>
    /// <param name="focusFinder">The <see cref="IPredicateDataFocusFinder"/> that provide the focus-finding logic.</param>
    /// <param name="predicate">The predicate passes to the focus-finding logic that uses for performing the matching.</param>
    /// <returns><see langword="true" /> if successfully grabs the focus; otherwise, <see langword="false" />.</returns>
    bool GrabFocus(
        IPredicateDataFocusFinder focusFinder,
        Predicate<TDataType> predicate
    );

    /// <summary>
    /// Trying to create a focus on the element based on the specified <see cref="IPredicateDataFocusFinder"/>.<br/>
    /// </summary>
    /// <param name="focusFinder">The <see cref="IPredicateDataFocusFinder"/> that provide the focus-finding logic.</param>
    /// <param name="predicate">The predicate passes to the focus-finding logic that uses for performing the matching.</param>
    /// <param name="extraArgument">The predicate passes to the <paramref name="predicate"/> to avoid closure allocation.</param>
    /// <returns><see langword="true" /> if successfully grabs the focus; otherwise, <see langword="false" />.</returns>
    bool GrabFocus<TExtraArgument>(
        IPredicateDataFocusFinder focusFinder,
        Func<TDataType, TExtraArgument, bool> predicate,
        TExtraArgument extraArgument
    );
}

interface IVirtualGridViewParent<TDataType, TExtraArgument>
{
    TExtraArgument? ExtraArgument { get; }
    void FocusTo(VirtualGridViewItemArg<TDataType, TExtraArgument>.CellInfo info);
    void MoveAndGrabFocus(MoveDirection moveDirection, int xIndex, int yIndex);
}