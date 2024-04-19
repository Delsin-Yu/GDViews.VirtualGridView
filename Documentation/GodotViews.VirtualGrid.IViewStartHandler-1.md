# <a id="GodotViews_VirtualGrid_IViewStartHandler_1"></a> Interface IViewStartHandler<TArgument\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

View Start Handler is responsible for resolving the start position from the viewport when finding focus.

```csharp
public interface IViewStartHandler<in TArgument>
```

#### Type Parameters

`TArgument` 

## Methods

### <a id="GodotViews_VirtualGrid_IViewStartHandler_1_ResolveStartPosition_GodotViews_VirtualGrid_ReadOnlyViewArray___0_"></a> ResolveStartPosition\(ref readonly ReadOnlyViewArray, TArgument\)

Resolves the start position relative to the viewport.

```csharp
Vector2I ResolveStartPosition(ref readonly ReadOnlyViewArray currentView, TArgument argument)
```

#### Parameters

`currentView` [ReadOnlyViewArray](GodotViews.VirtualGrid.ReadOnlyViewArray.md)

Provides access to the current displayed viewport items.

`argument` TArgument

An extra argument passes to the handler.

#### Returns

 Vector2I

The resolved start position, relative to the viewport.

