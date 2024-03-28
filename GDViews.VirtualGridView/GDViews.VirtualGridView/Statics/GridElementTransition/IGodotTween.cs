using Godot;

namespace GodotViews.VirtualGrid;

public interface IGodotTween
{
    float Duration { get; set; }
    TweenSetup TweenSetup { get; set; }
}