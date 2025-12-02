using System;
using Godot;

namespace GodotViews.VirtualGrid
{
    /// <summary>
    /// Inherit this type to create a script that can be attached to a <see cref="PackedScene"/>
    /// which makes it a valid prefab for use with <see cref="IVirtualGridView{TDataType}"/>.
    /// </summary>
    /// <typeparam name="TDataType">The type for the data the <see cref="IVirtualGridView{TDataType}"/> focuses on.</typeparam>
    /// <typeparam name="TExtraArgument">The extra argument passed to the various APIs
    /// of the instances of the attached <see cref="PackedScene"/>.</typeparam>
    /// <remarks>If the <typeparamref name="TExtraArgument"/> is unnecessary for the design,
    /// the developer may inherit the alternative type <see cref="GodotViews.VirtualGrid.Builder.IFinishingBuilderAccess{TDataType}.WithArgument{TButtonType}"/>
    /// and use the builder argument <see cref="GodotViews.VirtualGrid.Builder.IFinishingBuilderAccess{TDataType}"/>.</remarks>
    public partial class VirtualGridViewItemArg<TDataType, TExtraArgument> : Button
    {
        private readonly Action<TDataType, Vector2I, TExtraArgument> _onAppearHandler;

        private readonly Action _onCreateHandler;
        private readonly Action<TExtraArgument?> _onDisappearHandler;

        private readonly Action<TDataType, Vector2I, TExtraArgument> _onDrawHandler;

        private readonly Action<TDataType, Vector2I, TExtraArgument> _onFocusEnteredHandler;
        private readonly Action<TDataType, Vector2I, TExtraArgument> _onFocusExitedHandler;
        private readonly Action<InputEvent> _onGuiInputHandler;

        private readonly Action<TDataType, Vector2I, TExtraArgument> _onMoveHandler;
        private readonly Action<TDataType, Vector2I, TExtraArgument> _onMoveInHandler;
        private readonly Action<TDataType, Vector2I, TExtraArgument> _onMoveOutHandler;
        private readonly Action<int> _onNotificationHandler;
        private readonly Action<TDataType, Vector2I, TExtraArgument> _onPressedHandler;

        private string? _cachedName;

        internal CellInfo? Info;

        /// <summary>
        /// Construct an instance of the <see cref="VirtualGridViewItemArg{TDataType,TExtraArgument}"/>
        /// </summary>
        protected VirtualGridViewItemArg()
        {
            _onGuiInputHandler = _OnGuiInput;
            _onNotificationHandler = _OnNotification;

            _onCreateHandler = _OnGridItemCreate;

            _onDrawHandler = _OnGridItemDraw;
            _onMoveHandler = _OnGridItemMove;
            _onMoveInHandler = _OnGridItemMoveIn;
            _onMoveOutHandler = _OnGridItemMoveOut;
            _onAppearHandler = _OnGridItemAppear;
            _onDisappearHandler = _OnGridItemDisapper;
            _onFocusEnteredHandler = _OnGridItemFocusEntered;
            _onFocusExitedHandler = _OnGridItemFocusExited;
            _onPressedHandler = _OnGridItemPressed;
        }

        private string LocalName => _cachedName ??= Name;

        /// <summary>
        /// This method is sealed overriden by the <see cref="VirtualGridViewItemArg{TDataType,TExtraArgument}"/>
        /// for providing viewport edge detection mechanism, the developer may implement the
        /// <see cref="_OnGuiInput"/> for listening other input events.   
        /// </summary>
        public sealed override void _GuiInput(InputEvent inputEvent)
        {
            using (inputEvent)
            {
                if (!TryGetInfo(out var info)) return;

                if (inputEvent.IsReleased()) return;

                if (Check(Utils.UIDown, EdgeType.Down, in info, inputEvent))
                {
                    info.Parent.MoveAndGrabFocus(MoveDirection.Down, info.XIndex, info.YIndex);
                    AcceptEvent();
                    return;
                }

                if (Check(Utils.UIUp, EdgeType.Up, in info, inputEvent))
                {
                    info.Parent.MoveAndGrabFocus(MoveDirection.Up, info.XIndex, info.YIndex);
                    AcceptEvent();
                    return;
                }

                if (Check(Utils.UILeft, EdgeType.Left, in info, inputEvent))
                {
                    info.Parent.MoveAndGrabFocus(MoveDirection.Left, info.XIndex, info.YIndex);
                    AcceptEvent();
                    return;
                }

                if (Check(Utils.UIRight, EdgeType.Right, in info, inputEvent))
                {
                    info.Parent.MoveAndGrabFocus(MoveDirection.Right, info.XIndex, info.YIndex);
                    AcceptEvent();
                    return;
                }

                DelegateRunner.RunProtected(_onGuiInputHandler, inputEvent, "Gui Input", LocalName);
            }
        }

