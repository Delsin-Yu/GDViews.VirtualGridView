# <a id="GodotViews_VirtualGrid_ReadOnlyDataArray_1"></a> Struct ReadOnlyDataArray<TDataType\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Allow the developer to indirectly access the content of the datasets.

```csharp
public readonly struct ReadOnlyDataArray<TDataType>
```

#### Type Parameters

`TDataType` 

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="GodotViews_VirtualGrid_ReadOnlyDataArray_1_DataSetColumns"></a> DataSetColumns

The current columns of the viewport.

```csharp
public readonly int DataSetColumns
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GodotViews_VirtualGrid_ReadOnlyDataArray_1_DataSetRows"></a> DataSetRows

The current rows of the viewport.

```csharp
public readonly int DataSetRows
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Methods

### <a id="GodotViews_VirtualGrid_ReadOnlyDataArray_1_TryGetData__1_System_Func__0___0_System_Boolean____0_System_Int32__System_Int32__"></a> TryGetData<TMatchArgument\>\(Func<TDataType, TMatchArgument, bool\>, TMatchArgument, out int, out int\)

Trying to match the first element in the datasets that satisfies a specified condition.

```csharp
public bool TryGetData<TMatchArgument>(Func<TDataType, TMatchArgument, bool> matchHandler, TMatchArgument matchArgument, out int absoluteRowIndex, out int absoluteColumnIndex)
```

#### Parameters

`matchHandler` [Func](https://learn.microsoft.com/dotnet/api/system.func\-3)<TDataType, TMatchArgument, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>

A function to test each element for a condition.

`matchArgument` TMatchArgument

The argument passes to the <code class="paramref">matchHandler</code> to avoid closure allocation.

`absoluteRowIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

When this method returns, contains the row index of the matched value
    if any of the datasets element satisfies the <code class="paramref">matchHandler</code>; otherwise, -1.

`absoluteColumnIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

When this method returns, contains the column index of the matched value
    if any of the datasets element satisfies the <code class="paramref">matchHandler</code>; otherwise, -1.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if any of the datasets element satisfies the <code class="paramref">matchHandler</code>; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Type Parameters

`TMatchArgument` 

The type of the argument passes to the <code class="paramref">matchHandler</code>.

#### Remarks

This optimized method is more suitable for matching across large chunks of datasets.

### <a id="GodotViews_VirtualGrid_ReadOnlyDataArray_1_TryGetData_System_Int32_System_Int32__0__"></a> TryGetData\(int, int, out TDataType?\)

Trying to access the element at the specified datasets location based on the provided position.

```csharp
public bool TryGetData(int dataRowIndex, int dataColumnIndex, out TDataType? data)
```

#### Parameters

`dataRowIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The datasets row index.

`dataColumnIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The datasets column index.

`data` TDataType?

When this method returns, contains the value associated with the specified position
    if falls inside the datasets; otherwise, the default value for the type of the <code class="paramref">data</code> parameter.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the specified position falls inside the datasets; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Remarks

This method is less efficient than <xref href="GodotViews.VirtualGrid.ReadOnlyDataArray%601.TryGetData%60%601(System.Func%7b%600%2c%60%600%2cSystem.Boolean%7d%2c%60%600%2cSystem.Int32%40%2cSystem.Int32%40)" data-throw-if-not-resolved="false"></xref>
    when enumerating through large chunks of datasets, as doing per-cell position mapping
    takes significantly longer than the optimized version of enumeration APIs by design.

