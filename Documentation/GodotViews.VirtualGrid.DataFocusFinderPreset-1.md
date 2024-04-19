# <a id="GodotViews_VirtualGrid_DataFocusFinderPreset_1"></a> Struct DataFocusFinderPreset<TArgument\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

A focus finder preset contains a set of predefined combinations 
of arguments to simplify the arguments required to pass into
the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for the developers.

```csharp
public record struct DataFocusFinderPreset<TArgument> : IEquatable<DataFocusFinderPreset<TArgument>>
```

#### Type Parameters

`TArgument` 

#### Implements

[IEquatable<DataFocusFinderPreset<TArgument\>\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GodotViews_VirtualGrid_DataFocusFinderPreset_1__ctor_GodotViews_VirtualGrid_IDataFocusFinder__0__GodotViews_VirtualGrid_IDataStartHandler__0__"></a> DataFocusFinderPreset\(IDataFocusFinder<TArgument\>, IDataStartHandler<TArgument\>\)

A focus finder preset contains a set of predefined combinations 
of arguments to simplify the arguments required to pass into
the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for the developers.

```csharp
public DataFocusFinderPreset(IDataFocusFinder<TArgument> FocusFinder, IDataStartHandler<TArgument> StartHandler)
```

#### Parameters

`FocusFinder` [IDataFocusFinder](GodotViews.VirtualGrid.IDataFocusFinder\-1.md)<TArgument\>

`StartHandler` [IDataStartHandler](GodotViews.VirtualGrid.IDataStartHandler\-1.md)<TArgument\>

## Properties

### <a id="GodotViews_VirtualGrid_DataFocusFinderPreset_1_FocusFinder"></a> FocusFinder

```csharp
public IDataFocusFinder<TArgument> FocusFinder { readonly get; set; }
```

#### Property Value

 [IDataFocusFinder](GodotViews.VirtualGrid.IDataFocusFinder\-1.md)<TArgument\>

### <a id="GodotViews_VirtualGrid_DataFocusFinderPreset_1_StartHandler"></a> StartHandler

```csharp
public IDataStartHandler<TArgument> StartHandler { readonly get; set; }
```

#### Property Value

 [IDataStartHandler](GodotViews.VirtualGrid.IDataStartHandler\-1.md)<TArgument\>

