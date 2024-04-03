﻿using System;
using Godot;

namespace GodotViews.VirtualGrid;

public interface IVirtualGridView<TDataType, TButtonType, TExtraArgument> where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    void Redraw();
    
    IElementPositioner ElementPositioner { get; set; }
    IElementTweener ElementTweener { get; set; }
    IElementFader ElementFader { get; set; }
    
    bool GrabFocus();
    bool GrabFocus(IViewFocusFinder focusFinder);
    bool GrabFocus(IDataFocusFinder<TDataType> focusFinder);
}

public enum LastFocusType
{
    LastViewFocus,
    LastDataFocus
}

public interface IViewFocusFinder
{
    public bool TryResolveFocus(ref readonly ReadOnly2DArray currentView, out int rowIndex, out int columnIndex);
}

public interface IDataFocusFinder<TDataType>
{
    public bool ResolveFocus(ref readonly TDataType data, int rowIndex, int columnIndex, int rowCount, int columnCount);
}