# <a id="GodotViews_VirtualGrid_IDataStartHandler_1"></a> Interface IDataStartHandler<TArgument\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Data Start Handler is responsible for resolving the start position from the datasets when finding focus.

```csharp
public interface IDataStartHandler<in TArgument>
```

#### Type Parameters

`TArgument` 

## Methods

### <a id="GodotViews_VirtualGrid_IDataStartHandler_1_ResolveStartPosition__1_GodotViews_VirtualGrid_ReadOnlyDataArray___0____0_"></a> ResolveStartPosition<TDataType\>\(ref readonly ReadOnlyDataArray<TDataType\>, TArgument\)

Resolves the start position relative to the datasets.

```csharp
Vector2I ResolveStartPosition<TDataType>(ref readonly ReadOnlyDataArray<TDataType> currentView, TArgument argument)
```

#### Parameters

`currentView` [ReadOnlyDataArray](GodotViews.VirtualGrid.ReadOnlyDataArray\-1.md)<TDataType\>

Provides assess to the current datasets.

`argument` TArgument

An extra argument passes to the handler.

#### Returns

 Vector2I

The resolved start position, relative to the datasets position.

#### Type Parameters

`TDataType` 

The type for the data <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> focuses on.

