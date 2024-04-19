# <a id="GodotViews_VirtualGrid_SearchDirection"></a> Struct SearchDirection

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Represents the search direction passed to the <xref href="GodotViews.VirtualGrid.FocusFinders" data-throw-if-not-resolved="false"></xref>.

```csharp
public readonly struct SearchDirection
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="GodotViews_VirtualGrid_SearchDirection__ctor_Godot_Vector2I___"></a> SearchDirection\(Vector2I\[\]\)

Construct an instance of the <xref href="GodotViews.VirtualGrid.SearchDirection" data-throw-if-not-resolved="false"></xref>

```csharp
public SearchDirection(Vector2I[] backing)
```

#### Parameters

`backing` Vector2I\[\]

An array of directions the focus finder is meant to query through.

#### Remarks

Developers are highly recommended to only use the predefined contents from the <xref href="GodotViews.VirtualGrid.SearchDirections" data-throw-if-not-resolved="false"></xref> class, that is:
    <ul><li><xref href="GodotViews.VirtualGrid.SearchDirections.SearchUp" data-throw-if-not-resolved="false"></xref></li><li><xref href="GodotViews.VirtualGrid.SearchDirections.SearchDown" data-throw-if-not-resolved="false"></xref></li><li><xref href="GodotViews.VirtualGrid.SearchDirections.SearchLeft" data-throw-if-not-resolved="false"></xref></li><li><xref href="GodotViews.VirtualGrid.SearchDirections.SearchRight" data-throw-if-not-resolved="false"></xref></li></ul>

