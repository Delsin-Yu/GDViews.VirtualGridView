# <a id="GodotViews_VirtualGrid_FocusPresets"></a> Class FocusPresets

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Provides a set of predefined focus presets that cover common grab focus requirements.

```csharp
public static class FocusPresets
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[FocusPresets](GodotViews.VirtualGrid.FocusPresets.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Fields

### <a id="GodotViews_VirtualGrid_FocusPresets_BottomLeftData"></a> BottomLeftData

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available data
starting from the <xref href="GodotViews.VirtualGrid.Corners.BottomLeft" data-throw-if-not-resolved="false"></xref> corner of the datasets towards the <xref href="GodotViews.VirtualGrid.SearchDirections.RightUp" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly DataFocusFinderPreset BottomLeftData
```

#### Field Value

 [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_BottomLeftView"></a> BottomLeftView

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available virtualized grid element 
starting from the <xref href="GodotViews.VirtualGrid.Corners.BottomLeft" data-throw-if-not-resolved="false"></xref> corner of the viewport towards the <xref href="GodotViews.VirtualGrid.SearchDirections.RightUp" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly ViewFocusFinderPreset BottomLeftView
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_BottomRightData"></a> BottomRightData

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available data
starting from the <xref href="GodotViews.VirtualGrid.Corners.BottomRight" data-throw-if-not-resolved="false"></xref> corner of the datasets towards the <xref href="GodotViews.VirtualGrid.SearchDirections.LeftUp" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly DataFocusFinderPreset BottomRightData
```

#### Field Value

 [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_BottomRightView"></a> BottomRightView

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available virtualized grid element 
starting from the <xref href="GodotViews.VirtualGrid.Corners.BottomRight" data-throw-if-not-resolved="false"></xref> corner of the viewport towards the <xref href="GodotViews.VirtualGrid.SearchDirections.LeftUp" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly ViewFocusFinderPreset BottomRightView
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_CenterAnticlockwiseView"></a> CenterAnticlockwiseView

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available virtualized grid element 
starting from the center of the viewport towards the <xref href="GodotViews.VirtualGrid.SearchDirections.FourWayAnticlockwise" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly ViewFocusFinderPreset CenterAnticlockwiseView
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_CenterClockwiseView"></a> CenterClockwiseView

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available virtualized grid element 
starting from the center of the viewport towards the <xref href="GodotViews.VirtualGrid.SearchDirections.FourWayClockwise" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly ViewFocusFinderPreset CenterClockwiseView
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_CenterUpDownLeftRightView"></a> CenterUpDownLeftRightView

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available virtualized grid element 
starting from the center of the viewport towards the <xref href="GodotViews.VirtualGrid.SearchDirections.UpDownLeftRight" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly ViewFocusFinderPreset CenterUpDownLeftRightView
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_DataPosition"></a> DataPosition

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus by <xref href="GodotViews.VirtualGrid.FocusFinders.DataPosition" data-throw-if-not-resolved="false"></xref>,
the developer need to specify the start <xref href="GodotViews.VirtualGrid.Corners" data-throw-if-not-resolved="false"></xref> and <xref href="GodotViews.VirtualGrid.SearchDirections" data-throw-if-not-resolved="false"></xref> in addition.

```csharp
public static readonly DataFocusFinderPreset<Vector2I> DataPosition
```

#### Field Value

 [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset\-1.md)<Vector2I\>

### <a id="GodotViews_VirtualGrid_FocusPresets_LeftBottomData"></a> LeftBottomData

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available data
starting from the <xref href="GodotViews.VirtualGrid.Corners.BottomLeft" data-throw-if-not-resolved="false"></xref> corner of the datasets towards the <xref href="GodotViews.VirtualGrid.SearchDirections.UpRight" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly DataFocusFinderPreset LeftBottomData
```

#### Field Value

 [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_LeftBottomView"></a> LeftBottomView

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available virtualized grid element 
starting from the <xref href="GodotViews.VirtualGrid.Corners.BottomLeft" data-throw-if-not-resolved="false"></xref> corner of the viewport towards the <xref href="GodotViews.VirtualGrid.SearchDirections.UpRight" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly ViewFocusFinderPreset LeftBottomView
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_LeftTopData"></a> LeftTopData

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available data
starting from the <xref href="GodotViews.VirtualGrid.Corners.TopLeft" data-throw-if-not-resolved="false"></xref> corner of the datasets towards the <xref href="GodotViews.VirtualGrid.SearchDirections.DownRight" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly DataFocusFinderPreset LeftTopData
```

#### Field Value

 [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_LeftTopView"></a> LeftTopView

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available virtualized grid element 
starting from the <xref href="GodotViews.VirtualGrid.Corners.TopLeft" data-throw-if-not-resolved="false"></xref> corner of the viewport towards the <xref href="GodotViews.VirtualGrid.SearchDirections.DownRight" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly ViewFocusFinderPreset LeftTopView
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_Predicate"></a> Predicate

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus by <xref href="GodotViews.VirtualGrid.FocusFinders.Predicate" data-throw-if-not-resolved="false"></xref>,
the developer need to provide a predicate for matching against each data from the datasets.

```csharp
public static readonly IPredicateDataFocusFinder Predicate
```

#### Field Value

 [IPredicateDataFocusFinder](GodotViews.VirtualGrid.IPredicateDataFocusFinder.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_RightBottomData"></a> RightBottomData

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available data
starting from the <xref href="GodotViews.VirtualGrid.Corners.BottomRight" data-throw-if-not-resolved="false"></xref> corner of the datasets towards the <xref href="GodotViews.VirtualGrid.SearchDirections.UpLeft" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly DataFocusFinderPreset RightBottomData
```

#### Field Value

 [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_RightBottomView"></a> RightBottomView

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available virtualized grid element 
starting from the <xref href="GodotViews.VirtualGrid.Corners.BottomRight" data-throw-if-not-resolved="false"></xref> corner of the viewport towards the <xref href="GodotViews.VirtualGrid.SearchDirections.UpLeft" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly ViewFocusFinderPreset RightBottomView
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_RightTopData"></a> RightTopData

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available data
starting from the <xref href="GodotViews.VirtualGrid.Corners.TopRight" data-throw-if-not-resolved="false"></xref> corner of the datasets towards the <xref href="GodotViews.VirtualGrid.SearchDirections.DownLeft" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly DataFocusFinderPreset RightTopData
```

#### Field Value

 [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_RightTopView"></a> RightTopView

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available virtualized grid element 
starting from the <xref href="GodotViews.VirtualGrid.Corners.TopRight" data-throw-if-not-resolved="false"></xref> corner of the viewport towards the <xref href="GodotViews.VirtualGrid.SearchDirections.DownLeft" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly ViewFocusFinderPreset RightTopView
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_TopLeftData"></a> TopLeftData

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available data
starting from the <xref href="GodotViews.VirtualGrid.Corners.TopLeft" data-throw-if-not-resolved="false"></xref> corner of the datasets towards the <xref href="GodotViews.VirtualGrid.SearchDirections.RightDown" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly DataFocusFinderPreset TopLeftData
```

#### Field Value

 [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_TopLeftView"></a> TopLeftView

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available virtualized grid element 
starting from the <xref href="GodotViews.VirtualGrid.Corners.TopLeft" data-throw-if-not-resolved="false"></xref> corner of the viewport towards the <xref href="GodotViews.VirtualGrid.SearchDirections.RightDown" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly ViewFocusFinderPreset TopLeftView
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_TopRightData"></a> TopRightData

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available data
starting from the <xref href="GodotViews.VirtualGrid.Corners.TopRight" data-throw-if-not-resolved="false"></xref> corner of the datasets towards the <xref href="GodotViews.VirtualGrid.SearchDirections.LeftDown" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly DataFocusFinderPreset TopRightData
```

#### Field Value

 [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_TopRightView"></a> TopRightView

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus on the first available virtualized grid element 
starting from the <xref href="GodotViews.VirtualGrid.Corners.TopRight" data-throw-if-not-resolved="false"></xref> corner of the viewport towards the <xref href="GodotViews.VirtualGrid.SearchDirections.LeftDown" data-throw-if-not-resolved="false"></xref> search direction.

```csharp
public static readonly ViewFocusFinderPreset TopRightView
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_Value"></a> Value

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus by <xref href="GodotViews.VirtualGrid.FocusFinders.Value" data-throw-if-not-resolved="false"></xref>,
the developer need to provide a value for matching against each data from the datasets.

```csharp
public static readonly IEqualityDataFocusFinder Value
```

#### Field Value

 [IEqualityDataFocusFinder](GodotViews.VirtualGrid.IEqualityDataFocusFinder.md)

### <a id="GodotViews_VirtualGrid_FocusPresets_ViewCenter"></a> ViewCenter

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus by <xref href="GodotViews.VirtualGrid.StartHandlers.ViewCenter" data-throw-if-not-resolved="false"></xref>,
the developer need to specify the offset and <xref href="GodotViews.VirtualGrid.SearchDirections" data-throw-if-not-resolved="false"></xref> in addition.

```csharp
public static readonly ViewFocusFinderPreset<Vector2I> ViewCenter
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset\-1.md)<Vector2I\>

### <a id="GodotViews_VirtualGrid_FocusPresets_ViewPosition"></a> ViewPosition

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to grab focus by <xref href="GodotViews.VirtualGrid.FocusFinders.ViewPosition" data-throw-if-not-resolved="false"></xref>,
the developer need to specify the start <xref href="GodotViews.VirtualGrid.Corners" data-throw-if-not-resolved="false"></xref> and <xref href="GodotViews.VirtualGrid.SearchDirections" data-throw-if-not-resolved="false"></xref> in addition.

```csharp
public static readonly ViewFocusFinderPreset<Vector2I> ViewPosition
```

#### Field Value

 [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset\-1.md)<Vector2I\>

