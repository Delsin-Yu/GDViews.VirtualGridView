# <a id="GodotViews_VirtualGrid_IFinishingArgumentBuilder_3"></a> Interface IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

The builder that concludes the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.<br />

```csharp
public interface IFinishingArgumentBuilder<TDataType, TButtonType, in TExtraArgument> where TButtonType : VirtualGridViewItem<TDataType, in TExtraArgument>
```

#### Type Parameters

`TDataType` 

The type for the data the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> focuses on.

`TButtonType` 

The type of the script attached to the virtualized grid element.

`TExtraArgument` 

The extra argument passed to the script attached to the virtualized grid elements.

## Methods

### <a id="GodotViews_VirtualGrid_IFinishingArgumentBuilder_3_Build"></a> Build\(\)

Finish the final building configuration, and instantiate the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.

```csharp
IVirtualGridView<TDataType> Build()
```

#### Returns

 [IVirtualGridView](GodotViews.VirtualGrid.IVirtualGridView\-1.md)<TDataType\>

The instantiated <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.

### <a id="GodotViews_VirtualGrid_IFinishingArgumentBuilder_3_ConfigureExtraArgument__2_"></a> ConfigureExtraArgument\(TExtraArgument\)

Sets the extra argument that will passed to the script attached to the virtualized grid elements.

```csharp
IFinishingArgumentBuilder<TDataType, TButtonType, in TExtraArgument> ConfigureExtraArgument(TExtraArgument extraArgument)
```

#### Parameters

`extraArgument` TExtraArgument

The value of the extra argument.

#### Returns

 [IFinishingArgumentBuilder](GodotViews.VirtualGrid.IFinishingArgumentBuilder\-3.md)<TDataType, TButtonType, TExtraArgument\>

The same <xref href="GodotViews.VirtualGrid.IFinishingArgumentBuilder%603" data-throw-if-not-resolved="false"></xref>
    for continuing the configuration of this <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.

### <a id="GodotViews_VirtualGrid_IFinishingArgumentBuilder_3_ConfigureHorizontalScrollBar_Godot_ScrollBar_GodotViews_VirtualGrid_IScrollBarTweener_GodotViews_VirtualGrid_IElementFader_System_Boolean_"></a> ConfigureHorizontalScrollBar\(ScrollBar, IScrollBarTweener?, IElementFader?, bool\)

Assign a <xref href="Godot.ScrollBar" data-throw-if-not-resolved="false"></xref> to the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for it to become the horizontal progress indicator.

```csharp
IFinishingArgumentBuilder<TDataType, TButtonType, in TExtraArgument> ConfigureHorizontalScrollBar(ScrollBar horizontalScrollBar, IScrollBarTweener? tweener = null, IElementFader? fader = null, bool autoHide = false)
```

#### Parameters

`horizontalScrollBar` ScrollBar

The <xref href="Godot.ScrollBar" data-throw-if-not-resolved="false"></xref> to associate to.

`tweener` [IScrollBarTweener](GodotViews.VirtualGrid.IScrollBarTweener.md)?

The <xref href="GodotViews.VirtualGrid.IScrollBarTweener" data-throw-if-not-resolved="false"></xref> used to handle the value interpolation of the scroll bar.

`fader` [IElementFader](GodotViews.VirtualGrid.IElementFader.md)?

The <xref href="GodotViews.VirtualGrid.IElementTweener" data-throw-if-not-resolved="false"></xref> used to hiding or showing the scroll bar.

`autoHide` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to hide the scroll bar
    when the current viewport is horizontally sufficient for showing every element of the datasets.

#### Returns

 [IFinishingArgumentBuilder](GodotViews.VirtualGrid.IFinishingArgumentBuilder\-3.md)<TDataType, TButtonType, TExtraArgument\>

The same <xref href="GodotViews.VirtualGrid.IFinishingArgumentBuilder%603" data-throw-if-not-resolved="false"></xref>
    for continuing the configuration of this <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.

### <a id="GodotViews_VirtualGrid_IFinishingArgumentBuilder_3_ConfigureVerticalScrollBar_Godot_ScrollBar_GodotViews_VirtualGrid_IScrollBarTweener_GodotViews_VirtualGrid_IElementFader_System_Boolean_"></a> ConfigureVerticalScrollBar\(ScrollBar, IScrollBarTweener?, IElementFader?, bool\)

Assign a <xref href="Godot.ScrollBar" data-throw-if-not-resolved="false"></xref> to the building <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for it to become the vertical progress indicator.

```csharp
IFinishingArgumentBuilder<TDataType, TButtonType, in TExtraArgument> ConfigureVerticalScrollBar(ScrollBar verticalScrollBar, IScrollBarTweener? tweener = null, IElementFader? fader = null, bool autoHide = false)
```

#### Parameters

`verticalScrollBar` ScrollBar

The <xref href="Godot.ScrollBar" data-throw-if-not-resolved="false"></xref> to associate to.

`tweener` [IScrollBarTweener](GodotViews.VirtualGrid.IScrollBarTweener.md)?

The <xref href="GodotViews.VirtualGrid.IScrollBarTweener" data-throw-if-not-resolved="false"></xref> used to handle the value interpolation of the scroll bar.

`fader` [IElementFader](GodotViews.VirtualGrid.IElementFader.md)?

The <xref href="GodotViews.VirtualGrid.IElementTweener" data-throw-if-not-resolved="false"></xref> used to hiding or showing the scroll bar.

`autoHide` [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

Instructs the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> to hide the scroll bar
    when the current viewport is vertically sufficient for showing every element of the datasets.

#### Returns

 [IFinishingArgumentBuilder](GodotViews.VirtualGrid.IFinishingArgumentBuilder\-3.md)<TDataType, TButtonType, TExtraArgument\>

The same <xref href="GodotViews.VirtualGrid.IFinishingArgumentBuilder%603" data-throw-if-not-resolved="false"></xref>
    for continuing the configuration of this <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.

