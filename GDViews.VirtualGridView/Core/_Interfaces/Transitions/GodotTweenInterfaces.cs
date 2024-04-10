namespace GodotViews.VirtualGrid;

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

/// <summary>
/// Representing the <see cref="IScrollBarTweener"/> that's implemented
/// by the Godot's built-in <see cref="Godot.Tween"/> system.
/// </summary>
public interface IGodotTweenScrollBarTweener : IGodotTween, IScrollBarTweener { }

/// <summary>
/// Representing the <see cref="IElementTweener"/> that's implemented
/// by the Godot's built-in <see cref="Godot.Tween"/> system.
/// </summary>
public interface IGodotTweenTweener : IGodotTween, IElementTweener { }

/// <summary>
/// Representing the <see cref="IElementFader"/> that's implemented
/// by the Godot's built-in <see cref="Godot.Tween"/> system.
/// </summary>
public interface IGodotTweenFader : IGodotTween, IElementFader { }