# <a id="GodotViews_VirtualGrid_IFinishingBuilderAccess_1"></a> Interface IFinishingBuilderAccess<TDataType\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

The builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.<br />
Use the <xref href="GodotViews.VirtualGrid.IFinishingBuilderAccess%601.WithArgument%60%601(Godot.PackedScene%2cGodot.Control%2cGodotViews.VirtualGrid.IInfiniteLayoutGrid)" data-throw-if-not-resolved="false"></xref> or the <xref href="GodotViews.VirtualGrid.IFinishingBuilderAccess%601.WithArgument%60%602(Godot.PackedScene%2cGodot.Control%2cGodotViews.VirtualGrid.IInfiniteLayoutGrid)" data-throw-if-not-resolved="false"></xref> method
to pass in the necessary arguments.

```csharp
public interface IFinishingBuilderAccess<TDataType>
```

#### Type Parameters

`TDataType` 

The type for the data the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> focuses on.

## Methods

### <a id="GodotViews_VirtualGrid_IFinishingBuilderAccess_1_WithArgument__2_Godot_PackedScene_Godot_Control_GodotViews_VirtualGrid_IInfiniteLayoutGrid_"></a> WithArgument<TButtonType, TExtraArgument\>\(PackedScene, Control, IInfiniteLayoutGrid\)

Pass in the necessary arguments to the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>

```csharp
IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> WithArgument<TButtonType, TExtraArgument>(PackedScene itemPrefab, Control itemContainer, IInfiniteLayoutGrid layoutGrid) where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
```

#### Parameters

`itemPrefab` PackedScene

The <xref href="Godot.PackedScene" data-throw-if-not-resolved="false"></xref> used for the virtualized grid element
    that have a script inherits <xref href="GodotViews.VirtualGrid.VirtualGridViewItem%602" data-throw-if-not-resolved="false"></xref> attached.

`itemContainer` Control

The <xref href="Godot.Control" data-throw-if-not-resolved="false"></xref> used for the container of all virtualized grid elements.

`layoutGrid` [IInfiniteLayoutGrid](GodotViews.VirtualGrid.IInfiniteLayoutGrid.md)

The <xref href="GodotViews.VirtualGrid.InfiniteLayoutGrids" data-throw-if-not-resolved="false"></xref> used to handle the layout positioning of all virtualized grid elements.

#### Returns

 [IFinishingArgumentBuilder](GodotViews.VirtualGrid.IFinishingArgumentBuilder\-3.md)<TDataType, TButtonType, TExtraArgument\>

A builder that concludes the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.

#### Type Parameters

`TButtonType` 

The type of the script attached to the <code class="paramref">itemPrefab</code>.

`TExtraArgument` 

The extra argument passed to the script attached to the virtualized grid elements.

### <a id="GodotViews_VirtualGrid_IFinishingBuilderAccess_1_WithArgument__1_Godot_PackedScene_Godot_Control_GodotViews_VirtualGrid_IInfiniteLayoutGrid_"></a> WithArgument<TButtonType\>\(PackedScene, Control, IInfiniteLayoutGrid\)

Pass in the necessary arguments to the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>

```csharp
IFinishingArgumentBuilder<TDataType, TButtonType, NoExtraArgument> WithArgument<TButtonType>(PackedScene itemPrefab, Control itemContainer, IInfiniteLayoutGrid layoutGrid) where TButtonType : VirtualGridViewItem<TDataType, NoExtraArgument>
```

#### Parameters

`itemPrefab` PackedScene

The <xref href="Godot.PackedScene" data-throw-if-not-resolved="false"></xref> used for the virtualized grid element
    that have a script inherits <xref href="GodotViews.VirtualGrid.VirtualGridViewItem%602" data-throw-if-not-resolved="false"></xref> attached.

`itemContainer` Control

The <xref href="Godot.Control" data-throw-if-not-resolved="false"></xref> used for the container of all virtualized grid elements.

`layoutGrid` [IInfiniteLayoutGrid](GodotViews.VirtualGrid.IInfiniteLayoutGrid.md)

The <xref href="GodotViews.VirtualGrid.InfiniteLayoutGrids" data-throw-if-not-resolved="false"></xref> used to handle the layout positioning of all virtualized grid elements.

#### Returns

 [IFinishingArgumentBuilder](GodotViews.VirtualGrid.IFinishingArgumentBuilder\-3.md)<TDataType, TButtonType, [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)\>

A builder that concludes the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.

#### Type Parameters

`TButtonType` 

The type of the script attached to the <code class="paramref">itemPrefab</code>.

