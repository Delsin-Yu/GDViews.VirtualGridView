# <a id="GodotViews_VirtualGrid_TweenSetup"></a> Struct TweenSetup

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

The combination of <xref href="GodotViews.VirtualGrid.TweenSetup.EaseType" data-throw-if-not-resolved="false"></xref> and <xref href="GodotViews.VirtualGrid.TweenSetup.TransitionType" data-throw-if-not-resolved="false"></xref>.

```csharp
public record struct TweenSetup : IEquatable<TweenSetup>
```

#### Implements

[IEquatable<TweenSetup\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GodotViews_VirtualGrid_TweenSetup__ctor_Godot_Tween_EaseType_Godot_Tween_TransitionType_"></a> TweenSetup\(EaseType, TransitionType\)

The combination of <xref href="GodotViews.VirtualGrid.TweenSetup.EaseType" data-throw-if-not-resolved="false"></xref> and <xref href="GodotViews.VirtualGrid.TweenSetup.TransitionType" data-throw-if-not-resolved="false"></xref>.

```csharp
public TweenSetup(Tween.EaseType EaseType, Tween.TransitionType TransitionType)
```

#### Parameters

`EaseType` Tween.EaseType

The <xref href="GodotViews.VirtualGrid.TweenSetup.EaseType" data-throw-if-not-resolved="false"></xref> of this <xref href="GodotViews.VirtualGrid.TweenSetup" data-throw-if-not-resolved="false"></xref>.

`TransitionType` Tween.TransitionType

The <xref href="GodotViews.VirtualGrid.TweenSetup.TransitionType" data-throw-if-not-resolved="false"></xref> of this <xref href="GodotViews.VirtualGrid.TweenSetup" data-throw-if-not-resolved="false"></xref>.

## Properties

### <a id="GodotViews_VirtualGrid_TweenSetup_EaseType"></a> EaseType

The <xref href="GodotViews.VirtualGrid.TweenSetup.EaseType" data-throw-if-not-resolved="false"></xref> of this <xref href="GodotViews.VirtualGrid.TweenSetup" data-throw-if-not-resolved="false"></xref>.

```csharp
public Tween.EaseType EaseType { readonly get; set; }
```

#### Property Value

 Tween.EaseType

### <a id="GodotViews_VirtualGrid_TweenSetup_TransitionType"></a> TransitionType

The <xref href="GodotViews.VirtualGrid.TweenSetup.TransitionType" data-throw-if-not-resolved="false"></xref> of this <xref href="GodotViews.VirtualGrid.TweenSetup" data-throw-if-not-resolved="false"></xref>.

```csharp
public Tween.TransitionType TransitionType { readonly get; set; }
```

#### Property Value

 Tween.TransitionType

