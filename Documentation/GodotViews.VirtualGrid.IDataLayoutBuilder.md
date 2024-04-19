# <a id="GodotViews_VirtualGrid_IDataLayoutBuilder"></a> Interface IDataLayoutBuilder

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

The builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.<br />
Use the <xref href="GodotViews.VirtualGrid.IDataLayoutBuilder.WithHorizontalDataLayout%60%601(System.Collections.Generic.IEqualityComparer%7b%60%600%7d%2cSystem.Boolean)" data-throw-if-not-resolved="false"></xref> or the <xref href="GodotViews.VirtualGrid.IDataLayoutBuilder.WithVerticalDataLayout%60%601(System.Collections.Generic.IEqualityComparer%7b%60%600%7d%2cSystem.Boolean)" data-throw-if-not-resolved="false"></xref> method to choose between the layout of the data sets.

```csharp
public interface IDataLayoutBuilder
```

## Methods

### <a id="GodotViews_VirtualGrid_IDataLayoutBuilder_WithHorizontalDataLayout__1_System_Collections_Generic_IEqualityComparer___0__System_Boolean_"></a> WithHorizontalDataLayout<TDataType\>\(IEqualityComparer<TDataType\>?, bool\)

Instruct the view controller to layout the datasets horizontally.

```csharp
IHorizontalDataLayoutBuilder<TDataType> WithHorizontalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false)
```

#### Parameters

`equalityComparer` [IEqualityComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.iequalitycomparer\-1)<TDataType\>?

The <xref href="System.Collections.Generic.IEqualityComparer%601" data-throw-if-not-resolved="false"></xref> used to
    determine if the data associated to certain grid element has changed,
    setting to null will fallback to the <xref href="System.Collections.Generic.EqualityComparer%601.Default" data-throw-if-not-resolved="false"></xref>

`reverseLocalLayout` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When set to true, the view controller will reverse the layout of the provided datasets.

#### Returns

 [IHorizontalDataLayoutBuilder](GodotViews.VirtualGrid.IHorizontalDataLayoutBuilder\-1.md)<TDataType\>

A builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.

#### Type Parameters

`TDataType` 

The type for the data the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> focuses on.

#### Remarks

<pre><code class="lang-csharp">Preview of the horizontal data layout, each data set is allowed to occupy more than one row:
  [Row 0] [DataSet0: 00, 02, 04, 06, 08]
  [Row 1] [DataSet0: 01, 03, 05, 07, 09]
  [Row 2] [DataSet1: 00, 02, 04, 06, 08]
  [Row 3] [DataSet1: 01, 03, 05, 07, 09]
  [Row 4] [DataSet2: 00, 01, 02, 03, 04, 05]
  [Row 5] [DataSet3: 00, 01, 02, 03, 04, 05]

When the reverseLocalLayout is set to true:
  [Row 0] [DataSet0: 01, 03, 05, 07, 09]
  [Row 1] [DataSet0: 00, 02, 04, 06, 08]
  [Row 2] [DataSet1: 01, 03, 05, 07, 09]
  [Row 3] [DataSet1: 00, 02, 04, 06, 08]
  [Row 4] [DataSet2: 00, 01, 02, 03, 04, 05]
  [Row 5] [DataSet3: 00, 01, 02, 03, 04, 05]</code></pre>

### <a id="GodotViews_VirtualGrid_IDataLayoutBuilder_WithVerticalDataLayout__1_System_Collections_Generic_IEqualityComparer___0__System_Boolean_"></a> WithVerticalDataLayout<TDataType\>\(IEqualityComparer<TDataType\>?, bool\)

Instruct the view controller to layout the datasets vertically.

```csharp
IVerticalDataLayoutBuilder<TDataType> WithVerticalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false)
```

#### Parameters

`equalityComparer` [IEqualityComparer](https://learn.microsoft.com/dotnet/api/system.collections.generic.iequalitycomparer\-1)<TDataType\>?

The <xref href="System.Collections.Generic.IEqualityComparer%601" data-throw-if-not-resolved="false"></xref> used to
    determine if the data associated to certain grid element has changed,
    setting to null will fallback to the <xref href="System.Collections.Generic.EqualityComparer%601.Default" data-throw-if-not-resolved="false"></xref>

`reverseLocalLayout` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

When set to true, the view controller will reverse the layout of the provided datasets.

#### Returns

 [IVerticalDataLayoutBuilder](GodotViews.VirtualGrid.IVerticalDataLayoutBuilder\-1.md)<TDataType\>

A builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.

#### Type Parameters

`TDataType` 

The type for the data the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> focuses on.

#### Remarks

<pre><code class="lang-csharp">Preview of the vertical data layout, each data set is allowed to occupy more than one column:
 [Column 0] [Column 1] [Column 2] [Column 3] [Column 4] [Column 5]
 [DataSet0] [DataSet0] [DataSet1] [DataSet1] [DataSet2] [DataSet3]
|    00    |    01    |    00    |    01    |    00    |    00    |
|    02    |    03    |    02    |    03    |    01    |    01    |
|    04    |    05    |    04    |    05    |    02    |    02    |
|    06    |    07    |    06    |    07    |    03    |    03    |
|    08    |    09    |    08    |    09    |    04    |    04    |

When the reverseLocalLayout is set to true:
 [Column 0] [Column 1] [Column 2] [Column 3] [Column 4] [Column 5]
 [DataSet0] [DataSet0] [DataSet1] [DataSet1] [DataSet2] [DataSet3]
|    01    |    00    |    01    |    00    |    00    |    00    |
|    03    |    02    |    03    |    02    |    01    |    01    |
|    05    |    04    |    05    |    04    |    02    |    02    |
|    07    |    06    |    07    |    06    |    03    |    03    |
|    09    |    08    |    09    |    08    |    04    |    04    |</code></pre>

