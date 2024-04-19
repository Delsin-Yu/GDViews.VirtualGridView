using GodotViews.VirtualGrid.Transition;
using GodotViews.VirtualGrid.Transition.GodotTween;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a set of built-in <see cref="IScrollBarTweener"/> that should cover common UI/UX development needs.
/// </summary>
public static partial class ScrollBarTweeners
{
    /// <summary>
    /// This scroll bar tweener does not perform any form of visual transitions,
    /// it is also the most efficient and snappy choice if the absolute performance is required.
    /// </summary>
    public static readonly IScrollBarTweener None = new NoneImpl();

    /// <summary>
    /// Create an scroll bar tweener that does value interpolation based on the provided arguments.
    /// </summary>
    /// <param name="duration">The duration this tweener takes to finish the value interpolation.</param>
    /// <param name="tweenSetup">The <see cref="TweenSetup"/> tweener uses when doing the interpolation.</param>
    /// <returns>The instance of the created scroll bar tweener that
    /// can be passed to the <see cref="IVirtualGridView{TDataType}"/>
    /// for handling the scroll bar value interpolation.</returns>
    public static IGodotTweenScrollBarTweener CreateLerp(float duration, TweenSetup? tweenSetup = null) =>
        new LerpImpl(duration, TweenSetups.CurrentOrDefault(tweenSetup));
}