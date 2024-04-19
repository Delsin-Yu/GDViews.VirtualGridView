# <a id="GodotViews_VirtualGrid_VirtualGridViewItem_2_CellInfo"></a> Struct VirtualGridViewItem<TDataType, TExtraArgument\>.CellInfo

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Stores the associated info assigned to the current virtualized grid element.

```csharp
public readonly struct VirtualGridViewItem<TDataType, TExtraArgument>.CellInfo
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_2_CellInfo_ColumnIndex"></a> ColumnIndex

The viewport column index this virtualized grid element belongs to.

```csharp
public readonly int ColumnIndex
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_2_CellInfo_Data"></a> Data

The associated data for this virtualized grid element.

```csharp
public readonly TDataType? Data
```

#### Field Value

 TDataType?

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_2_CellInfo_DataSetEdgeType"></a> DataSetEdgeType

The edge of the dataset this virtualized grid element belongs to.

```csharp
public readonly EdgeType DataSetEdgeType
```

#### Field Value

 [EdgeType](GodotViews.VirtualGrid.EdgeType.md)

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_2_CellInfo_DefinedViewEdgeType"></a> DefinedViewEdgeType

The edge of the defined viewport this virtualized grid element belongs to.

```csharp
public readonly EdgeType DefinedViewEdgeType
```

#### Field Value

 [EdgeType](GodotViews.VirtualGrid.EdgeType.md)

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_2_CellInfo_RowIndex"></a> RowIndex

The viewport row index this virtualized grid element belongs to.

```csharp
public readonly int RowIndex
```

#### Field Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_2_CellInfo_ViewEdgeType"></a> ViewEdgeType

The edge of the current displayed viewport this virtualized grid element belongs to.

```csharp
public readonly EdgeType ViewEdgeType
```

#### Field Value

 [EdgeType](GodotViews.VirtualGrid.EdgeType.md)

## Properties

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_2_CellInfo_ExtraArgument"></a> ExtraArgument

The extra argument associated to this virtualized grid element.

```csharp
public TExtraArgument? ExtraArgument { get; }
```

#### Property Value

 TExtraArgument?

## Methods

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_2_CellInfo_ToString"></a> ToString\(\)

Returns the string representation for this <xref href="GodotViews.VirtualGrid.VirtualGridViewItem%602.CellInfo" data-throw-if-not-resolved="false"></xref>.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

The string representation for this <xref href="GodotViews.VirtualGrid.VirtualGridViewItem%602.CellInfo" data-throw-if-not-resolved="false"></xref>.

