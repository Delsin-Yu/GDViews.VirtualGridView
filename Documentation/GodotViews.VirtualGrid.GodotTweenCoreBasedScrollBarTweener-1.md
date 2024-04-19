# <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedScrollBarTweener_1"></a> Class GodotTweenCoreBasedScrollBarTweener<TCachedArgument\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

This implementation of the <xref href="GodotViews.VirtualGrid.IScrollBarTweener" data-throw-if-not-resolved="false"></xref> leverages the <xref href="Godot.Tween" data-throw-if-not-resolved="false"></xref> system for handling transitions,
this type handles necessary initialization and interruptions, and leaves blank the actual tween logic abstract.
The developer may inherit this type for creating their customized scroll bar tweener.

```csharp
public abstract class GodotTweenCoreBasedScrollBarTweener<TCachedArgument> : IGodotTweenScrollBarTweener, IGodotTween, IScrollBarTweener
```

#### Type Parameters

`TCachedArgument` 

The argument that gets cached for each running tween action, per control,
    it is useful for restoring or fast-forwarding the controls' state when interrupting an existing tween or starting a new one.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[GodotTweenCoreBasedScrollBarTweener<TCachedArgument\>](GodotViews.VirtualGrid.GodotTweenCoreBasedScrollBarTweener\-1.md)

#### Implements

