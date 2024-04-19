namespace GodotViews.VirtualGrid.Transition.GodotTween;

/// <summary>
/// Representing the transition controller that's implemented
/// by the Godot's built-in <see cref="Godot.Tween"/> system.
/// </summary>
public interface IGodotTween
{
    /// <summary>
    /// The duration required to perform the interpolating.
    /// </summary>
    float Duration { get; set; }
    
    /// <summary>
    /// The <see cref="TweenSetup"/> controller uses when doing the interpolation.
    /// </summary>
    TweenSetup TweenSetup { get; set; }
}