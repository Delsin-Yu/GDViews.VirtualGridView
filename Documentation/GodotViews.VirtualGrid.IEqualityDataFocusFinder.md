# <a id="GodotViews_VirtualGrid_IEqualityDataFocusFinder"></a> Interface IEqualityDataFocusFinder

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Equality Data Focus Finder is the abstraction of the focus finding algorithm that
calculates the target focus coordinate based on matching the elements in the datasets.

```csharp
public interface IEqualityDataFocusFinder
```

## Methods

### <a id="GodotViews_VirtualGrid_IEqualityDataFocusFinder_TryResolveFocus__1___0__GodotViews_VirtualGrid_ReadOnlyDataArray___0___System_Int32__System_Int32__"></a> TryResolveFocus<TDataType\>\(ref readonly TDataType, ref readonly ReadOnlyDataArray<TDataType\>, out int, out int\)

Try to calculate the target focus coordinate based on the specified arguments.

```csharp
bool TryResolveFocus<TDataType>(ref readonly TDataType matchingArgument, ref readonly ReadOnlyDataArray<TDataType> currentView, out int dataSetRowIndex, out int dataSetColumnIndex)
```

#### Parameters

`matchingArgument` TDataType

The argument that uses for performing the equality matching.

`currentView` [ReadOnlyDataArray](GodotViews.VirtualGrid.ReadOnlyDataArray\-1.md)<TDataType\>

Provides indirect access to the content of the datasets.

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

The type for the data this handler focuses on.

