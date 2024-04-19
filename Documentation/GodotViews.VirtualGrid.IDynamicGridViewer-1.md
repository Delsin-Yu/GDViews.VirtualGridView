# <a id="GodotViews_VirtualGrid_IDynamicGridViewer_1"></a> Interface IDynamicGridViewer<T\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Dynamic Grid Viewer is the abstraction of a 2D list with one fixed dimension,
it can also be used for emulating a 2D view from a regular list.<br />
You may create an instance of the dynamic grid viewer from the <xref href="GodotViews.VirtualGrid.DynamicGridViewers" data-throw-if-not-resolved="false"></xref> class.

```csharp
public interface IDynamicGridViewer<T>
```

#### Type Parameters

`T` 

The type of the 2D list.

## Properties

### <a id="GodotViews_VirtualGrid_IDynamicGridViewer_1_FixedMetric"></a> FixedMetric

The fixed metric of the 2D list, that is,
this value stays the same as the total length of the list changes.

```csharp
int FixedMetric { set; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Methods

### <a id="GodotViews_VirtualGrid_IDynamicGridViewer_1_GetDynamicMetric"></a> GetDynamicMetric\(\)

Obtains the dynamic metric of the 2D list, that is,
the value that changes as the total length of the list changes.

```csharp
int GetDynamicMetric()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

The calculated dynamic metric.

### <a id="GodotViews_VirtualGrid_IDynamicGridViewer_1_TryGetGridElement_System_Int32_System_Int32__0__"></a> TryGetGridElement\(int, int, out T?\)

Trying to access the element at the specified location base on the provided metric indexes.

```csharp
bool TryGetGridElement(int fixedMetricIndex, int dynamicMetricIndex, out T? element)
```

#### Parameters

`fixedMetricIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The fixed metric index.

`dynamicMetricIndex` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The dynamic metric index

`element` T?

When this method returns, contains the value associated with the specified metric indexes
    if falls inside the 2D list; otherwise, the default value for the type of the <code class="paramref">element</code> parameter.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if the specified metric indexes falls inside the 2D list; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

