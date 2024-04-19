# <a id="GodotViews_VirtualGrid_IElementFader"></a> Interface IElementFader

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Element Fader is responsible for managing the hiding and showing of the virtualized elements.<br />
You may access a set of built-in element faders from the <xref href="GodotViews.VirtualGrid.ElementFaders" data-throw-if-not-resolved="false"></xref> class.

```csharp
public interface IElementFader
```

## Remarks

The <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> abstracted the virtualized element hiding and showing to three separate method calls,
developers may inherit this interface and create their customized element faders.

## Methods

### <a id="GodotViews_VirtualGrid_IElementFader_Appear_Godot_Control_"></a> Appear\(Control\)

Invoked when the view controller is showing a virtualized element.

```csharp
void Appear(Control control)
```

#### Parameters

`control` Control

The virtualized element that gets shown.

### <a id="GodotViews_VirtualGrid_IElementFader_Disappear_Godot_Control_System_Action_Godot_Control__"></a> Disappear\(Control, Action<Control\>\)

Invoked when the view controller is hiding a virtualized element.

```csharp
void Disappear(Control control, Action<Control> onFinish)
```

#### Parameters

`control` Control

The virtualized element that gets hidden.

`onFinish` [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<Control\>

Invoked by the fader when finishing the hiding process,
    this delegate notifies the controller to hide and cache the element.

### <a id="GodotViews_VirtualGrid_IElementFader_KillTween_Godot_Control_"></a> KillTween\(Control\)

Invoked when the view controller is about to perform a fading action.<br />
The developer should interrupt and clean up the existing fades for the given element.

```csharp
void KillTween(Control control)
```

#### Parameters

`control` Control

The virtualized element that should have its associated fade interrupted.

### <a id="GodotViews_VirtualGrid_IElementFader_Reinitialize_Godot_Control_"></a> Reinitialize\(Control\)

Invoked when the view controller is caching a virtualized element.<br />
The developer should reset every value affected by this element fader
to their initial state.

```csharp
void Reinitialize(Control control)
```

#### Parameters

`control` Control

The virtualized element that gets reset.

