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
        IViewFocusFinder<TArgument> focusFinder,
        ViewStartPositionHandler<TArgument> startPositionHandler,
        TArgument argument,
        SearchDirection searchDirection
    );

    bool GrabFocus(in ViewFocusFinderPreset focusFinderPreset) =>
        GrabFocus(
            focusFinderPreset.FocusFinder,
            focusFinderPreset.StartPositionHandler,
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
        DataStartPositionHandler<TDataType, TArgument> startPositionHandler,
        TArgument matchingArgument,
        SearchDirection searchDirection
    );
}