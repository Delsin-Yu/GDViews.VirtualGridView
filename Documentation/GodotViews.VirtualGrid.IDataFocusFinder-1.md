# <a id="GodotViews_VirtualGrid_IDataFocusFinder_1"></a> Interface IDataFocusFinder<TArgument\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Data Focus Finder is the abstraction of the focus finding algorithm that
calculates the target focus coordinate based on the elements in the datasets.

```csharp
public interface IDataFocusFinder<TArgument>
```

#### Type Parameters

`TArgument` 

The type of the argument required by the focus finder.

## Methods

### <a id="GodotViews_VirtualGrid_IDataFocusFinder_1_TryResolveFocus__1_GodotViews_VirtualGrid_ReadOnlyDataArray___0___System_ReadOnlySpan_Godot_Vector2I___GodotViews_VirtualGrid_IDataStartHandler__0___0__System_Int32__System_Int32__"></a> TryResolveFocus<TDataType\>\(ref readonly ReadOnlyDataArray<TDataType\>, ref readonly ReadOnlySpan<Vector2I\>, IDataStartHandler<TArgument\>, ref readonly TArgument, out int, out int\)

Try to calculate the target focus coordinate based on the specified arguments.

```csharp
bool TryResolveFocus<TDataType>(ref readonly ReadOnlyDataArray<TDataType> currentView, ref readonly ReadOnlySpan<Vector2I> searchDirection, IDataStartHandler<TArgument> dataStartPositionHandler, ref readonly TArgument argument, out int dataSetRowIndex, out int dataSetColumnIndex)
```

#### Parameters

`currentView` [ReadOnlyDataArray](GodotViews.VirtualGrid.ReadOnlyDataArray\-1.md)<TDataType\>

Provides indirect access to the content of the datasets.

`searchDirection` [ReadOnlySpan](https://learn.microsoft.com/dotnet/api/system.readonlyspan\-1)<Vector2I\>

The search direction from the start position.

`dataStartPositionHandler` [IDataStartHandler](GodotViews.VirtualGrid.IDataStartHandler\-1.md)<TArgument\>

The handler responsible for resolving the start position.

`argument` TArgument

The argument passes to the <code class="paramref">dataStartPositionHandler</code>

`dataSetRowIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The calculated datasets row index,
    unused when the finder fails to obtain the coordinate.

`dataSetColumnIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The calculated datasets column index,
    unused when the finder fails to obtain the coordinate.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the finder successfully obtain the coordinate;
otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Type Parameters

`TDataType` 

