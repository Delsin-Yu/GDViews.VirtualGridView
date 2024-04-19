# <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener_1"></a> Class GodotTweenCoreBasedElementTweener<TCachedArgument\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

This implementation of the <xref href="GodotViews.VirtualGrid.IElementTweener" data-throw-if-not-resolved="false"></xref> leverages the <xref href="Godot.Tween" data-throw-if-not-resolved="false"></xref> system for handling transitions,
this type handles necessary initialization and interruptions, and leaves blank the actual tween logic abstract.
The developer may inherit this type for creating their customized element tweener.

```csharp
public abstract class GodotTweenCoreBasedElementTweener<TCachedArgument> : IGodotTweenTweener, IGodotTween, IElementTweener
```

#### Type Parameters

`TCachedArgument` 

The argument that gets cached for each running tween action, per control,
    it is useful for restoring or fast-forwarding the controls' state when interrupting an existing tween or starting a new one.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GodotTweenCoreBasedElementTweener<TCachedArgument\>](GodotViews.VirtualGrid.GodotTweenCoreBasedElementTweener\-1.md)

#### Implements

[IGodotTweenTweener](GodotViews.VirtualGrid.IGodotTweenTweener.md), 
[IGodotTween](GodotViews.VirtualGrid.IGodotTween.md), 
[IElementTweener](GodotViews.VirtualGrid.IElementTweener.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener_1__ctor_System_Single_GodotViews_VirtualGrid_TweenSetup_"></a> GodotTweenCoreBasedElementTweener\(float, TweenSetup\)

Construct an instance of this <xref href="GodotViews.VirtualGrid.GodotTweenCoreBasedElementTweener%601" data-throw-if-not-resolved="false"></xref>

```csharp
protected GodotTweenCoreBasedElementTweener(float duration, TweenSetup tweenSetup)
```

#### Parameters

`duration` [float](https://learn.microsoft.com/dotnet/api/system.single)

The duration takes to complete the interpolation.

`tweenSetup` [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)

The <xref href="GodotViews.VirtualGrid.GodotTweenCoreBasedElementTweener%601.TweenSetup" data-throw-if-not-resolved="false"></xref> used for doing the interpolation.

## Properties

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener_1_Duration"></a> Duration

The duration takes to complete the interpolation.

```csharp
public float Duration { get; set; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener_1_TweenSetup"></a> TweenSetup

ZThe <xref href="GodotViews.VirtualGrid.GodotTweenCoreBasedElementFader%601.TweenSetup" data-throw-if-not-resolved="false"></xref> used for doing the interpolation.

```csharp
public TweenSetup TweenSetup { get; set; }
```

#### Property Value

 [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)

## Methods

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener_1_FastForwardState_Godot_Control__0_"></a> FastForwardState\(Control, TCachedArgument\)

Invoked before starting an interpolation, developer should use the specified value
to fast-forward the state of the affected properties for the provided control.

```csharp
public abstract void FastForwardState(Control control, TCachedArgument previousTarget)
```

#### Parameters

`control` Control

The control to have its affected properties fast-forwarded.

`previousTarget` TCachedArgument

The cached argument for this control.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener_1_InitializeTween_GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener__0__TweenType_Godot_Vector2__Godot_Control_Godot_Tween_"></a> InitializeTween\(TweenType, in Vector2, Control, Tween\)

Invoked for setting up the tween for the target control.

```csharp
public abstract TCachedArgument InitializeTween(GodotTweenCoreBasedElementTweener<TCachedArgument>.TweenType type, in Vector2 targetValue, Control control, Tween tween)
```

#### Parameters

`type` [GodotTweenCoreBasedElementTweener](GodotViews.VirtualGrid.GodotTweenCoreBasedElementTweener\-1.md)<TCachedArgument\>.[TweenType](GodotViews.VirtualGrid.GodotTweenCoreBasedElementTweener\-1.TweenType.md)

The type of the tween.

`targetValue` Vector2

The target position this control is meant to get moved to.

`control` Control

The control to move.

`tween` Tween

The setting up tween.

#### Returns

 TCachedArgument

The target argument this tween updates,
    this value will be use for fast-forwarding this control's state when interrupting the interpolation.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener_1_IsTweenSupported_GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener__0__TweenType_"></a> IsTweenSupported\(TweenType\)

Use to check if the specified type of interpolation is supported by this fader.

```csharp
public abstract bool IsTweenSupported(GodotTweenCoreBasedElementTweener<TCachedArgument>.TweenType type)
```

#### Parameters

`type` [GodotTweenCoreBasedElementTweener](GodotViews.VirtualGrid.GodotTweenCoreBasedElementTweener\-1.md)<TCachedArgument\>.[TweenType](GodotViews.VirtualGrid.GodotTweenCoreBasedElementTweener\-1.TweenType.md)

The type for the checking interpolation.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if supports the specified type;
otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener_1_KillTween_Godot_Control_"></a> KillTween\(Control\)

Invoked when the view controller is about to perform a moving action.<br />
The developer should interrupt and clean up the existing tweens for the given element.

```csharp
public void KillTween(Control control)
```

#### Parameters

`control` Control

The virtualized element that should have its associated tween interrupted.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener_1_MoveIn_Godot_Control_Godot_Vector2_"></a> MoveIn\(Control, Vector2\)

Invoked when the view controller is moving a newly spawned or reused virtualized element into the viewport.

```csharp
public void MoveIn(Control control, Vector2 targetPosition)
```

#### Parameters

`control` Control

The newly spawned or reused virtualized element, outside of the viewport.

`targetPosition` Vector2

The target position this element is meant to get moves to.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener_1_MoveOut_Godot_Control_Godot_Vector2_System_Action_Godot_Control__"></a> MoveOut\(Control, Vector2, Action<Control\>\)

Invoked when the view controller is moving a virtualized element out from the viewport.

```csharp
public void MoveOut(Control control, Vector2 targetPosition, Action<Control> onFinish)
```

#### Parameters

`control` Control

The virtualized element to get moves out.

`targetPosition` Vector2

The target position this element is meant to get moves out to.

`onFinish` [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<Control\>

Invoked by the tweener when finishing the moving process,
    this delegate notifies the controller to hide and cache the element.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementTweener_1_MoveTo_Godot_Control_Godot_Vector2_"></a> MoveTo\(Control, Vector2\)

Invoked when the view controller is moving a virtualized element inside the viewport.

```csharp
public void MoveTo(Control control, Vector2 targetPosition)
```

#### Parameters

`control` Control

The moving virtualized element.

`targetPosition` Vector2

The target position this element is meant to get moves to.

