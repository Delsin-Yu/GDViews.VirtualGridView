using System;
using GodotViews.Core.FocusFinder;

namespace GodotViews.VirtualGrid;

public interface IVirtualGridView<TDataType, TButtonType, TExtraArgument>
    where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    void Redraw();

    IElementPositioner ElementPositioner { get; set; }
    IElementTweener ElementTweener { get; set; }
    IElementFader ElementFader { get; set; }

    int ViewColumns { get; }
    int ViewRows { get; }

    bool GrabFocus();

    bool GrabFocus(
        IViewFocusFinder focusFinder,
        StartPositionHandler startPositionHandler,
        SearchDirection searchDirection
    );

    bool GrabFocus(in ViewFocusFinderPreset focusFinderPreset) =>
        GrabFocus(
            focusFinderPreset.ViewFocusFinder,
            focusFinderPreset.StartPosition,
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
    
    bool GrabFocus<TMatchingArgument>(
        IDataFocusFinder<TMatchingArgument> focusFinder,
        in TMatchingArgument matchingArgument
    );
}