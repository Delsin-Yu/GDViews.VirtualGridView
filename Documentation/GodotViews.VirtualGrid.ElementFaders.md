# <a id="GodotViews_VirtualGrid_ElementFaders"></a> Class ElementFaders

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Provides a set of built-in <xref href="GodotViews.VirtualGrid.IElementFader" data-throw-if-not-resolved="false"></xref> that should cover common UI/UX development needs.

```csharp
public static class ElementFaders
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ElementFaders](GodotViews.VirtualGrid.ElementFaders.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="GodotViews_VirtualGrid_ElementFaders_None"></a> None

This element fader does not perform any form of visual transitions,
it is also the most efficient and snappy choice if the absolute performance is required.

```csharp
public static readonly IElementFader None
```

#### Field Value

 [IElementFader](GodotViews.VirtualGrid.IElementFader.md)

## Methods

### <a id="GodotViews_VirtualGrid_ElementFaders_CreateFade_System_Single_System_Nullable_GodotViews_VirtualGrid_TweenSetup__"></a> CreateFade\(float, TweenSetup?\)

Create an element fader that does <xref href="Godot.CanvasItem.Modulate" data-throw-if-not-resolved="false"></xref> interpolation based on the provided arguments.

```csharp
public static IGodotTweenFader CreateFade(float duration, TweenSetup? tweenSetup = null)
```

#### Parameters

`duration` [float](https://learn.microsoft.com/dotnet/api/system.single)

The duration this fader takes to do finish interpolation.

`tweenSetup` [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)?

The <xref href="GodotViews.VirtualGrid.TweenSetup" data-throw-if-not-resolved="false"></xref> fader uses when doing the interpolation.

#### Returns

 [IGodotTweenFader](GodotViews.VirtualGrid.IGodotTweenFader.md)

The instance of the created element fader that
    can be passed to the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
    for handling the virtual grid element fade interpolation.

### <a id="GodotViews_VirtualGrid_ElementFaders_CreateScale_System_Single_System_Nullable_GodotViews_VirtualGrid_TweenSetup__"></a> CreateScale\(float, TweenSetup?\)

Create an element fader that does <xref href="Godot.Control.Scale" data-throw-if-not-resolved="false"></xref> interpolation based on the provided arguments.

```csharp
public static IGodotTweenFader CreateScale(float duration, TweenSetup? tweenSetup = null)
```

#### Parameters

`duration` [float](https://learn.microsoft.com/dotnet/api/system.single)

The duration this fader takes to do finish interpolation.

`tweenSetup` [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)?

The <xref href="GodotViews.VirtualGrid.TweenSetup" data-throw-if-not-resolved="false"></xref> fader uses when doing the interpolation.

#### Returns

 [IGodotTweenFader](GodotViews.VirtualGrid.IGodotTweenFader.md)

The instance of the created element fader that
    can be passed to the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
    for handling the virtual grid element fade interpolation.

### <a id="GodotViews_VirtualGrid_ElementFaders_CreateScaleRotate_System_Single_System_Nullable_GodotViews_VirtualGrid_TweenSetup__"></a> CreateScaleRotate\(float, TweenSetup?\)

Create an element fader that does <xref href="Godot.Control.Scale" data-throw-if-not-resolved="false"></xref> and <xref href="Godot.Control.Rotation" data-throw-if-not-resolved="false"></xref> interpolation based on the provided arguments.

```csharp
public static IGodotTweenFader CreateScaleRotate(float duration, TweenSetup? tweenSetup = null)
```

#### Parameters

`duration` [float](https://learn.microsoft.com/dotnet/api/system.single)

The duration this fader takes to do finish interpolation.

`tweenSetup` [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)?

The <xref href="GodotViews.VirtualGrid.TweenSetup" data-throw-if-not-resolved="false"></xref> fader uses when doing the interpolation.

#### Returns

 [IGodotTweenFader](GodotViews.VirtualGrid.IGodotTweenFader.md)

The instance of the created element fader that
    can be passed to the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
    for handling the virtual grid element fade interpolation.

