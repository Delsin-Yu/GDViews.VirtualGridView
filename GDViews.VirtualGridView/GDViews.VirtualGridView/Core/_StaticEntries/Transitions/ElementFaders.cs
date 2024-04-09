namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a set of built-in <see cref="IElementFader"/> that should cover common UI/UX development needs.
/// </summary>
public static partial class ElementFaders
{
    /// <summary>
    /// This element fader does not perform any form of visual transitions,
    /// it is also the most efficient and snappy choice if the absolute performance is required.
    /// </summary>
    public static readonly IElementFader None = new NonFaderImpl();

    /// <summary>
    /// Create an element fader that does <see cref="Godot.CanvasItem.Modulate"/> interpolation based on the provided arguments.
    /// </summary>
    /// <param name="duration">The duration this fader takes to do finish interpolation.</param>
    /// <param name="tweenSetup">The <see cref="TweenSetup"/> fader uses when doing the interpolation.</param>
    /// <returns>The instance of the created element fader that
    /// can be passed to the <see cref="IVirtualGridView{TDataType}"/>
    /// for handling the virtual grid element fade interpolation.</returns>
    public static IGodotTweenFader CreateFade(float duration, TweenSetup? tweenSetup = null) => 
        new FadeFaderImpl(duration, TweenSetups.CurrentOrDefault(tweenSetup));

    /// <summary>
    /// Create an element fader that does <see cref="Godot.Control.Scale"/> interpolation based on the provided arguments.
    /// </summary>
    /// <param name="duration">The duration this fader takes to do finish interpolation.</param>
    /// <param name="tweenSetup">The <see cref="TweenSetup"/> fader uses when doing the interpolation.</param>
    /// <returns>The instance of the created element fader that
    /// can be passed to the <see cref="IVirtualGridView{TDataType}"/>
    /// for handling the virtual grid element fade interpolation.</returns>
    public static IGodotTweenFader CreateScale(float duration, TweenSetup? tweenSetup = null) =>
        new ScaleFaderImpl(duration, TweenSetups.CurrentOrDefault(tweenSetup));

    /// <summary>
    /// Create an element fader that does <see cref="Godot.Control.Scale"/> and <see cref="Godot.Control.Rotation"/> interpolation based on the provided arguments.
    /// </summary>
    /// <param name="duration">The duration this fader takes to do finish interpolation.</param>
    /// <param name="tweenSetup">The <see cref="TweenSetup"/> fader uses when doing the interpolation.</param>
    /// <returns>The instance of the created element fader that
    /// can be passed to the <see cref="IVirtualGridView{TDataType}"/>
    /// for handling the virtual grid element fade interpolation.</returns>
    public static IGodotTweenFader CreateScaleRotate(float duration, TweenSetup? tweenSetup = null) => 
        new ScaleRotateFaderImpl(duration, TweenSetups.CurrentOrDefault(tweenSetup));
}