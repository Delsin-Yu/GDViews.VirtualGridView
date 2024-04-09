namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a set of built-in <see cref="IElementTweener"/> that should cover common UI/UX development needs.
/// </summary>
public static partial class ElementTweeners
{
    /// <summary>
    /// This element tweener does not perform any form of visual transitions,
    /// it is also the most efficient and snappy choice if the absolute performance is required.
    /// </summary>
    public static readonly IElementTweener None = new NoneTweenerImpl();

    /// <summary>
    /// Create an element tweener that does position interpolation based on the provided arguments.
    /// </summary>
    /// <param name="duration">The duration this tweener takes to finish moving a virtual grid element to the target position.</param>
    /// <param name="tweenSetup">The <see cref="TweenSetup"/> tweener uses when doing the interpolation.</param>
    /// <returns>The instance of the created element tweener that
    /// can be passed to the <see cref="IVirtualGridView{TDataType}"/>
    /// for handling the virtual grid element positional interpolation.</returns>
    public static IGodotTweenTweener CreatePan(float duration, TweenSetup? tweenSetup = null) => 
        new PanTweenerImpl(duration, TweenSetups.CurrentOrDefault(tweenSetup));
}