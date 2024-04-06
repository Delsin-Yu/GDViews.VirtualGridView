using System;
using GodotViews.Core.FocusFinder;

namespace GodotViews.VirtualGrid;

public interface IVirtualGridView<TDataType>
{
    void Redraw();

    IElementPositioner ElementPositioner { get; set; }
    IElementTweener ElementTweener { get; set; }
    IElementFader ElementFader { get; set; }

    int ViewColumns { get; }
    int ViewRows { get; }

    bool GrabFocus();

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

    bool GrabFocus<TArgument>(
        IViewFocusFinder<TArgument> focusFinder,
        IViewStartHandler<TArgument> handler,
        TArgument argument,
        SearchDirection searchDirection
    );

    bool GrabFocus(in ViewFocusFinderPreset focusFinderPreset) =>
        GrabFocus(
            focusFinderPreset.Preset.FocusFinder,
            focusFinderPreset.Preset.StartHandler,
            focusFinderPreset.Argument,
            focusFinderPreset.SearchDirection
        );

    bool GrabFocus(
        IEqualityDataFocusFinder focusFinder,
        in TDataType matchingArgument
    );

    bool GrabFocus(
        IPredicateDataFocusFinder focusFinder,
        Predicate<TDataType> matchingArgument
    );

    bool GrabFocus<TArgument>(
        IDataFocusFinder<TArgument> focusFinder,
        IDataStartHandler<TArgument> startPositionHandler,
        TArgument matchingArgument,
        SearchDirection searchDirection
    );

    
    bool GrabFocus(in DataFocusFinderPreset focusFinderPreset) =>
        GrabFocus(
            focusFinderPreset.Preset.FocusFinder,
            focusFinderPreset.Preset.StartHandler,
            focusFinderPreset.Argument,
            focusFinderPreset.SearchDirection
        );
    
    bool GrabFocus<TArgument>(
        DataFocusFinderPreset<TArgument> preset,
        TArgument matchingArgument,
        SearchDirection searchDirection
    ) =>
        GrabFocus(
            preset.FocusFinder,
            preset.StartHandler,
            matchingArgument,
            searchDirection
        );
}