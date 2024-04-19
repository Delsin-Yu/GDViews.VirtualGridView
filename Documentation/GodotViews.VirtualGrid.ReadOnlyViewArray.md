# <a id="GodotViews_VirtualGrid_ReadOnlyViewArray"></a> Struct ReadOnlyViewArray

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Allow the developer to indirectly access the populate state of the current viewport.

```csharp
public readonly struct ReadOnlyViewArray
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="GodotViews_VirtualGrid_ReadOnlyViewArray_ViewColumns"></a> ViewColumns

The total defined columns of the viewport.

```csharp
public readonly int ViewColumns
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GodotViews_VirtualGrid_ReadOnlyViewArray_ViewRows"></a> ViewRows

The total defined rows of the viewport.

```csharp
public readonly int ViewRows
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Properties

### <a id="GodotViews_VirtualGrid_ReadOnlyViewArray_Item_System_Int32_System_Int32_"></a> this\[int, int\]

Check if the cell at the given position has populated.

```csharp
public bool this[int columnIndex, int rowIndex] { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

