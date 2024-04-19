# <a id="GodotViews_VirtualGrid_IVerticalDataLayoutBuilder_1"></a> Interface IVerticalDataLayoutBuilder<TDataType\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

The builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.<br />
Use the <xref href="GodotViews.VirtualGrid.IVerticalDataLayoutBuilder%601.AppendColumnDataSet(GodotViews.VirtualGrid.IDynamicGridViewer%7b%600%7d%2cSystem.Int32)" data-throw-if-not-resolved="false"></xref> method to build up the vertical datasets,
and call the <xref href="GodotViews.VirtualGrid.IFinishingBuilderAccess%601.WithArgument%60%602(Godot.PackedScene%2cGodot.Control%2cGodotViews.VirtualGrid.IInfiniteLayoutGrid)" data-throw-if-not-resolved="false"></xref> method when finished building the dataset.

```csharp
public interface IVerticalDataLayoutBuilder<TDataType> : IFinishingBuilderAccess<TDataType>
```

#### Type Parameters

`TDataType` 

The type for the data the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> focuses on.

#### Implements

[IFinishingBuilderAccess<TDataType\>](GodotViews.VirtualGrid.IFinishingBuilderAccess\-1.md)

## Methods

### <a id="GodotViews_VirtualGrid_IVerticalDataLayoutBuilder_1_AppendColumnDataSet_GodotViews_VirtualGrid_IDynamicGridViewer__0__System_Int32_"></a> AppendColumnDataSet\(IDynamicGridViewer<TDataType\>, int\)

Append the specified <code class="paramref">dataSet</code> to the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.
Call the <xref href="GodotViews.VirtualGrid.IFinishingBuilderAccess%601.WithArgument%60%602(Godot.PackedScene%2cGodot.Control%2cGodotViews.VirtualGrid.IInfiniteLayoutGrid)" data-throw-if-not-resolved="false"></xref> method when finished building the dataset.

```csharp
IVerticalDataLayoutBuilder<TDataType> AppendColumnDataSet(IDynamicGridViewer<TDataType> dataSet, int repeatCount = 1)
```

#### Parameters

`dataSet` [IDynamicGridViewer](GodotViews.VirtualGrid.IDynamicGridViewer\-1.md)<TDataType\>

The <xref href="GodotViews.VirtualGrid.IDynamicGridViewer%601" data-throw-if-not-resolved="false"></xref> that gets appended to the datasets.

`repeatCount` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The columns this dataset takes, increase this value is equatable for calling this API multiple times.

#### Returns

 [IVerticalDataLayoutBuilder](GodotViews.VirtualGrid.IVerticalDataLayoutBuilder\-1.md)<TDataType\>

The same <xref href="GodotViews.VirtualGrid.IVerticalDataLayoutBuilder%601" data-throw-if-not-resolved="false"></xref> for continuing the building of the datasets.

#### Remarks

<pre><code class="lang-csharp"> [Column 0] [Column 1]
 [DataSet0] [DataSet1]
|    00    |    00    |
|    01    |    01    |
|    02    |    02    |
|    03    |    03    |
|    04    |    04    |
            ^^ +New ^^</code></pre>

