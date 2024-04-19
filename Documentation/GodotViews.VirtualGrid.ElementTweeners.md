# <a id="GodotViews_VirtualGrid_ElementTweeners"></a> Class ElementTweeners

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Provides a set of built-in <xref href="GodotViews.VirtualGrid.IElementTweener" data-throw-if-not-resolved="false"></xref> that should cover common UI/UX development needs.

```csharp
public static class ElementTweeners
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ElementTweeners](GodotViews.VirtualGrid.ElementTweeners.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="GodotViews_VirtualGrid_ElementTweeners_None"></a> None

This element tweener does not perform any form of visual transitions,
it is also the most efficient and snappy choice if the absolute performance is required.

```csharp
public static readonly IElementTweener None
```

#### Field Value

 [IElementTweener](GodotViews.VirtualGrid.IElementTweener.md)

## Methods

### <a id="GodotViews_VirtualGrid_ElementTweeners_CreatePan_System_Single_System_Nullable_GodotViews_VirtualGrid_TweenSetup__"></a> CreatePan\(float, TweenSetup?\)

Create an element tweener that does position interpolation based on the provided arguments.

```csharp
public static IGodotTweenTweener CreatePan(float duration, TweenSetup? tweenSetup = null)
```

#### Parameters

`duration` [float](https://learn.microsoft.com/dotnet/api/system.single)

The duration this tweener takes to finish moving a virtual grid element to the target position.

`tweenSetup` [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)?

The <xref href="GodotViews.VirtualGrid.TweenSetup" data-throw-if-not-resolved="false"></xref> tweener uses when doing the interpolation.

#### Returns

 [IGodotTweenTweener](GodotViews.VirtualGrid.IGodotTweenTweener.md)

The instance of the created element tweener that
    can be passed to the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
    for handling the virtual grid element positional interpolation.

