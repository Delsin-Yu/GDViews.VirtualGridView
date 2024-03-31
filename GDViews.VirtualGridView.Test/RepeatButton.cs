using Godot;

namespace GodotViews.VirtualGrid.Examples;

public partial class RepeatButton : Button
{
    [Export] private float _repeatOffset;
    private Timer _timer;
    
    public override void _Ready()
    {
        ActionMode = ActionModeEnum.Press;
        
        _timer = new() { Autostart = false };
        _timer.Timeout += () => EmitSignal(SignalName.Pressed);
        ButtonDown += () => _timer.Start(_repeatOffset);
        ButtonUp += () => _timer.Stop();
        
        AddChild(_timer, @internal: InternalMode.Front);
    }
}