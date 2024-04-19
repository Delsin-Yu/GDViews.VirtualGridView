# <a id="GodotViews_VirtualGrid_DynamicGridViewers"></a> Class DynamicGridViewers

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Provides a built-in <xref href="GodotViews.VirtualGrid.IDynamicGridViewer%601" data-throw-if-not-resolved="false"></xref>.

```csharp
public static class DynamicGridViewers
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[DynamicGridViewers](GodotViews.VirtualGrid.DynamicGridViewers.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GodotViews_VirtualGrid_DynamicGridViewers_CreateList__1_System_Collections_Generic_IReadOnlyList___0__"></a> CreateList<T\>\(IReadOnlyList<T\>\)

Create an dynamic grid viewer that emulates a 2D list view from provided regular list.

```csharp
public static IDynamicGridViewer<T> CreateList<T>(IReadOnlyList<T> list)
```

#### Parameters

`list` [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<T\>

The backing list to emulate from.

#### Returns

 [IDynamicGridViewer](GodotViews.VirtualGrid.IDynamicGridViewer\-1.md)<T\>

The instance of the created dynamic grid viewer that
    can be passed to the builders of <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
    for constructing the datasets.

#### Type Parameters

`T` 

The type of elements in the list

