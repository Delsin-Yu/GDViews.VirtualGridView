# <a id="GodotViews_VirtualGrid_IHorizontalDataLayoutBuilder_1"></a> Interface IHorizontalDataLayoutBuilder<TDataType\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

The builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.<br />
Use the <xref href="GodotViews.VirtualGrid.IHorizontalDataLayoutBuilder%601.AppendRowDataSet(GodotViews.VirtualGrid.IDynamicGridViewer%7b%600%7d%2cSystem.Int32)" data-throw-if-not-resolved="false"></xref> method to build up the horizontal datasets,
and call the <xref href="GodotViews.VirtualGrid.IFinishingBuilderAccess%601.WithArgument%60%602(Godot.PackedScene%2cGodot.Control%2cGodotViews.VirtualGrid.IInfiniteLayoutGrid)" data-throw-if-not-resolved="false"></xref> method when finished building the dataset.

```csharp
public interface IHorizontalDataLayoutBuilder<TDataType> : IFinishingBuilderAccess<TDataType>
```

#### Type Parameters

`TDataType` 

The type for the data the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> focuses on.

#### Implements

[IFinishingBuilderAccess<TDataType\>](GodotViews.VirtualGrid.IFinishingBuilderAccess\-1.md)

## Methods

### <a id="GodotViews_VirtualGrid_IHorizontalDataLayoutBuilder_1_AppendRowDataSet_GodotViews_VirtualGrid_IDynamicGridViewer__0__System_Int32_"></a> AppendRowDataSet\(IDynamicGridViewer<TDataType\>, int\)

Append the specified <code class="paramref">dataSet</code> to the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.
Call the <xref href="GodotViews.VirtualGrid.IFinishingBuilderAccess%601.WithArgument%60%602(Godot.PackedScene%2cGodot.Control%2cGodotViews.VirtualGrid.IInfiniteLayoutGrid)" data-throw-if-not-resolved="false"></xref> method when finished building the dataset.

```csharp
IHorizontalDataLayoutBuilder<TDataType> AppendRowDataSet(IDynamicGridViewer<TDataType> dataSet, int repeatCount = 1)
```

#### Parameters

`dataSet` [IDynamicGridViewer](GodotViews.VirtualGrid.IDynamicGridViewer\-1.md)<TDataType\>

The <xref href="GodotViews.VirtualGrid.IDynamicGridViewer%601" data-throw-if-not-resolved="false"></xref> that gets appended to the datasets.

`repeatCount` [int](https://learn.microsoft.com/dotnet/api/system.int32)

The rows this dataset takes, increase this value is equatable for calling this API multiple times.

#### Returns

 [IHorizontalDataLayoutBuilder](GodotViews.VirtualGrid.IHorizontalDataLayoutBuilder\-1.md)<TDataType\>

The same <xref href="GodotViews.VirtualGrid.IHorizontalDataLayoutBuilder%601" data-throw-if-not-resolved="false"></xref> for continuing the building of the datasets.

#### Remarks

<pre><code class="lang-csharp">[Row 0] [DataSet0: 00, 02, 04, 06, 08]
[Row 1] [DataSet1: 00, 02, 04, 06, 08] &lt;---- +New</code></pre>

