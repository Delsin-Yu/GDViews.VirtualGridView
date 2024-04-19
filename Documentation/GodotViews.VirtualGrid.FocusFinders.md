# <a id="GodotViews_VirtualGrid_FocusFinders"></a> Class FocusFinders

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Provides a set of predefined focus finders for handling the focus finding logic.

```csharp
public static class FocusFinders
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[FocusFinders](GodotViews.VirtualGrid.FocusFinders.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="GodotViews_VirtualGrid_FocusFinders_DataPosition"></a> DataPosition

Try to find a valid grid element as the focus candidate by the specified datasets position.

```csharp
public static readonly IDataFocusFinder<Vector2I> DataPosition
```

#### Field Value

 [IDataFocusFinder](GodotViews.VirtualGrid.IDataFocusFinder\-1.md)<Vector2I\>

### <a id="GodotViews_VirtualGrid_FocusFinders_Predicate"></a> Predicate

Try to find a valid grid element as the focus candidate by the specified predicate.

```csharp
public static readonly IPredicateDataFocusFinder Predicate
```

#### Field Value

 [IPredicateDataFocusFinder](GodotViews.VirtualGrid.IPredicateDataFocusFinder.md)

### <a id="GodotViews_VirtualGrid_FocusFinders_Value"></a> Value

Try to find a valid grid element as the focus candidate by the specified value equality.

```csharp
public static readonly IEqualityDataFocusFinder Value
```

#### Field Value

 [IEqualityDataFocusFinder](GodotViews.VirtualGrid.IEqualityDataFocusFinder.md)

### <a id="GodotViews_VirtualGrid_FocusFinders_ViewPosition"></a> ViewPosition

Try to find a valid grid element as the focus candidate by the specified viewport position.

```csharp
public static readonly IViewFocusFinder<Vector2I> ViewPosition
```

#### Field Value

 [IViewFocusFinder](GodotViews.VirtualGrid.IViewFocusFinder\-1.md)<Vector2I\>

