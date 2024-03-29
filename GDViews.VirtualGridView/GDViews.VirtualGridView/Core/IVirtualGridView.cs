namespace GodotViews.VirtualGrid;

public interface IVirtualGridView<TDataType, TButtonType>
{
    void Redraw();
    void Move(MoveDirection moveDirection);
    int ViewColumnIndex { get; }
    int ViewRowIndex { get; }
}