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
    public abstract partial class VirtualGridViewItem<TDataType, TExtraArgument> : Button
    {
        private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnAppearHandler;

        private readonly Action _OnCreateHandler;
        private readonly Action<TExtraArgument?> _OnDisappearHandler;

        private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnDrawHandler;

        private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnFocusEnteredHandler;
        private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnFocusExitedHandler;
        private readonly Action<InputEvent> _OnGuiInputHandler;

        private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveHandler;
        private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveInHandler;
        private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveOutHandler;
        private readonly Action<int> _OnNotificationHandler;
        private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnPressedHandler;

        private string? _cachedName;

        internal CellInfo? Info;

        /// <summary>
        /// Construct an instance of the <see cref="VirtualGridViewItem{TDataType,TExtraArgument}"/>
        /// </summary>
        protected VirtualGridViewItem()
        {
            _OnGuiInputHandler = _OnGuiInput;
            _OnNotificationHandler = _OnNotification;

            _OnCreateHandler = _OnGridItemCreate;

            _OnDrawHandler = _OnGridItemDraw;
            _OnMoveHandler = _OnGridItemMove;
            _OnMoveInHandler = _OnGridItemMoveIn;
            _OnMoveOutHandler = _OnGridItemMoveOut;
            _OnAppearHandler = _OnGridItemAppear;
            _OnDisappearHandler = _OnGridItemDisapper;
            _OnFocusEnteredHandler = _OnGridItemFocusEntered;
            _OnFocusExitedHandler = _OnGridItemFocusExited;
            _OnPressedHandler = _OnGridItemPressed;
        }

        private string LocalName => _cachedName ??= Name;

        /// <summary>
        /// This method is sealed overriden by the <see cref="VirtualGridViewItem{TDataType,TExtraArgument}"/>
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
                    info.Parent.MoveAndGrabFocus(MoveDirection.Down, info.RowIndex, info.ColumnIndex);
                    AcceptEvent();
                    return;
                }

                if (Check(Utils.UIUp, EdgeType.Up, in info, inputEvent))
                {
                    info.Parent.MoveAndGrabFocus(MoveDirection.Up, info.RowIndex, info.ColumnIndex);
                    AcceptEvent();
                    return;
                }

                if (Check(Utils.UILeft, EdgeType.Left, in info, inputEvent))
                {
                    info.Parent.MoveAndGrabFocus(MoveDirection.Left, info.RowIndex, info.ColumnIndex);
                    AcceptEvent();
                    return;
                }

                if (Check(Utils.UIRight, EdgeType.Right, in info, inputEvent))
                {
                    info.Parent.MoveAndGrabFocus(MoveDirection.Right, info.RowIndex, info.ColumnIndex);
                    AcceptEvent();
                    return;
                }

                DelegateRunner.RunProtected(_OnGuiInputHandler, inputEvent, "Gui Input", LocalName);
            }
        }

        private static bool Check(
            StringName actionName,
            EdgeType edgeType,
            ref readonly CellInfo info,
            InputEvent inputEvent
        ) =>
            info.ViewEdgeType.HasFlag(edgeType) &&
            !info.DataSetEdgeType.HasFlag(edgeType) &&
            inputEvent.IsAction(actionName, true);

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

        private void CallDelegate(Action<TExtraArgument?> call, in CellInfo info, string methodName)
        {
            DelegateRunner.RunProtected(
                call,
                info.Parent.ExtraArgument,
                methodName,
                LocalName
            );
        }

        private void CallDelegate(Action<TDataType, Vector2I, TExtraArgument?> call, in CellInfo info, string methodName)
        {
            DelegateRunner.RunProtected(
                call,
                info.Data!,
                Utils.CreatePosition(info.RowIndex, info.ColumnIndex),
                info.Parent.ExtraArgument,
                methodName,
                LocalName
            );
        }

        /// <summary>
        /// This method is sealed overriden by the <see cref="VirtualGridViewItem{TDataType,TExtraArgument}"/>
        /// for providing pressed mechanism, the developer may implement the
        /// <see cref="_OnGridItemPressed"/> for listening to pressed event.   
        /// </summary>
        public sealed override void _Pressed()
        {
            if (!TryGetInfo(out var info)) return;
            CallDelegate(_OnPressedHandler, info, "On Press");
        }


        /// <summary>
        /// This method is sealed overriden by the <see cref="VirtualGridViewItem{TDataType,TExtraArgument}"/>
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
                    CallDelegate(_OnFocusEnteredHandler, info, "On Focus Enter");
                    break;
                case NotificationFocusExit:
                    Utils.CurrentActiveGridView = null;
                    CallDelegate(_OnFocusExitedHandler, info, "On Focus Exit");
                    break;
                default:
                    DelegateRunner.RunProtected(_OnNotificationHandler, what, "Notification", LocalName);
                    break;
            }
        }

        internal void CallCreate() => DelegateRunner.RunProtected(_OnCreateHandler, "On Create", LocalName);

        internal void DrawGridItem(in CellInfo info)
        {
            Info = info;
            CallDelegate(_OnDrawHandler, info, "On Draw");
        }

        internal void CallAppear() => CallDelegate(_OnAppearHandler, Info!.Value, "On Appear");

        internal void CallDisappear()
        {
            CallDelegate(_OnDisappearHandler, Info!.Value, "On Disappear");
            Info = null;
        }

        internal void CallMove(in CellInfo info)
        {
            Info = info;
            CallDelegate(_OnMoveHandler, Info!.Value, "On Move");
        }

        internal void CallMoveIn() => CallDelegate(_OnMoveInHandler, Info!.Value, "On Move In");

        internal void CallMoveOut()
        {
            var currentInfo = Info!.Value;
            CallDelegate(_OnMoveOutHandler, currentInfo, "On Move Out");
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
        protected virtual void _OnGridItemDraw(TDataType data, Vector2I viewPosition, TExtraArgument? extraArgument) { }

        /// <summary>
        /// Invoked when the view controller is moving this virtualized grid element inside the viewport.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemMove(TDataType data, Vector2I viewPosition, TExtraArgument? extraArgument) { }

        /// <summary>
        /// Invoked when the view controller is moving this newly spawned or
        /// reused virtualized grid element instance into the viewport.  
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemMoveIn(TDataType data, Vector2I viewPosition, TExtraArgument? extraArgument) { }

        /// <summary>
        /// Invoked when the view controller is moving this
        /// virtualized grid element instance out from the viewport.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemMoveOut(TDataType data, Vector2I viewPosition, TExtraArgument? extraArgument) { }

        /// <summary>
        /// Invoked when the view controller is showing this virtualized grid element instance.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemAppear(TDataType data, Vector2I viewPosition, TExtraArgument? extraArgument) { }

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
        protected virtual void _OnGridItemFocusEntered(TDataType data, Vector2I viewPosition, TExtraArgument? extraArgument) { }

        /// <summary>
        /// Invoked when this virtualized grid element instance loses focus.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemFocusExited(TDataType data, Vector2I viewPosition, TExtraArgument? extraArgument) { }

        /// <summary>
        /// Invoked when this virtualized grid element instance is pressed.
        /// </summary>
        /// <param name="data">The data of the current virtualized grid element instance.</param>
        /// <param name="viewPosition">The position of this virtualized grid element instance in the viewport.</param>
        /// <param name="extraArgument">The extra argument passed to this virtualized grid element instance.</param>
        protected virtual void _OnGridItemPressed(TDataType data, Vector2I viewPosition, TExtraArgument? extraArgument) { }

        /// <summary>
        /// Stores the associated info assigned to the current virtualized grid element.
        /// </summary>
        public readonly struct CellInfo
        {
            internal CellInfo(
                IVirtualGridViewParent<TDataType, TExtraArgument> parent,
                int rowIndex,
                int columnIndex,
                EdgeType definedViewEdgeType,
                EdgeType viewEdgeType,
                EdgeType dataSetEdgeType,
                TDataType? data
            )
            {
                Parent = parent;
                RowIndex = rowIndex;
                ColumnIndex = columnIndex;
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
            /// The viewport row index this virtualized grid element belongs to.
            /// </summary>
            public readonly int RowIndex;

            /// <summary>
            /// The viewport column index this virtualized grid element belongs to.
            /// </summary>
            public readonly int ColumnIndex;

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
                $"({RowIndex},{ColumnIndex}), " +
                $"DefinedViewEdge: {DefinedViewEdgeType}, " +
                $"ViewEdge: {ViewEdgeType}, " +
                $"DataEdge: {DataSetEdgeType}, " +
                $"Data: {Data}";
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
        None = 0
    }
}