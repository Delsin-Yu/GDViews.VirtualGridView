# <a id="GodotViews_VirtualGrid_InfiniteLayoutGrids"></a> Class InfiniteLayoutGrids

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Provides a built-in <xref href="GodotViews.VirtualGrid.IInfiniteLayoutGrid" data-throw-if-not-resolved="false"></xref>.

```csharp
public static class InfiniteLayoutGrids
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[InfiniteLayoutGrids](GodotViews.VirtualGrid.InfiniteLayoutGrids.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GodotViews_VirtualGrid_InfiniteLayoutGrids_CreateSimple_Godot_Vector2_System_Nullable_Godot_Vector2__"></a> CreateSimple\(Vector2, Vector2?\)

Create a simple infinite layout grid that has functions equivalent to a <xref href="Godot.GridContainer" data-throw-if-not-resolved="false"></xref>.

```csharp
public static IInfiniteLayoutGrid CreateSimple(Vector2 itemSize, Vector2? itemSeparation = null)
```

#### Parameters

`itemSize` Vector2

`itemSeparation` Vector2?

#### Returns

 [IInfiniteLayoutGrid](GodotViews.VirtualGrid.IInfiniteLayoutGrid.md)

The instance of the created dynamic grid viewer that
    can be passed to the builders of <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.

