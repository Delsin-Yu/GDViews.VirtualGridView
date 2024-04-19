# <a id="GodotViews_VirtualGrid_ScrollBarTweeners"></a> Class ScrollBarTweeners

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Provides a set of built-in <xref href="GodotViews.VirtualGrid.IScrollBarTweener" data-throw-if-not-resolved="false"></xref> that should cover common UI/UX development needs.

```csharp
public static class ScrollBarTweeners
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ScrollBarTweeners](GodotViews.VirtualGrid.ScrollBarTweeners.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="GodotViews_VirtualGrid_ScrollBarTweeners_None"></a> None

This scroll bar tweener does not perform any form of visual transitions,
it is also the most efficient and snappy choice if the absolute performance is required.

```csharp
public static readonly IScrollBarTweener None
```

#### Field Value

 [IScrollBarTweener](GodotViews.VirtualGrid.IScrollBarTweener.md)

## Methods

### <a id="GodotViews_VirtualGrid_ScrollBarTweeners_CreateLerp_System_Single_System_Nullable_GodotViews_VirtualGrid_TweenSetup__"></a> CreateLerp\(float, TweenSetup?\)

Create an scroll bar tweener that does value interpolation based on the provided arguments.

```csharp
public static IGodotTweenScrollBarTweener CreateLerp(float duration, TweenSetup? tweenSetup = null)
```

#### Parameters

`duration` [float](https://learn.microsoft.com/dotnet/api/system.single)

The duration this tweener takes to finish the value interpolation.

`tweenSetup` [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)?

The <xref href="GodotViews.VirtualGrid.TweenSetup" data-throw-if-not-resolved="false"></xref> tweener uses when doing the interpolation.

#### Returns

 [IGodotTweenScrollBarTweener](GodotViews.VirtualGrid.IGodotTweenScrollBarTweener.md)

The instance of the created scroll bar tweener that
    can be passed to the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
    for handling the scroll bar value interpolation.

