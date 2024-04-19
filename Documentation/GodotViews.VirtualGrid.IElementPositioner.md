# <a id="GodotViews_VirtualGrid_IElementPositioner"></a> Interface IElementPositioner

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Element Positioner is responsible for calculating the positioning of the virtual viewport when focusing on a grid element.<br />
You may access a set of built-in element positioners from the <xref href="GodotViews.VirtualGrid.ElementPositioners" data-throw-if-not-resolved="false"></xref> class.

```csharp
public interface IElementPositioner
```

## Remarks

The <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> abstracted the virtualized viewport positioning to two method calls,
developers may inherit this interface and create their customized element positioners.

## Methods

### <a id="GodotViews_VirtualGrid_IElementPositioner_GetDragViewPosition_Godot_Vector2I_GodotViews_VirtualGrid_MoveDirection_Godot_Vector2I_Godot_Vector2I__"></a> GetDragViewPosition\(Vector2I, MoveDirection, Vector2I, out Vector2I\)

Calculate the the desired position of the focused grid element when dragging the view.

```csharp
void GetDragViewPosition(Vector2I viewportSize, MoveDirection dragDirection, Vector2I positionRelativeToViewport, out Vector2I destPositionRelativeToViewport)
```

#### Parameters

`viewportSize` Vector2I

The size of the viewport.

`dragDirection` [MoveDirection](GodotViews.VirtualGrid.MoveDirection.md)

The dragging direction.

`positionRelativeToViewport` Vector2I

The current position of the focused grid element relative to the viewport, this value can lie outside of the viewport.

`destPositionRelativeToViewport` Vector2I

The calculated destination position for the focused grid element relative to the viewport.

#### Remarks

When the player drags the view, it is counter-intuitive to
simply map the drag action to per-grid element UI navigation.<br />
For example, when using the <xref href="GodotViews.VirtualGrid.ElementPositioners.Side" data-throw-if-not-resolved="false"></xref> positioner,
the player is expecting to drag the "viewport", not moving the cursor like Keyboard or Gamepad movement.<br />
This method is here, under the side circumstances, to move the cursor to the viewport side
so that the drag motion always translates to the emulated "viewport" movement.

### <a id="GodotViews_VirtualGrid_IElementPositioner_GetTargetPosition_Godot_Vector2I_Godot_Vector2I_Godot_Vector2I__"></a> GetTargetPosition\(Vector2I, Vector2I, out Vector2I\)

Calculate the desired position of the focused grid element.

```csharp
void GetTargetPosition(Vector2I viewportSize, Vector2I positionRelativeToViewport, out Vector2I destPositionRelativeToViewport)
```

#### Parameters

`viewportSize` Vector2I

The size of the viewport.

`positionRelativeToViewport` Vector2I

The current position of the focused grid element relative to the viewport, this value can lie outside of the viewport.

`destPositionRelativeToViewport` Vector2I

The calculated destination position for the focused grid element relative to the viewport.