[IGodotTweenScrollBarTweener](GodotViews.VirtualGrid.IGodotTweenScrollBarTweener.md), 
[IGodotTween](GodotViews.VirtualGrid.IGodotTween.md), 
[IScrollBarTweener](GodotViews.VirtualGrid.IScrollBarTweener.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedScrollBarTweener_1__ctor_System_Single_GodotViews_VirtualGrid_TweenSetup_"></a> GodotTweenCoreBasedScrollBarTweener\(float, TweenSetup\)

Construct an instance of this <xref href="GodotViews.VirtualGrid.GodotTweenCoreBasedScrollBarTweener%601" data-throw-if-not-resolved="false"></xref>

```csharp
protected GodotTweenCoreBasedScrollBarTweener(float duration, TweenSetup tweenSetup)
```

#### Parameters

`duration` [float](https://learn.microsoft.com/dotnet/api/system.single)

The duration takes to complete the interpolation.

`tweenSetup` [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)

The <xref href="GodotViews.VirtualGrid.GodotTweenCoreBasedScrollBarTweener%601.TweenSetup" data-throw-if-not-resolved="false"></xref> used for doing the interpolation.

## Properties

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedScrollBarTweener_1_Duration"></a> Duration

The duration takes to complete the interpolation.

```csharp
public float Duration { get; set; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedScrollBarTweener_1_TweenSetup"></a> TweenSetup

ZThe <xref href="GodotViews.VirtualGrid.GodotTweenCoreBasedScrollBarTweener%601.TweenSetup" data-throw-if-not-resolved="false"></xref> used for doing the interpolation.

```csharp
public TweenSetup TweenSetup { get; set; }
```

#### Property Value

 [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)

## Methods

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedScrollBarTweener_1_FastForwardState_Godot_Control__0_"></a> FastForwardState\(Control, TCachedArgument\)

Invoked before starting an interpolation, developer should use the specified value
to fast-forward the state of the affected properties for the provided control.

```csharp
public void FastForwardState(Control control, TCachedArgument previousTarget)
```

#### Parameters

`control` Control

The control to have its affected properties fast-forwarded.

`previousTarget` TCachedArgument

The cached argument for this control.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedScrollBarTweener_1_FastForwardState_Godot_ScrollBar__0_"></a> FastForwardState\(ScrollBar, TCachedArgument\)

Invoked before starting an interpolation, developer should use the specified value
to fast-forward the state of the affected properties for the provided scroll bar.

```csharp
protected abstract void FastForwardState(ScrollBar scrollBar, TCachedArgument previousTarget)
```

#### Parameters

`scrollBar` ScrollBar

The scroll bar to have its affected properties fast-forwarded.

`previousTarget` TCachedArgument

The cached argument for this scroll bar.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedScrollBarTweener_1_InitializeTween_GodotViews_VirtualGrid_NoExtraArgument_System_ValueTuple_System_Single_System_Single___Godot_Control_Godot_Tween_"></a> InitializeTween\(NoExtraArgument, in \(float targetValue, float targetPage\), Control, Tween\)

Invoked for setting up the tween for the target control.

```csharp
public TCachedArgument InitializeTween(NoExtraArgument type, in (float targetValue, float targetPage) targetValue, Control control, Tween tween)
```

#### Parameters

`type` [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

The type of the tween.

`targetValue` \([float](https://learn.microsoft.com/dotnet/api/system.single) [targetValue](https://learn.microsoft.com/dotnet/api/system.valuetuple\-system.single,system.single\-.targetvalue), [float](https://learn.microsoft.com/dotnet/api/system.single) [targetPage](https://learn.microsoft.com/dotnet/api/system.valuetuple\-system.single,system.single\-.targetpage)\)

The target position this control is meant to get moved to.

`control` Control

The control to move.

`tween` Tween

The setting up tween.

#### Returns

 TCachedArgument

The target argument this tween updates,
    this value will be use for fast-forwarding this control's state when interrupting the interpolation.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedScrollBarTweener_1_InitializeTween_System_ValueTuple_System_Single_System_Single___Godot_ScrollBar_Godot_Tween_"></a> InitializeTween\(in \(float targetValue, float targetPage\), ScrollBar, Tween\)

Invoked for setting up the tween for the target scroll bar.

```csharp
protected abstract TCachedArgument InitializeTween(in (float targetValue, float targetPage) targetValue, ScrollBar scrollBar, Tween tween)
```

#### Parameters

`targetValue` \([float](https://learn.microsoft.com/dotnet/api/system.single) [targetValue](https://learn.microsoft.com/dotnet/api/system.valuetuple\-system.single,system.single\-.targetvalue), [float](https://learn.microsoft.com/dotnet/api/system.single) [targetPage](https://learn.microsoft.com/dotnet/api/system.valuetuple\-system.single,system.single\-.targetpage)\)

The target value this scroll bar is meant to get updated.

`scrollBar` ScrollBar

The scroll bar to update.

`tween` Tween

The setting up tween.

#### Returns

 TCachedArgument

The target argument this tween updates,
    this value will be use for fast-forwarding this scroll bar's state when interrupting the interpolation.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedScrollBarTweener_1_IsTweenSupported_GodotViews_VirtualGrid_NoExtraArgument_"></a> IsTweenSupported\(NoExtraArgument\)

Use to check if the specified type of interpolation is supported by this fader.

```csharp
public bool IsTweenSupported(NoExtraArgument type)
```

#### Parameters

`type` [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

The type for the checking interpolation.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if supports the specified type;
otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedScrollBarTweener_1_KillTween_Godot_ScrollBar_"></a> KillTween\(ScrollBar\)

Invoked when the view controller is about to perform a viewport moving action.<br />
The developer should interrupt and clean up the existing tweens for the given scrollBar.

```csharp
public void KillTween(ScrollBar scrollBar)
```

#### Parameters

`scrollBar` ScrollBar

The <xref href="Godot.ScrollBar" data-throw-if-not-resolved="false"></xref> that should have its associated tween interrupted.

### <a id="GodotViews_VirtualGrid_GodotTweenCoreBasedScrollBarTweener_1_UpdateValue_Godot_ScrollBar_System_Single_System_Single_"></a> UpdateValue\(ScrollBar, float, float\)

Invoked when the view controller is updating the values of a <xref href="Godot.ScrollBar" data-throw-if-not-resolved="false"></xref>.

```csharp
public void UpdateValue(ScrollBar scrollBar, float targetValue, float targetPage)
```

#### Parameters

`scrollBar` ScrollBar

The scroll bar to have its value updated.

`targetValue` [float](https://learn.microsoft.com/dotnet/api/system.single)

The target <xref href="Godot.Range.Value" data-throw-if-not-resolved="false"></xref> value.

`targetPage` [float](https://learn.microsoft.com/dotnet/api/system.single)

The target <xref href="Godot.Range.Page" data-throw-if-not-resolved="false"></xref> value.

