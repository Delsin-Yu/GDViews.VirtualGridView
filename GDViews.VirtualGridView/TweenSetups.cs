using Godot;
using GodotViews.VirtualGrid.Transition.GodotTween;

namespace GodotViews.VirtualGrid
{
    /// <summary>
    /// Provides a set of <see cref="TweenSetup"/> that should cover common UI/UX development needs,
    /// you may check their visualization from the https://easings.net/ website.
    /// </summary>
    public static class TweenSetups
    {
        /// <inheritdoc cref="Tween.TransitionType.Linear"/>
        public static readonly TweenSetup Linear = new(Tween.EaseType.InOut, Tween.TransitionType.Linear);

        /// <summary>
        /// Access the default 
        /// </summary>
        public static TweenSetup Default { get; set; } = EaseOut.Quad;

        internal static TweenSetup CurrentOrDefault(TweenSetup? current) => current ?? Default;

        /// <summary>
        /// Contains a set of <see cref="Tween.TransitionType"/> with an ease type of <see cref="Tween.EaseType.In"/>.
        /// </summary>
        public static class EaseIn
        {
            /// <inheritdoc cref="Tween.TransitionType.Sine"/>
            public static readonly TweenSetup Sine = new(Tween.EaseType.In, Tween.TransitionType.Sine);

            /// <inheritdoc cref="Tween.TransitionType.Quad"/>
            public static readonly TweenSetup Quad = new(Tween.EaseType.In, Tween.TransitionType.Quad);

            /// <inheritdoc cref="Tween.TransitionType.Cubic"/>
            public static readonly TweenSetup Cubic = new(Tween.EaseType.In, Tween.TransitionType.Cubic);

            /// <inheritdoc cref="Tween.TransitionType.Quart"/>
            public static readonly TweenSetup Quart = new(Tween.EaseType.In, Tween.TransitionType.Quart);

            /// <inheritdoc cref="Tween.TransitionType.Quint"/>
            public static readonly TweenSetup Quint = new(Tween.EaseType.In, Tween.TransitionType.Quint);

            /// <inheritdoc cref="Tween.TransitionType.Expo"/>
            public static readonly TweenSetup Expo = new(Tween.EaseType.In, Tween.TransitionType.Expo);

            /// <inheritdoc cref="Tween.TransitionType.Circ"/>
            public static readonly TweenSetup Circ = new(Tween.EaseType.In, Tween.TransitionType.Circ);

            /// <inheritdoc cref="Tween.TransitionType.Back"/>
            public static readonly TweenSetup Back = new(Tween.EaseType.In, Tween.TransitionType.Back);

            /// <inheritdoc cref="Tween.TransitionType.Elastic"/>
            public static readonly TweenSetup Elastic = new(Tween.EaseType.In, Tween.TransitionType.Elastic);

            /// <inheritdoc cref="Tween.TransitionType.Bounce"/>
            public static readonly TweenSetup Bounce = new(Tween.EaseType.In, Tween.TransitionType.Bounce);
        }

        /// <summary>
        /// Contains a set of <see cref="Tween.TransitionType"/> with an ease type of <see cref="Tween.EaseType.Out"/>.
        /// </summary>
        public static class EaseOut
        {
            /// <inheritdoc cref="Tween.TransitionType.Sine"/>
            public static readonly TweenSetup Sine = new(Tween.EaseType.Out, Tween.TransitionType.Sine);

            /// <inheritdoc cref="Tween.TransitionType.Quad"/>
            public static readonly TweenSetup Quad = new(Tween.EaseType.Out, Tween.TransitionType.Quad);

            /// <inheritdoc cref="Tween.TransitionType.Cubic"/>
            public static readonly TweenSetup Cubic = new(Tween.EaseType.Out, Tween.TransitionType.Cubic);

            /// <inheritdoc cref="Tween.TransitionType.Quart"/>
            public static readonly TweenSetup Quart = new(Tween.EaseType.Out, Tween.TransitionType.Quart);

