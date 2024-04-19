# <a id="GodotViews_VirtualGrid_IGodotTween"></a> Interface IGodotTween

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Representing the transition controller that's implemented
by the Godot's built-in <xref href="Godot.Tween" data-throw-if-not-resolved="false"></xref> system.

```csharp
public interface IGodotTween
```

## Properties

### <a id="GodotViews_VirtualGrid_IGodotTween_Duration"></a> Duration

The duration required to perform the interpolating.

```csharp
float Duration { get; set; }
```

#### Property Value

 [float](https://learn.microsoft.com/dotnet/api/system.single)

### <a id="GodotViews_VirtualGrid_IGodotTween_TweenSetup"></a> TweenSetup

The <xref href="GodotViews.VirtualGrid.IGodotTween.TweenSetup" data-throw-if-not-resolved="false"></xref> controller uses when doing the interpolation.

```csharp
TweenSetup TweenSetup { get; set; }
```

#### Property Value

 [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)

