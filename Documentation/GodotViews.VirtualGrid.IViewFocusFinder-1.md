# <a id="GodotViews_VirtualGrid_IViewFocusFinder_1"></a> Interface IViewFocusFinder<TArgument\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

View Focus Finder is the abstraction of the focus finding algorithm that
calculates the target focus coordinate based on the elements in the current viewport.

```csharp
public interface IViewFocusFinder<TArgument>
```

#### Type Parameters

`TArgument` 

The type of the argument required by the focus finder.

## Methods

### <a id="GodotViews_VirtualGrid_IViewFocusFinder_1_TryResolveFocus_GodotViews_VirtualGrid_ReadOnlyViewArray__System_ReadOnlySpan_Godot_Vector2I___GodotViews_VirtualGrid_IViewStartHandler__0___0__System_Int32__System_Int32__"></a> TryResolveFocus\(ref readonly ReadOnlyViewArray, ref readonly ReadOnlySpan<Vector2I\>, IViewStartHandler<TArgument\>, ref readonly TArgument, out int, out int\)

Try to calculate the target focus coordinate based on the specified arguments.

```csharp
bool TryResolveFocus(ref readonly ReadOnlyViewArray currentView, ref readonly ReadOnlySpan<Vector2I> searchDirection, IViewStartHandler<TArgument> viewStartPositionHandler, ref readonly TArgument argument, out int viewRowIndex, out int viewColumnIndex)
```

#### Parameters

`currentView` [ReadOnlyViewArray](GodotViews.VirtualGrid.ReadOnlyViewArray.md)

Provides indirect access to the populated state of the current viewport.

`searchDirection` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan\-1)<Vector2I\>

The search direction from the start position.

`viewStartPositionHandler` [IViewStartHandler](GodotViews.VirtualGrid.IViewStartHandler\-1.md)<TArgument\>

The handler responsible for resolving the start position.

`argument` TArgument

The argument passes to the <code class="paramref">viewStartPositionHandler</code>

`viewRowIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The calculated target row index relative to the viewport,
    unused when the finder fails to obtain the coordinate.

`viewColumnIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The calculated target column index relative to the viewport,
    unused when the finder fails to obtain the coordinate.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the finder successfully obtain the coordinate;
otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

