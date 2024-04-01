using System.Collections;
using Godot;

namespace GodotViews.VirtualGrid.Examples;

public record struct LogInfo(string time, string world, string message);

public partial class LogViewItem : VirtualGridViewItem<LogInfo, LogView>
{
    [Export] private Label _date;
    [Export] private Label _world;
    [Export] private Label _message;
    
    protected override void _OnGridItemDraw(LogInfo data, Vector2I gridPosition, LogView extraArgument)
    {
        _date.Text = data.time;
        _world.Text = data.world;
        _message.Text = data.message;
    }
}