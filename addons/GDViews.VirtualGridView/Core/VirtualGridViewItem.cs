using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Inherit this type to create a script that can be attached to a <see cref="PackedScene"/>
/// which makes it a valid prefab for use with <see cref="IVirtualGridView{TDataType}"/>.
/// </summary>
/// <typeparam name="TDataType">The type for the data the <see cref="IVirtualGridView{TDataType}"/> focuses on.</typeparam>
public partial class VirtualGridViewItem<TDataType> : VirtualGridViewItemArg<TDataType, NoExtraArgument>
{
    /// <inheritdoc />
    protected sealed override void _OnGridItemDraw(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument) => _OnGridItemDraw(data, viewPosition);

    /// <inheritdoc />
    protected sealed override void _OnGridItemMove(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument) => _OnGridItemMove(data, viewPosition);

    /// <inheritdoc />
    protected sealed override void _OnGridItemMoveIn(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument) => _OnGridItemMoveIn(data, viewPosition);

    /// <inheritdoc />
    protected sealed override void _OnGridItemMoveOut(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument) => _OnGridItemMoveOut(data, viewPosition);

    /// <inheritdoc />
    protected sealed override void _OnGridItemAppear(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument) => _OnGridItemAppear(data, viewPosition);

    /// <inheritdoc />
    protected sealed override void _OnGridItemDisapper(NoExtraArgument extraArgument) => _OnGridItemDisappear();

    /// <inheritdoc />
    protected sealed override void _OnGridItemFocusEntered(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument) => _OnGridItemFocusEntered(data, viewPosition);

    /// <inheritdoc />
    protected sealed override void _OnGridItemFocusExited(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument) => _OnGridItemFocusExited(data, viewPosition);

    /// <inheritdoc />
    protected sealed override void _OnGridItemPressed(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument) => _OnGridItemPressed(data, viewPosition);


    /// <summary>
    /// Invoked when the internal data of the current virtualized grid element instance
    /// has changed (or initialized) and requires developer-implemented draw logic.
    /// </summary>
    /// <param name="data">The data of the current virtualized grid element instance.</param>
    /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
    protected virtual void _OnGridItemDraw(TDataType data, Vector2I viewPosition) { }

    /// <summary>
    /// Invoked when the view controller is moving this virtualized grid element inside the viewport.
    /// </summary>
    /// <param name="data">The data of the current virtualized grid element instance.</param>
    /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
    protected virtual void _OnGridItemMove(TDataType data, Vector2I viewPosition) { }

    /// <summary>
    /// Invoked when the view controller is moving this newly spawned or
    /// reused virtualized grid element instance into the viewport.  
    /// </summary>
    /// <param name="data">The data of the current virtualized grid element instance.</param>
    /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
    protected virtual void _OnGridItemMoveIn(TDataType data, Vector2I viewPosition) { }


    /// <summary>
    /// Invoked when the view controller is moving this
    /// virtualized grid element instance out from the viewport.
    /// </summary>
    /// <param name="data">The data of the current virtualized grid element instance.</param>
    /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
    protected virtual void _OnGridItemMoveOut(TDataType data, Vector2I viewPosition) { }

    /// <summary>
    /// Invoked when the view controller is showing this virtualized grid element instance.
    /// </summary>
    /// <param name="data">The data of the current virtualized grid element instance.</param>
    /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
    protected virtual void _OnGridItemAppear(TDataType data, Vector2I viewPosition) { }

    /// <summary>
    /// Invoked when the view controller is hiding this virtualized grid element instance.
    /// </summary>
    protected virtual void _OnGridItemDisappear() { }

    /// <summary>
    /// Invoked when this virtualized grid element instance grabs focus.
    /// </summary>
    /// <param name="data">The data of the current virtualized grid element instance.</param>
    /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
    protected virtual void _OnGridItemFocusEntered(TDataType data, Vector2I viewPosition) { }

    /// <summary>
    /// Invoked when this virtualized grid element instance loses focus.
    /// </summary>
    /// <param name="data">The data of the current virtualized grid element instance.</param>
    /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
    protected virtual void _OnGridItemFocusExited(TDataType data, Vector2I viewPosition) { }

    /// <summary>
    /// Invoked when this virtualized grid element instance is pressed.
    /// </summary>
    /// <param name="data">The data of the current virtualized grid element instance.</param>
    /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
    protected virtual void _OnGridItemPressed(TDataType data, Vector2I viewPosition) { }
}