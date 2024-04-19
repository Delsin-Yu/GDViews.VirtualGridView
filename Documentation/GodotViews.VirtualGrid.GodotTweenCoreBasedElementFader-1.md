# <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader_1"></a> Class GodotTweenCoreBasedElementFader<TCachedArgument\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

This implementation of the <xref href="GodotViews.VirtualGrid.IElementFader" data-throw-if-not-resolved="false"></xref> leverages the <xref href="Godot.Tween" data-throw-if-not-resolved="false"></xref> system for handling transitions,
this type handles necessary initialization and interruptions, and leaves blank the actual tween logic abstract.
The developer may inherit this type for creating their customized element fader.

```csharp
public abstract class GodotTweenCoreBasedElementFader<TCachedArgument> : IGodotTweenFader, IGodotTween, IElementFader
```

#### Type Parameters

`TCachedArgument` 

The argument that gets cached for each running tween action, per control,
    it is useful for restoring or fast-forwarding the controls' state when interrupting an existing tween or starting a new one.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GodotTweenCoreBasedElementFader<TCachedArgument\>](GodotViews.VirtualGrid.GodotTweenCoreBasedElementFader\-1.md)

#### Implements

[IGodotTweenFader](GodotViews.VirtualGrid.IGodotTweenFader.md), 
[IGodotTween](GodotViews.VirtualGrid.IGodotTween.md), 
[IElementFader](GodotViews.VirtualGrid.IElementFader.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader_1__ctor_System_Single_GodotViews_VirtualGrid_TweenSetup_"></a> GodotTweenCoreBasedElementFader\(float, TweenSetup\)

Construct an instance of this <xref href="GodotViews.VirtualGrid.GodotTweenCoreBasedElementFader%601" data-throw-if-not-resolved="false"></xref>

```csharp
protected GodotTweenCoreBasedElementFader(float duration, TweenSetup tweenSetup)
```

#### Parameters

`duration` [float](https://learn.microsoft.com/dotnet/api/system.single)

The duration takes to complete the interpolation.

`tweenSetup` [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)

The <xref href="GodotViews.VirtualGrid.GodotTweenCoreBasedElementFader%601.TweenSetup" data-throw-if-not-resolved="false"></xref> used for doing the interpolation.

## Properties

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader_1_Duration"></a> Duration

The duration takes to complete the interpolation.

```csharp
public float Duration { get; set; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader_1_TweenSetup"></a> TweenSetup

ZThe <xref href="GodotViews.VirtualGrid.GodotTweenCoreBasedElementFader%601.TweenSetup" data-throw-if-not-resolved="false"></xref> used for doing the interpolation.

```csharp
public TweenSetup TweenSetup { get; set; }
```

#### Property Value

 [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)

## Methods

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader_1_Appear_Godot_Control_"></a> Appear\(Control\)

Invoked when the view controller is showing a virtualized element.

```csharp
public void Appear(Control control)
```

#### Parameters

`control` Control

The virtualized element that gets shown.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader_1_Disappear_Godot_Control_System_Action_Godot_Control__"></a> Disappear\(Control, Action<Control\>\)

Invoked when the view controller is hiding a virtualized element.

```csharp
public void Disappear(Control control, Action<Control> onFinish)
```

#### Parameters

`control` Control

The virtualized element that gets hidden.

`onFinish` [Action](https://learn.microsoft.com/dotnet/api/system.action\-1)<Control\>

Invoked by the fader when finishing the hiding process,
    this delegate notifies the controller to hide and cache the element.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader_1_FastForwardState_Godot_Control__0_"></a> FastForwardState\(Control, TCachedArgument\)

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

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader_1_InitializeTween_GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader__0__FadeType_Godot_Vector2__Godot_Control_Godot_Tween_"></a> InitializeTween\(FadeType, in Vector2, Control, Tween\)

Invoked for setting up the tween for the target control.

```csharp
public abstract TCachedArgument InitializeTween(GodotTweenCoreBasedElementFader<TCachedArgument>.FadeType type, in Vector2 targetValue, Control control, Tween tween)
```

#### Parameters

`type` [GodotTweenCoreBasedElementFader](GodotViews.VirtualGrid.GodotTweenCoreBasedElementFader\-1.md)<TCachedArgument\>.[FadeType](GodotViews.VirtualGrid.GodotTweenCoreBasedElementFader\-1.FadeType.md)

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

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader_1_IsTweenSupported_GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader__0__FadeType_"></a> IsTweenSupported\(FadeType\)

Use to check if the specified type of interpolation is supported by this fader.

```csharp
public abstract bool IsTweenSupported(GodotTweenCoreBasedElementFader<TCachedArgument>.FadeType type)
```

#### Parameters

`type` [GodotTweenCoreBasedElementFader](GodotViews.VirtualGrid.GodotTweenCoreBasedElementFader\-1.md)<TCachedArgument\>.[FadeType](GodotViews.VirtualGrid.GodotTweenCoreBasedElementFader\-1.FadeType.md)

The type for the checking interpolation.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if supports the specified type;
otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader_1_KillTween_Godot_Control_"></a> KillTween\(Control\)

Invoked when the view controller is about to perform a fading action.<br />
The developer should interrupt and clean up the existing fades for the given element.

```csharp
public void KillTween(Control control)
```

#### Parameters

`control` Control

The virtualized element that should have its associated fade interrupted.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedElementFader_1_Reinitialize_Godot_Control_"></a> Reinitialize\(Control\)

Invoked when the view controller is caching a virtualized element.<br />
The developer should reset every value affected by this element fader
to their initial state.

```csharp
public abstract void Reinitialize(Control control)
```

#### Parameters

`control` Control

The virtualized element that gets reset.

