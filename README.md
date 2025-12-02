# GDViews.VirtualGridView

[![GitHub Release](https://img.shields.io/github/v/release/Delsin-Yu/GDViews.VirtualGridView)](https://github.com/Delsin-Yu/GDViews.VirtualGridView/releases/latest) [![Stars](https://img.shields.io/github/stars/Delsin-Yu/GDViews.VirtualGridView?color=brightgreen)](https://github.com/Delsin-Yu/GDViews.VirtualGridView/stargazers) [![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/Delsin-Yu/GDViews.VirtualGridView/blob/main/LICENSE)

## Introduction

Supports in `Godot 4.1+` with `mono (.Net)` module.
***GDViews.VirtualGridView*** is a `Godot 4` UI Component that provides classes that's useful for creating highly customizable, fully virtualized list/grid views.

You may visit the [**API Documentations Page**](https://delsin-yu.github.io/GDViews.VirtualGridView/) for detailed documentation.

## Installation

As the Godot doesn't work well with external assemblies, the `GD Panel Framework` is now distributed by `zip/addon` instead of being a Nuget Package.

1. Ensure your project C# language version is at least `<LangVersion>12</LangVersion>`.
2. Download the `GDViews.VirtualGridView.zip` from the latest [release](https://github.com/Delsin-Yu/GDViews.VirtualGridView/releases).
3. Decompress the file and place the `addons` directory into the project root (`res://`).

This addon only contains source file for runtime use; so you do not need to enable it from the `Plugin` window.

---

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
## Table of Contents

- [Glossarys](#glossarys)
  - [`VirtualGridView / GridView`](#virtualgridview--gridview)
  - [`VirtualGridViewItem / GridViewItem`](#virtualgridviewitem--gridviewitem)
- [API Usage](#api-usage)
  - [Creating a `GridViewItem`](#creating-a-gridviewitem)
  - [Creating a `GridView`](#creating-a-gridview)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

---

## Glossarys

### `VirtualGridView / GridView`

The C# type that controls a group of associated `GridViewItem` and handles the all the necessary item virtualization, layout calculations, event method invoking, and input processing; it provides APIs that allow the developer to control the internal focused items, movement of virtual viewports and triggering redraws.

### `VirtualGridViewItem / GridViewItem`

The script(s) inheriting the or `VirtualGridViewItemT`, attaching the script to a control to make it a `GridViewItem`, this type contains all the necessary event method for customizing the visual behaviour of a `GridViewItem`, the developer should pass their implementation of `GridViewItem` PackedScene to the corresponding builder(mentioned below) when construct.

## API Usage

### Creating a `GridViewItem`

Attach the following script to a `Control` to make it a `GridViewItem`.  
This time log view item displays the time for each log entry, and contains a button that allows user to remove the current entry when pressed.

```csharp
/// <summary>
/// The model that describes the data within each cell.
/// </summary>
/// <param name="ID">The data ID.</param>
/// <param name="CurrentTime">The time when adding the data.</param>
public record struct DataModel(int ID, DateTime CurrentTime);

/// <summary>
/// The type that handles logics related to per-virtualized grid element interactions, attach this script to a <see cref="Button"/> to make it a grid item.
/// </summary>
public partial class ExampleGridItem : VirtualGridViewItem<DataModel, ExampleMain>
{
    // Assigned from the inspector.
    [Export] private Label _id;
    [Export] private Label _time;
    [Export] private Button _deleteButton;

    /// <summary>
    /// Invoked when the view controller has just create this virtualized grid element instance.
    /// </summary>
    protected override void _OnGridItemCreate()
    {
        _deleteButton.Pressed += () =>
        {
            // Do nothing if the current element is invalid (hidden or empty)
            if (!TryGetInfo(out var info)) return;

            // Do nothing if the associated value is null (moving out)
            if (info.ExtraArgument is null || info.Data == default) return;

            // Notify the controller to remove the data
            // associated to this element from the dataset, and performs a redraw. 
            info.ExtraArgument.RemoveEntry(info.Data);
        };
    }

    /// <summary>
    /// Invoked when the internal data of the current virtualized grid element instance
    /// has changed (or initialized) and requires developer-implemented draw logic.
    /// </summary>
    /// <param name="data">The data of the current virtualized grid element instance.</param>
    protected override void _OnGridItemDraw(DataModel data, Vector2I _, ExampleMain __)
    {
        // Developer defined draw logic.
        _id.Text = data.ID.ToString("D2");
        _time.Text = data.CurrentTime.ToString("yyyy-MM-dd HH:mm:ss:ffff");
    }
}
```

### Creating a `GridView`

The `VirtualGridView` is pure C# implementation, so instead of attaching a script to a node in the scene tree, developers need to create and reference it in scripts, due to the complexity of the creation process, `the builder design pattern` is used.

```csharp
/// <summary>
/// Attach this script to a node from the scene tree, and assigning the required exported fields.
/// </summary>
public partial class ExampleMain : Node
{
    [Export] private Button _addData;

    [Export] private int _displayedItems;
    [Export] private PackedScene _itemPrefab;
    [Export] private Control _itemContainer;
    [Export] private Vector2 _itemSize;
    [Export] private Vector2 _itemSeparation;
    [Export] private ScrollBar _verticalScrollBar;

    private IVirtualGridView<DataModel> _virtualGridView;
    private readonly List<DataModel> _dataset = [];

    /// <summary>
    /// Called when the node is "ready", i.e. when both the node and its children have entered the scene tree.
    /// </summary>
    public override void _Ready()
    {
        _virtualGridView = 
    
            // Call the Create function under this static class to initiate a build.
            VirtualGridView
                // Here we are specifying the viewport dimensions
                .Create(viewportColumns: 1, viewportRows: _displayedItems)
                // Call the WithHandlers function to specifying the visual logic.
                .WithHandlers(
                
                    // Handles the positioning of the virtual viewport,
                    // side positioner does not move the viewport
                    // unless the selected item lies outside of the viewport.
                    elementPositioner: 
                    ElementPositioners.Side,
                    
                    // Manages the visual positional interpolation of the
                    // elements when user moves the virtualized viewport.
                    // pan tweener does position interpolation.
                    elementTweener: 
                    ElementTweeners.CreatePan(duration: 0.1f, tweenSetup: TweenSetups.EaseOut.Quad),
                    
                    // Manages the hiding and showing of the virtualized elements.
                    // fade fader does Modulate interpolation.
                    elementFader: 
                    ElementFaders.CreateFade(duration: 0.1f, tweenSetup: TweenSetups.EaseInOut.Sine)
                    
                )
                
                // Specifying the dataset positioning, developer may choose to
                // horizontally or vertically layout their dataset(s).
                // Here we choose to layout our data set vertically,
                // so it will show like:
                //
                //     [Column 0]
                //     [DataSet0]
                //    |    00    |
                //    |    01    |
                //    |    02    |
                //    |    03    |
                //    |    04    |
                //
                // Specifying the type of the dataset as well.
                .WithVerticalDataLayout<DataModel>()
                
                    // Append our dataset to the building datasets
                    // developer may append multiple datasets, under 
                    // the vertical data layout, it will show like:
                    //
                    //     [Column 0] [Column 1] [Column 2]
                    //     [DataSet0] [DataSet0] [DataSet1]
                    //    |    00    |    01    |    00    |
                    //    |    02    |    03    |    01    |
                    //    |    04    |    05    |    02    |
                    //    |    08    |    09    |    04    |
                    //    |    06    |    07    |    03    |
                    //                           ^^ +New ^^
                    //
                    // the developer may also choose to make one dataset
                    // to occupy multiple columns by increasing the repeat
                    // argument or add the same IDynamicGridViewer multiple times
                    .AppendColumnDataSet(
                    
                            // The dynamic grid viewer emulates a 2D list access
                            // from the backing list we passes in.
                            DynamicGridViewers.CreateList(_dataset)
                            
                        )
                // Call the WithArgument function to specifying the rest of the arguments.
                .WithArgument<ExampleGridItem, ExampleMain>(
                    
                    // The prefab that's used to create
                    // instances of the virtualized elements.
                    _itemPrefab,
                    
                    // The container for the virtualized elements.
                    // this container is also used for receiving inputs.
                    _itemContainer,
                    
                    // Infinite Layout Grid is the abstraction of a layout grid that has infinite amount of cells.
                    // simple infinite layout grid that has functions equivalent to a GridContainer.
                    InfiniteLayoutGrids.CreateSimple(
                        
                        // Size of one virtualized element.
                        _itemSize,
                        
                        // Separation between the elements
                        _itemSeparation
                        
                        )
                )
                
                    // Pass the current instance to the view
                    // so that we can get this instance from
                    // the virtualized instance
                    .ConfigureExtraArgument(this)
                
                    // The vertical scroll bar use to indicate
                    // the current viewport position relative to the
                    // data sets.
                    .ConfigureVerticalScrollBar(
                    
                        _verticalScrollBar, 
                        
                        // Managing the Value and Page value interpolation
                        // of the ScrollBar when user moves the virtualized viewport.
                        ScrollBarTweeners.CreateLerp(0.1f, TweenSetups.EaseInOut.Sine), 
                        
                        // Setting autohide to true will make the
                        // view hide the scroll bar when it's unnecessary.
                        autoHide: true
                        
                        )
                
                // Finish the build and get the built instance.
                .Build();

        // Press this button to add an entry to the dataset,
        // and triggers the view redraw.
        _addData.Pressed += AddEntry;
    }

    /// <summary>
    /// Add an entry to the dataset and triggers the redraw.
    /// </summary>
    private void AddEntry()
    {
        var id = _dataset.Count;
        var time = DateTime.Now;

        // Add to the dataset.
        _dataset.Add(new(id, time));
        
        // Make the view to redraw.
        _virtualGridView.Redraw();
        
        // Make sure we have a focus
        // (for platforms that do not have pointing device).
        _virtualGridView.GrabFocus();
    }

    /// <summary>
    /// Removes the specified value from the dataset and triggers the redraw.
    /// </summary>
    public void RemoveEntry(DataModel dataModelToRemove)
    {
        // Remove from the dataset.
        _dataset.Remove(dataModelToRemove);
        
        // Make the view to redraw.
        _virtualGridView.Redraw();
        
        // Make sure we have a focus
        // (for platforms that do not have pointing device).
        _virtualGridView.GrabFocus();
    }
}
```