            /// <inheritdoc cref="Tween.TransitionType.Quint"/>
            public static readonly TweenSetup Quint = new(Tween.EaseType.Out, Tween.TransitionType.Quint);

            /// <inheritdoc cref="Tween.TransitionType.Expo"/>
            public static readonly TweenSetup Expo = new(Tween.EaseType.Out, Tween.TransitionType.Expo);

            /// <inheritdoc cref="Tween.TransitionType.Circ"/>
            public static readonly TweenSetup Circ = new(Tween.EaseType.Out, Tween.TransitionType.Circ);

            /// <inheritdoc cref="Tween.TransitionType.Back"/>
            public static readonly TweenSetup Back = new(Tween.EaseType.Out, Tween.TransitionType.Back);

            /// <inheritdoc cref="Tween.TransitionType.Elastic"/>
            public static readonly TweenSetup Elastic = new(Tween.EaseType.Out, Tween.TransitionType.Elastic);

            /// <inheritdoc cref="Tween.TransitionType.Bounce"/>
            public static readonly TweenSetup Bounce = new(Tween.EaseType.Out, Tween.TransitionType.Bounce);
        }

        /// <summary>
        /// Contains a set of <see cref="Tween.TransitionType"/> with an ease type of <see cref="Tween.EaseType.InOut"/>.
        /// </summary>
        public static class EaseInOut
        {
            /// <inheritdoc cref="Tween.TransitionType.Sine"/>
            public static readonly TweenSetup Sine = new(Tween.EaseType.InOut, Tween.TransitionType.Sine);

            /// <inheritdoc cref="Tween.TransitionType.Quad"/>
            public static readonly TweenSetup Quad = new(Tween.EaseType.InOut, Tween.TransitionType.Quad);

            /// <inheritdoc cref="Tween.TransitionType.Cubic"/>
            public static readonly TweenSetup Cubic = new(Tween.EaseType.InOut, Tween.TransitionType.Cubic);

            /// <inheritdoc cref="Tween.TransitionType.Quart"/>
            public static readonly TweenSetup Quart = new(Tween.EaseType.InOut, Tween.TransitionType.Quart);

            /// <inheritdoc cref="Tween.TransitionType.Quint"/>
            public static readonly TweenSetup Quint = new(Tween.EaseType.InOut, Tween.TransitionType.Quint);

            /// <inheritdoc cref="Tween.TransitionType.Expo"/>
            public static readonly TweenSetup Expo = new(Tween.EaseType.InOut, Tween.TransitionType.Expo);

            /// <inheritdoc cref="Tween.TransitionType.Circ"/>
            public static readonly TweenSetup Circ = new(Tween.EaseType.InOut, Tween.TransitionType.Circ);

            /// <inheritdoc cref="Tween.TransitionType.Back"/>
            public static readonly TweenSetup Back = new(Tween.EaseType.InOut, Tween.TransitionType.Back);

            /// <inheritdoc cref="Tween.TransitionType.Elastic"/>
            public static readonly TweenSetup Elastic = new(Tween.EaseType.InOut, Tween.TransitionType.Elastic);

            /// <inheritdoc cref="Tween.TransitionType.Bounce"/>
            public static readonly TweenSetup Bounce = new(Tween.EaseType.InOut, Tween.TransitionType.Bounce);
        }
    }
}

namespace GodotViews.VirtualGrid.Transition.GodotTween
{
    /// <summary>
    /// The combination of <see cref="EaseType"/> and <see cref="TransitionType"/>. 
    /// </summary>
    /// <param name="EaseType">The <see cref="EaseType"/> of this <see cref="TweenSetup"/>.</param>
    /// <param name="TransitionType">The <see cref="TransitionType"/> of this <see cref="TweenSetup"/>.</param>
    public record struct TweenSetup(Tween.EaseType EaseType, Tween.TransitionType TransitionType);
}