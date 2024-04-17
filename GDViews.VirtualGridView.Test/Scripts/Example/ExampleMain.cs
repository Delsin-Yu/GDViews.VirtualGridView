using System;
using System.Collections.Generic;
using Godot;
using GodotViews.VirtualGrid;

namespace GDViews.VirtualGrid.Example;

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