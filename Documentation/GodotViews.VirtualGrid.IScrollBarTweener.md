# <a id="GodotViews_VirtualGrid_IScrollBarTweener"></a> Interface IScrollBarTweener

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

ScrollBar Tweener is responsible for managing the <xref href="Godot.Range.Value" data-throw-if-not-resolved="false"></xref> and <xref href="Godot.Range.Page" data-throw-if-not-resolved="false"></xref>
value interpolation of the <xref href="Godot.ScrollBar" data-throw-if-not-resolved="false"></xref> when user moves the virtualized viewport.<br />
You may access a set of built-in element tweeners from the <xref href="GodotViews.VirtualGrid.ScrollBarTweeners" data-throw-if-not-resolved="false"></xref> class.

```csharp
public interface IScrollBarTweener
```

## Methods

### <a id="GodotViews_VirtualGrid_IScrollBarTweener_KillTween_Godot_ScrollBar_"></a> KillTween\(ScrollBar\)

Invoked when the view controller is about to perform a viewport moving action.<br />
The developer should interrupt and clean up the existing tweens for the given scrollBar.

```csharp
void KillTween(ScrollBar scrollBar)
```

#### Parameters

`scrollBar` ScrollBar

The <xref href="Godot.ScrollBar" data-throw-if-not-resolved="false"></xref> that should have its associated tween interrupted.

### <a id="GodotViews_VirtualGrid_IScrollBarTweener_UpdateValue_Godot_ScrollBar_System_Single_System_Single_"></a> UpdateValue\(ScrollBar, float, float\)

Invoked when the view controller is updating the values of a <xref href="Godot.ScrollBar" data-throw-if-not-resolved="false"></xref>.

```csharp
void UpdateValue(ScrollBar scrollBar, float targetValue, float targetPage)
```

#### Parameters

`scrollBar` ScrollBar

The scroll bar to have its value updated.

`targetValue` [float](https://learn.microsoft.com/dotnet/api/system.single)

The target <xref href="Godot.Range.Value" data-throw-if-not-resolved="false"></xref> value.

`targetPage` [float](https://learn.microsoft.com/dotnet/api/system.single)

The target <xref href="Godot.Range.Page" data-throw-if-not-resolved="false"></xref> value.

