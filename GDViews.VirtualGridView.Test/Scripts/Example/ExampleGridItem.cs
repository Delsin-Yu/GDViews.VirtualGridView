using System;
using Godot;
using GodotViews.VirtualGrid;

// ReSharper disable InvalidXmlDocComment

namespace GDViews.VirtualGrid.Example;

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