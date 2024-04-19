# <a id="GodotViews_VirtualGrid_ElementPositioners"></a> Class ElementPositioners

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Provides a set of built-in <xref href="GodotViews.VirtualGrid.IElementPositioner" data-throw-if-not-resolved="false"></xref> that should cover common UI/UX development needs.

```csharp
public static class ElementPositioners
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[ElementPositioners](GodotViews.VirtualGrid.ElementPositioners.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="GodotViews_VirtualGrid_ElementPositioners_Centered"></a> Centered

This element positioner places the focused element in the center of the viewport.

```csharp
public static IElementPositioner Centered { get; }
```

#### Property Value

 [IElementPositioner](GodotViews.VirtualGrid.IElementPositioner.md)

### <a id="GodotViews_VirtualGrid_ElementPositioners_Side"></a> Side

This element positioner adjusts the viewport accordingly when the focused element lies outside of the viewport.

```csharp
public static IElementPositioner Side { get; }
```

#### Property Value

 [IElementPositioner](GodotViews.VirtualGrid.IElementPositioner.md)

