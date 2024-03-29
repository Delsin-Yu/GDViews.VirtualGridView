using Godot;

namespace GodotViews.VirtualGrid;

public abstract partial class VirtualGridViewItem<TDataType> : VirtualGridViewItem<TDataType, NoExtraArgument>
{
    protected sealed override void _OnGridItemDraw(TDataType data, Vector2I gridPosition, NoExtraArgument extraArgument) => _OnGridItemDraw(data, gridPosition);
    protected sealed override void _OnGridItemMove(TDataType data, Vector2I gridPosition, NoExtraArgument extraArgument) => _OnGridItemMove(data, gridPosition);
    protected sealed override void _OnGridItemMoveIn(TDataType data, Vector2I gridPosition, NoExtraArgument extraArgument) => _OnGridItemMoveIn(data, gridPosition);
    protected sealed override void _OnGridItemMoveOut(TDataType data, Vector2I gridPosition, NoExtraArgument extraArgument) => _OnGridItemMoveOut(data, gridPosition);
    protected sealed override void _OnGridItemAppear(TDataType data, Vector2I gridPosition, NoExtraArgument extraArgument) => _OnGridItemAppear(data, gridPosition);
    protected sealed override void _OnGridItemDisapper(NoExtraArgument extraArgument) => _OnGridItemDisappear();
    protected sealed override void _OnGridItemFocusEntered(TDataType data, Vector2I gridPosition, NoExtraArgument extraArgument) => _OnGridItemFocusEntered(data, gridPosition);
    protected sealed override void _OnGridItemFocusExited(TDataType data, Vector2I gridPosition, NoExtraArgument extraArgument) => _OnGridItemFocusExited(data, gridPosition);
    protected sealed override void _OnGridItemPressed(TDataType data, Vector2I gridPosition, NoExtraArgument extraArgument) => _OnGridItemPressed(data, gridPosition);


    protected virtual void _OnGridItemDraw(TDataType data, Vector2I gridPosition) { }
    protected virtual void _OnGridItemMove(TDataType data, Vector2I gridPosition) { }
    protected virtual void _OnGridItemMoveIn(TDataType data, Vector2I gridPosition) { }
    protected virtual void _OnGridItemMoveOut(TDataType data, Vector2I gridPosition) { }
    protected virtual void _OnGridItemAppear(TDataType data, Vector2I gridPosition) { }
    protected virtual void _OnGridItemDisappear() { }
    protected virtual void _OnGridItemFocusEntered(TDataType data, Vector2I gridPosition) { }
    protected virtual void _OnGridItemFocusExited(TDataType data, Vector2I gridPosition) { }
    protected virtual void _OnGridItemPressed(TDataType data, Vector2I gridPosition) { }
}