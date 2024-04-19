# <a id="GodotViews_VirtualGrid_IViewHandlerBuilder"></a> Interface IViewHandlerBuilder

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

The builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.<br />
Use the <xref href="GodotViews.VirtualGrid.IViewHandlerBuilder.WithHandlers(GodotViews.VirtualGrid.IElementPositioner%2cGodotViews.VirtualGrid.IElementTweener%2cGodotViews.VirtualGrid.IElementFader)" data-throw-if-not-resolved="false"></xref> method to set up the visual transition behavior for the grid elements.

```csharp
public interface IViewHandlerBuilder
```

## Methods

### <a id="GodotViews_VirtualGrid_IViewHandlerBuilder_WithHandlers_GodotViews_VirtualGrid_IElementPositioner_GodotViews_VirtualGrid_IElementTweener_GodotViews_VirtualGrid_IElementFader_"></a> WithHandlers\(IElementPositioner, IElementTweener, IElementFader\)

Sets the visual transition behavior for the grid elements, and moves to the next build process.

```csharp
IDataLayoutBuilder WithHandlers(IElementPositioner elementPositioner, IElementTweener elementTweener, IElementFader elementFader)
```

#### Parameters

`elementPositioner` [IElementPositioner](GodotViews.VirtualGrid.IElementPositioner.md)

The Positioner assigned to the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.

`elementTweener` [IElementTweener](GodotViews.VirtualGrid.IElementTweener.md)

The Tweener assigned to the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.

`elementFader` [IElementFader](GodotViews.VirtualGrid.IElementFader.md)

The Fader assigned to the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.

#### Returns

 [IDataLayoutBuilder](GodotViews.VirtualGrid.IDataLayoutBuilder.md)

A builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.

