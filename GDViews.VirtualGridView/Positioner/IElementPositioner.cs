using Godot;

namespace GodotViews.VirtualGrid.Positioner;

/// <summary>
/// Element Positioner is responsible for calculating the positioning of the virtual viewport when focusing on a grid element.<br/>
/// You may access a set of built-in element positioners from the <see cref="ElementPositioners"/> class.
/// </summary>
/// <remarks>
/// The <see cref="IVirtualGridView{TDataType}"/> abstracted the virtualized viewport positioning to two method calls,
/// developers may inherit this interface and create their customized element positioners.
/// </remarks>
public interface IElementPositioner
{
    /// <summary>
    /// Calculate the desired position of the focused grid element.
    /// </summary>
    /// <param name="viewportSize">The size of the viewport.</param>
    /// <param name="positionRelativeToViewport">The current position of the focused grid element relative to the viewport, this value can lie outside of the viewport.</param>
    /// <param name="destPositionRelativeToViewport">The calculated destination position for the focused grid element relative to the viewport.</param>
    void GetTargetPosition(Vector2I viewportSize, Vector2I positionRelativeToViewport, out Vector2I destPositionRelativeToViewport);

    /// <summary>
    /// Calculate the the desired position of the focused grid element when dragging the view.
    /// </summary>
    /// <param name="viewportSize">The size of the viewport.</param>
    /// <param name="dragDirection">The dragging direction.</param>
    /// <param name="positionRelativeToViewport">The current position of the focused grid element relative to the viewport, this value can lie outside of the viewport.</param>
    /// <param name="destPositionRelativeToViewport">The calculated destination position for the focused grid element relative to the viewport.</param>
    /// <remarks>
    /// When the player drags the view, it is counter-intuitive to
    /// simply map the drag action to per-grid element UI navigation.<br/>
    /// For example, when using the <see cref="ElementPositioners.Side"/> positioner,
    /// the player is expecting to drag the "viewport", not moving the cursor like Keyboard or Gamepad movement.<br/>
    /// This method is here, under the side circumstances, to move the cursor to the viewport side
    /// so that the drag motion always translates to the emulated "viewport" movement. 
    /// </remarks>
    void GetDragViewPosition(Vector2I viewportSize, MoveDirection dragDirection, Vector2I positionRelativeToViewport, out Vector2I destPositionRelativeToViewport) => 
        destPositionRelativeToViewport = positionRelativeToViewport;
}