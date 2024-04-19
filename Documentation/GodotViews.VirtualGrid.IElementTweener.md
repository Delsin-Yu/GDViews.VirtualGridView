# <a id="GodotViews_VirtualGrid_IElementTweener"></a> Interface IElementTweener

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Element Tweener is responsible for managing the visual positional interpolation of the elements when user moves the virtualized viewport.<br />
You may access a set of built-in element tweeners from the <xref href="GodotViews.VirtualGrid.ElementTweeners" data-throw-if-not-resolved="false"></xref> class.

```csharp
public interface IElementTweener
```

## Remarks

The <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> abstracted the virtualized element movement to three separate method calls,
developers may inherit this interface and create their customized element tweeners.

## Methods

### <a id="GodotViews_VirtualGrid_IElementTweener_KillTween_Godot_Control_"></a> KillTween\(Control\)

Invoked when the view controller is about to perform a moving action.<br />
The developer should interrupt and clean up the existing tweens for the given element.

```csharp
void KillTween(Control control)
```

#### Parameters

`control` Control

The virtualized element that should have its associated tween interrupted.

### <a id="GodotViews_VirtualGrid_IElementTweener_MoveIn_Godot_Control_Godot_Vector2_"></a> MoveIn\(Control, Vector2\)

Invoked when the view controller is moving a newly spawned or reused virtualized element into the viewport.

```csharp
void MoveIn(Control control, Vector2 targetPosition)
```

#### Parameters

`control` Control

The newly spawned or reused virtualized element, outside of the viewport.

`targetPosition` Vector2

The target position this element is meant to get moves to.

### <a id="GodotViews_VirtualGrid_IElementTweener_MoveOut_Godot_Control_Godot_Vector2_System_Action_Godot_Control__"></a> MoveOut\(Control, Vector2, Action<Control\>\)

Invoked when the view controller is moving a virtualized element out from the viewport.

```csharp
void MoveOut(Control control, Vector2 targetPosition, Action<Control> onFinish)
```

#### Parameters

`control` Control

The virtualized element to get moves out.

`targetPosition` Vector2

The target position this element is meant to get moves out to.

`onFinish` [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<Control\>

Invoked by the tweener when finishing the moving process,
    this delegate notifies the controller to hide and cache the element.

### <a id="GodotViews_VirtualGrid_IElementTweener_MoveTo_Godot_Control_Godot_Vector2_"></a> MoveTo\(Control, Vector2\)

Invoked when the view controller is moving a virtualized element inside the viewport.

```csharp
void MoveTo(Control control, Vector2 targetPosition)
```

#### Parameters

`control` Control

The moving virtualized element.

`targetPosition` Vector2

The target position this element is meant to get moves to.

