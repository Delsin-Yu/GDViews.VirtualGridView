# <a id="GodotViews_VirtualGrid_IInfiniteLayoutGrid"></a> Interface IInfiniteLayoutGrid

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Infinite Layout Grid is the abstraction of a layout grid that has infinite amount of cells.<br />
You may create an instance of the dynamic grid viewer from the <xref href="GodotViews.VirtualGrid.InfiniteLayoutGrids" data-throw-if-not-resolved="false"></xref> class.

```csharp
public interface IInfiniteLayoutGrid
```

## Properties

### <a id="GodotViews_VirtualGrid_IInfiniteLayoutGrid_ItemSeparation"></a> ItemSeparation

The distance between cells.

```csharp
Vector2 ItemSeparation { get; }
```

#### Property Value

 Vector2

### <a id="GodotViews_VirtualGrid_IInfiniteLayoutGrid_ItemSize"></a> ItemSize

The size of a cell.

```csharp
Vector2 ItemSize { get; }
```

#### Property Value

 Vector2

## Methods

### <a id="GodotViews_VirtualGrid_IInfiniteLayoutGrid_GetGridElementPosition_Godot_Vector2I_"></a> GetGridElementPosition\(Vector2I\)

Calculate the layout position for the given <code class="paramref">gridPosition</code>.

```csharp
Vector2 GetGridElementPosition(Vector2I gridPosition)
```

#### Parameters

`gridPosition` Vector2I

The position to calculate the layout position from.

#### Returns

 Vector2

The calculated layout position.

