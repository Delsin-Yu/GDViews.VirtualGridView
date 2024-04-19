# <a id="GodotViews_VirtualGrid_VirtualGridView"></a> Class VirtualGridView

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Use the <xref href="GodotViews.VirtualGrid.VirtualGridView.Create(System.Int32%2cSystem.Int32)" data-throw-if-not-resolved="false"></xref> method to initiate a build process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.

```csharp
public static class VirtualGridView
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[VirtualGridView](GodotViews.VirtualGrid.VirtualGridView.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GodotViews_VirtualGrid_VirtualGridView_Create_System_Int32_System_Int32_"></a> Create\(int, int\)

Initiate a build process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance
by setting up the viewport metrics, or the amount of elements displayed concurrently by the control.

```csharp
public static IViewHandlerBuilder Create(int viewportColumns, int viewportRows)
```

#### Parameters

`viewportColumns` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of columns for the concurrently displayed virtualized grid items.

`viewportRows` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The number of rows for the concurrently displayed virtualized grid items.

#### Returns

 [IViewHandlerBuilder](GodotViews.VirtualGrid.IViewHandlerBuilder.md)

A builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.

