# <a id="GodotViews_VirtualGrid_IPredicateDataFocusFinder"></a> Interface IPredicateDataFocusFinder

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Predicate Data Focus Finder is the abstraction of the focus finding algorithm that
calculates the target focus coordinate based on the matching the elements
by the custom predicate in the datasets.

```csharp
public interface IPredicateDataFocusFinder
```

## Methods

### <a id="GodotViews_VirtualGrid_IPredicateDataFocusFinder_TryResolveFocus__1_System_Predicate___0___GodotViews_VirtualGrid_ReadOnlyDataArray___0___System_Int32__System_Int32__"></a> TryResolveFocus<TDataType\>\(ref readonly Predicate<TDataType\>, ref readonly ReadOnlyDataArray<TDataType\>, out int, out int\)

<summary>Try to calculate the target focus coordinate based on the specified arguments.</summary>

```csharp
bool TryResolveFocus<TDataType>(ref readonly Predicate<TDataType> predicate, ref readonly ReadOnlyDataArray<TDataType> currentView, out int dataSetRowIndex, out int dataSetColumnIndex)
```

#### Parameters

`predicate` [Predicate](https://learn.microsoft.com/dotnet/api/system.predicate\-1)<TDataType\>

The predicate passes that uses for performing the matching.

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

### <a id="GodotViews_VirtualGrid_IPredicateDataFocusFinder_TryResolveFocus__2_System_Func___0___1_System_Boolean___GodotViews_VirtualGrid_ReadOnlyDataArray___0_____1__System_Int32__System_Int32__"></a> TryResolveFocus<TDataType, TExtraArgument\>\(ref readonly Func<TDataType, TExtraArgument, bool\>, ref readonly ReadOnlyDataArray<TDataType\>, in TExtraArgument, out int, out int\)

<summary>Try to calculate the target focus coordinate based on the specified arguments.</summary>

```csharp
bool TryResolveFocus<TDataType, TExtraArgument>(ref readonly Func<TDataType, TExtraArgument, bool> predicate, ref readonly ReadOnlyDataArray<TDataType> currentView, in TExtraArgument extraArgument, out int dataSetRowIndex, out int dataSetColumnIndex)
```

#### Parameters

`predicate` [Func](https://learn.microsoft.com/dotnet/api/system.func\-3)<TDataType, TExtraArgument, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>

The predicate passes that uses for performing the matching.

`currentView` [ReadOnlyDataArray](GodotViews.VirtualGrid.ReadOnlyDataArray\-1.md)<TDataType\>

Provides indirect access to the content of the datasets.

`extraArgument` TExtraArgument

The predicate passes to the <code class="paramref">predicate</code> to avoid closure allocation.

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

`TExtraArgument` 

The type of the argument required by the focus finder.

