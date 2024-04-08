using System;

namespace GodotViews.VirtualGrid;

internal interface IVirtualGridViewParent<TDataType, TExtraArgument>
{
    TExtraArgument? ExtraArgument { get; }
    void FocusTo(VirtualGridViewItem<TDataType, TExtraArgument>.CellInfo info);
    void MoveAndGrabFocus(MoveDirection moveDirection, int rowIndex, int columnIndex);
}

public interface IVirtualGridView<TDataType>
{
    IElementPositioner ElementPositioner { get; set; }
    IElementTweener ElementTweener { get; set; }
    IElementFader ElementFader { get; set; }
    IScrollBarTweener HScrollBarTweener { get; set; }
    IScrollBarTweener VScrollBarTweener { get; set; }
    IElementFader HScrollBarFader { get; set; }
    IElementFader VScrollBarFader { get; set; }
    bool AutoHideHScrollBar { get; set; }
    bool AutoHideVScrollBar { get; set; }

    int ViewColumns { get; }
    int ViewRows { get; }
    void Redraw();

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

    bool GrabFocus<TExtraArgument>(
        IPredicateDataFocusFinder focusFinder,
        Func<TDataType, TExtraArgument, bool> matchingArgument,
        TExtraArgument extraArgument
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