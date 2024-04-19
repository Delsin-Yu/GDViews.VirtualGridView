# <a id="GodotViews_VirtualGrid_ViewFocusFinderPreset_1"></a> Struct ViewFocusFinderPreset<TArgument\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

A focus finder preset contains a set of predefined combinations 
of arguments to simplify the arguments required to pass into
the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for the developers.

```csharp
public record struct ViewFocusFinderPreset<TArgument> : IEquatable<ViewFocusFinderPreset<TArgument>>
```

#### Type Parameters

`TArgument` 

#### Implements

[IEquatable<ViewFocusFinderPreset<TArgument\>\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GodotViews_VirtualGrid_ViewFocusFinderPreset_1__ctor_GodotViews_VirtualGrid_IViewFocusFinder__0__GodotViews_VirtualGrid_IViewStartHandler__0__"></a> ViewFocusFinderPreset\(IViewFocusFinder<TArgument\>, IViewStartHandler<TArgument\>\)

A focus finder preset contains a set of predefined combinations 
of arguments to simplify the arguments required to pass into
the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for the developers.

```csharp
public ViewFocusFinderPreset(IViewFocusFinder<TArgument> FocusFinder, IViewStartHandler<TArgument> StartHandler)
```

#### Parameters

`FocusFinder` [IViewFocusFinder](GodotViews.VirtualGrid.IViewFocusFinder\-1.md)<TArgument\>

`StartHandler` [IViewStartHandler](GodotViews.VirtualGrid.IViewStartHandler\-1.md)<TArgument\>

## Properties

### <a id="GodotViews_VirtualGrid_ViewFocusFinderPreset_1_FocusFinder"></a> FocusFinder

```csharp
public IViewFocusFinder<TArgument> FocusFinder { readonly get; set; }
```

#### Property Value

 [IViewFocusFinder](GodotViews.VirtualGrid.IViewFocusFinder\-1.md)<TArgument\>

### <a id="GodotViews_VirtualGrid_ViewFocusFinderPreset_1_StartHandler"></a> StartHandler

```csharp
public IViewStartHandler<TArgument> StartHandler { readonly get; set; }
```

#### Property Value

 [IViewStartHandler](GodotViews.VirtualGrid.IViewStartHandler\-1.md)<TArgument\>

