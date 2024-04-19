# <a id="GodotViews_VirtualGrid_DataFocusFinderPreset"></a> Struct DataFocusFinderPreset

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

A focus finder preset contains a set of predefined combinations 
of arguments to simplify the arguments required to pass into
the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for the developers.

```csharp
public record struct DataFocusFinderPreset : IEquatable<DataFocusFinderPreset>
```

#### Implements

[IEquatable<DataFocusFinderPreset\>](https://learn.microsoft.com/dotnet/api/system.iequatable\-1)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GodotViews_VirtualGrid_DataFocusFinderPreset__ctor_GodotViews_VirtualGrid_DataFocusFinderPreset_Godot_Vector2I__Godot_Vector2I_GodotViews_VirtualGrid_SearchDirection_"></a> DataFocusFinderPreset\(DataFocusFinderPreset<Vector2I\>, Vector2I, SearchDirection\)

A focus finder preset contains a set of predefined combinations 
of arguments to simplify the arguments required to pass into
the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for the developers.

```csharp
public DataFocusFinderPreset(DataFocusFinderPreset<Vector2I> Preset, Vector2I Argument, SearchDirection SearchDirection)
```

#### Parameters

`Preset` [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset\-1.md)<Vector2I\>

`Argument` Vector2I

`SearchDirection` [SearchDirection](GodotViews.VirtualGrid.SearchDirection.md)

## Properties

### <a id="GodotViews_VirtualGrid_DataFocusFinderPreset_Argument"></a> Argument

```csharp
public Vector2I Argument { readonly get; set; }
```

#### Property Value

 Vector2I

### <a id="GodotViews_VirtualGrid_DataFocusFinderPreset_Preset"></a> Preset

```csharp
public DataFocusFinderPreset<Vector2I> Preset { readonly get; set; }
```

#### Property Value

 [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset\-1.md)<Vector2I\>

### <a id="GodotViews_VirtualGrid_DataFocusFinderPreset_SearchDirection"></a> SearchDirection

```csharp
public SearchDirection SearchDirection { readonly get; set; }
```

#### Property Value

 [SearchDirection](GodotViews.VirtualGrid.SearchDirection.md)

