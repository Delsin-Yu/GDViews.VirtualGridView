# <a id="GodotViews_VirtualGrid_StartHandlers"></a> Class StartHandlers

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Provides a set of predefined start handlers for obtaining the start position of the focus finding logic.

```csharp
public static class StartHandlers
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[StartHandlers](GodotViews.VirtualGrid.StartHandlers.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="GodotViews_VirtualGrid_StartHandlers_DataPosition"></a> DataPosition

Gets the start position from the datasets coordinate by the specified dataset position.

```csharp
public static readonly IDataStartHandler<Vector2I> DataPosition
```

#### Field Value

 [IDataStartHandler](GodotViews.VirtualGrid.IDataStartHandler\-1.md)<Vector2I\>

### <a id="GodotViews_VirtualGrid_StartHandlers_ViewCenter"></a> ViewCenter

Get the start position from the viewport coordinate with the specified offset.

```csharp
public static readonly IViewStartHandler<Vector2I> ViewCenter
```

#### Field Value

 [IViewStartHandler](GodotViews.VirtualGrid.IViewStartHandler\-1.md)<Vector2I\>

### <a id="GodotViews_VirtualGrid_StartHandlers_ViewPosition"></a> ViewPosition

Gets the start position from the viewport coordinate by the specified viewport position.

```csharp
public static readonly IViewStartHandler<Vector2I> ViewPosition
```

#### Field Value

 [IViewStartHandler](GodotViews.VirtualGrid.IViewStartHandler\-1.md)<Vector2I\>