        private static bool Check(
            StringName actionName,
            EdgeType edgeType,
            ref readonly CellInfo info,
            InputEvent inputEvent
        ) =>
            info.ViewEdgeType.HasFlag(edgeType) && !info.DataSetEdgeType.HasFlag(edgeType) && inputEvent.IsAction(actionName, true);

        /// <summary>
        /// Try to get the info associated with this instance of virtualized grid element.
        /// </summary>
        /// <param name="info">The info associated with this instance of virtualized grid element.</param>
        /// <returns><see langword="true" /> if the current virtualized grid element has data associated to;
        /// otherwise, <see langword="false" />.</returns>
        protected bool TryGetInfo(out CellInfo info)
        {
            if (Info is null)
            {
                info = default;
                return false;
            }

            info = Info.Value;
            return true;
        }

        private void CallDelegate(Action<TExtraArgument> call, in CellInfo info, string methodName) =>
            DelegateRunner.RunProtected(
                call,
                info.Parent.ExtraArgument!,
                methodName,
                LocalName
            );

        private void CallDelegate(Action<TDataType, Vector2I, TExtraArgument> call, in CellInfo info, string methodName) =>
            DelegateRunner.RunProtected(
                call,
                info.Data!,
                new(info.XIndex, info.YIndex),
                info.Parent.ExtraArgument!,
                methodName,
                LocalName
            );

        /// <summary>
        /// This method is sealed overriden by the <see cref="VirtualGridViewItemArg{TDataType,TExtraArgument}"/>
        /// for providing pressed mechanism, the developer may implement the
        /// <see cref="_OnGridItemPressed"/> for listening to pressed event.   
        /// </summary>
        public sealed override void _Pressed()
        {
            if (!TryGetInfo(out var info)) return;
            CallDelegate(_onPressedHandler, info, "On Press");
        }


        /// <summary>
        /// This method is sealed overriden by the <see cref="VirtualGridViewItemArg{TDataType,TExtraArgument}"/>
        /// for providing focus management mechanism, the developer may implement the
        /// <see cref="_OnNotification"/> for listening other notifications.   
        /// </summary>
        public sealed override void _Notification(int what)
        {
            if (!TryGetInfo(out var info)) return;

            switch ((long)what)
            {
                case NotificationFocusEnter:
                    Utils.CurrentActiveGridView = info.Parent;
                    info.Parent.FocusTo(info);
                    CallDelegate(_onFocusEnteredHandler, info, "On Focus Enter");
                    break;
                case NotificationFocusExit:
                    Utils.CurrentActiveGridView = null;
                    CallDelegate(_onFocusExitedHandler, info, "On Focus Exit");
                    break;
                default:
                    DelegateRunner.RunProtected(_onNotificationHandler, what, "Notification", LocalName);
                    break;
            }
        }

        internal void CallCreate() => DelegateRunner.RunProtected(_onCreateHandler, "On Create", LocalName);

        internal void DrawGridItem(in CellInfo info)
        {
            Info = info;
            CallDelegate(_onDrawHandler, info, "On Draw");
        }

        internal void CallAppear() => CallDelegate(_onAppearHandler, Info!.Value, "On Appear");

        internal void CallDisappear()
        {
            CallDelegate(_onDisappearHandler, Info!.Value, "On Disappear");
            Info = null;
        }

        internal void CallMove(in CellInfo info)
        {
            Info = info;
            CallDelegate(_onMoveHandler, Info!.Value, "On Move");
        }

        internal void CallMoveIn() => CallDelegate(_onMoveInHandler, Info!.Value, "On Move In");

        internal void CallMoveOut()
        {
            var currentInfo = Info!.Value;
            CallDelegate(_onMoveOutHandler, currentInfo, "On Move Out");
        }

        /// <inheritdoc cref="Control._GuiInput"/>
        protected virtual void _OnGuiInput(InputEvent inputEvent) { }

        /// <inheritdoc cref="GodotObject._Notification"/>
        protected virtual void _OnNotification(int what) { }

        /// <summary>
        /// Invoked when the view controller has just create this virtualized grid element instance.
        /// </summary>
        protected virtual void _OnGridItemCreate() { }

        /// <summary>
        /// Invoked when the internal data of the current virtualized grid element instance
        /// has changed (or initialized) and requires developer-implemented draw logic.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemDraw(TDataType data, Vector2I viewPosition, TExtraArgument extraArgument) { }

