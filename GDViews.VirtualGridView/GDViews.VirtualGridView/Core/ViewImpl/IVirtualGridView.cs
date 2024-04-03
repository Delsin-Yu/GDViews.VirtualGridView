using System;
using Godot;
using GodotViews.Core.FocusFinder;

namespace GodotViews.VirtualGrid;

public interface IVirtualGridView<TDataType, TButtonType, TExtraArgument> where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    void Redraw();
    
    IElementPositioner ElementPositioner { get; set; }
    IElementTweener ElementTweener { get; set; }
    IElementFader ElementFader { get; set; }
    
    int ViewColumns { get; }
    int ViewRows { get; }
    
    bool GrabFocus();
    bool GrabFocus(IViewFocusFinder focusFinder, StartPositionHandler startPositionHandler, SearchDirection searchDirection);
    bool GrabFocus(in ViewFocusFinderPreset focusFinderPreset) => GrabFocus(focusFinderPreset.ViewFocusFinder, focusFinderPreset.StartPosition, focusFinderPreset.SearchDirection);
    bool GrabFocus<TArgument>(IArgumentViewFocusFinder<TArgument> focusFinder, TArgument argument);
    bool GrabFocus(IDataFocusFinder<TDataType> focusFinder);
}

public enum LastFocusType
{
    LastViewFocus,
    LastDataFocus
}

public interface IViewFocusFinder
{
    public bool TryResolveFocus(ref readonly ReadOnlyViewArray currentView, ref readonly ReadOnlySpan<Vector2I> searchDirection, StartPositionHandler startPositionHandler, out int rowIndex, out int columnIndex);
}

public interface IArgumentViewFocusFinder<TArgument>
{
    public bool TryResolveFocus(ref readonly TArgument argument, ref readonly ReadOnlyViewArray currentView, out int rowIndex, out int columnIndex);
}
public interface IArgumentViewFocusFinder<TArgument1, TArgument2>
{
    public bool TryResolveFocus(ref readonly TArgument1 argument, ref readonly TArgument2 argument2, ref readonly ReadOnlyViewArray currentView, out int rowIndex, out int columnIndex);
}

public interface IDataFocusFinder<TDataType>
{
    public bool ResolveFocus(ref readonly TDataType data, int rowIndex, int columnIndex, int rowCount, int columnCount);
}