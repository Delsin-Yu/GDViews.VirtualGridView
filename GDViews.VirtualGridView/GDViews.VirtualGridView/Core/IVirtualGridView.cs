using System;

namespace GodotViews.VirtualGrid;

public interface IVirtualGridView<TDataType, TButtonType, TExtraArgument> where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    void Redraw();
    void Move(MoveDirection moveDirection);
    bool GrabLastFocus(LastFocusType lastFocusType);
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
    public bool ResolveFocus(int rowIndex, int columnIndex, int rowCount, int columnCount);
}

public interface IDataFocusFinder<TDataType>
{
    public bool ResolveFocus(ref readonly TDataType data, int rowIndex, int columnIndex, int rowCount, int columnCount);
}