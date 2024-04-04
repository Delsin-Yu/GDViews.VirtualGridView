namespace GodotViews.VirtualGrid;

internal interface IVirtualGridViewParent<TDataType, TExtraArgument>
{
    TExtraArgument? ExtraArgument { get; }
    void FocusTo(VirtualGridViewItem<TDataType, TExtraArgument>.CurrentInfo info);
    void MoveAndGrabFocus(MoveDirection moveDirection, int rowIndex, int columnIndex);
}