        /// <summary>
        /// Invoked when the view controller is moving this virtualized grid element inside the viewport.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemMove(TDataType data, Vector2I viewPosition, TExtraArgument extraArgument) { }

        /// <summary>
        /// Invoked when the view controller is moving this newly spawned or
        /// reused virtualized grid element instance into the viewport.  
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemMoveIn(TDataType data, Vector2I viewPosition, TExtraArgument extraArgument) { }

        /// <summary>
        /// Invoked when the view controller is moving this
        /// virtualized grid element instance out from the viewport.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemMoveOut(TDataType data, Vector2I viewPosition, TExtraArgument extraArgument) { }

        /// <summary>
        /// Invoked when the view controller is showing this virtualized grid element instance.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemAppear(TDataType data, Vector2I viewPosition, TExtraArgument extraArgument) { }

        /// <summary>
        /// Invoked when the view controller is hiding this virtualized grid element instance.
        /// </summary>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemDisapper(TExtraArgument? extraArgument) { }

        /// <summary>
        /// Invoked when this virtualized grid element instance grabs focus.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemFocusEntered(TDataType data, Vector2I viewPosition, TExtraArgument extraArgument) { }

        /// <summary>
        /// Invoked when this virtualized grid element instance loses focus.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemFocusExited(TDataType data, Vector2I viewPosition, TExtraArgument extraArgument) { }

        /// <summary>
        /// Invoked when this virtualized grid element instance is pressed.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemPressed(TDataType data, Vector2I viewPosition, TExtraArgument extraArgument) { }

        /// <summary>
        /// Stores the associated info assigned to the current virtualized grid element.
        /// </summary>
        public readonly struct CellInfo
        {
            internal CellInfo(
                IVirtualGridViewParent<TDataType, TExtraArgument> parent,
                int xIndex,
                int yIndex,
                EdgeType definedViewEdgeType,
                EdgeType viewEdgeType,
                EdgeType dataSetEdgeType,
                TDataType? data
            )
            {
                Parent = parent;
                XIndex = xIndex;
                YIndex = yIndex;
                DefinedViewEdgeType = definedViewEdgeType;
                ViewEdgeType = viewEdgeType;
                DataSetEdgeType = dataSetEdgeType;
                Data = data;
            }

            internal readonly IVirtualGridViewParent<TDataType, TExtraArgument> Parent;

            /// <summary>
            /// The extra argument associated to this virtualized grid element. 
            /// </summary>
            public TExtraArgument? ExtraArgument => Parent.ExtraArgument;

            /// <summary>
            /// The viewport x index this virtualized grid element belongs to.
            /// </summary>
            public readonly int XIndex;

            /// <summary>
            /// The viewport y index this virtualized grid element belongs to.
            /// </summary>
            public readonly int YIndex;

            /// <summary>
            /// The edge of the defined viewport this virtualized grid element belongs to.
            /// </summary>
            public readonly EdgeType DefinedViewEdgeType;

            /// <summary>
            /// The edge of the current displayed viewport this virtualized grid element belongs to.
            /// </summary>
            public readonly EdgeType ViewEdgeType;

            /// <summary>
            /// The edge of the dataset this virtualized grid element belongs to.
            /// </summary>
            public readonly EdgeType DataSetEdgeType;

            /// <summary>
            /// The associated data for this virtualized grid element.
            /// </summary>
            public readonly TDataType? Data;

            /// <summary>
            /// Returns the string representation for this <see cref="CellInfo"/>.
            /// </summary>
            /// <returns>The string representation for this <see cref="CellInfo"/>.</returns>
            public override string ToString() =>
                $"({XIndex},{YIndex}), " + $"DefinedViewEdge: {DefinedViewEdgeType}, " + $"ViewEdge: {ViewEdgeType}, " + $"DataEdge: {DataSetEdgeType}, " + $"Data: {Data}";
        }
    }
}

namespace GodotViews.VirtualGrid
{
    /// <summary>
    /// Defines the edge type of the current virtualized grid element.
    /// </summary>
    [Flags]
    public enum EdgeType : byte
    {
        /// <summary>
        /// The element is a part of the up edge.
        /// </summary>
        Up = 0b1000,

        /// <summary>
        /// The element is a part of the down edge.
        /// </summary>
        Down = 0b0100,

        /// <summary>
        /// The element is a part of the left edge.
        /// </summary>
        Left = 0b0010,

        /// <summary>
        /// The element is a part of the right edge.
        /// </summary>
        Right = 0b0001,

        /// <summary>
        /// The element does not belongs to any edge.
        /// </summary>
        None = 0,
    }
}