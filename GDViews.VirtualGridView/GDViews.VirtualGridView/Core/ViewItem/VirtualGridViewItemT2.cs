using System;
using Godot;

namespace GodotViews.VirtualGrid;

internal static class UIInputActionNames
{
    public static StringName UIDown = "ui_down";
    public static StringName UIUp = "ui_up";
    public static StringName UILeft = "ui_left";
    public static StringName UIRight = "ui_right";
}

[Flags]
public enum EdgeType : byte
{
    Up = 0b1000,
    Down = 0b0100,
    Left = 0b0010,
    Right = 0b0001,
    None = 0
}

public abstract partial class VirtualGridViewItem<TDataType, TExtraArgument> : Button
{
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnAppearHandler;
    private readonly Action<TExtraArgument?> _OnDisappearHandler;

    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnDrawHandler;

    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnFocusEnteredHandler;
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnFocusExitedHandler;

    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveHandler;
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveInHandler;
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveOutHandler;
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnPressedHandler;

    private string? _cachedName;

    private Label _label;

    internal CellInfo? Info;

    protected VirtualGridViewItem()
    {
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

    internal string LocalName => _cachedName ??= Name;

    public override void _Ready()
    {
        base._Ready();
        _label = new();
        _label.AddThemeFontSizeOverride("font_size", 12);
        AddChild(_label);
    }

    public sealed override void _GuiInput(InputEvent inputEvent)
    {
        using (inputEvent)
        {
            if (!AccessInfo(out var info)) return;

            if (inputEvent.IsReleased()) return;

            if (Check(UIInputActionNames.UIDown, EdgeType.Down, in info, inputEvent))
            {
                info.Parent.MoveAndGrabFocus(MoveDirection.Down, info.RowIndex, info.ColumnIndex);
                return;
            }

            if (Check(UIInputActionNames.UIUp, EdgeType.Up, in info, inputEvent))
            {
                info.Parent.MoveAndGrabFocus(MoveDirection.Up, info.RowIndex, info.ColumnIndex);
                return;
            }

            if (Check(UIInputActionNames.UILeft, EdgeType.Left, in info, inputEvent))
            {
                info.Parent.MoveAndGrabFocus(MoveDirection.Left, info.RowIndex, info.ColumnIndex);
                return;
            }

            if (Check(UIInputActionNames.UIRight, EdgeType.Right, in info, inputEvent)) info.Parent.MoveAndGrabFocus(MoveDirection.Right, info.RowIndex, info.ColumnIndex);
        }
    }

    private static bool Check(
        StringName actionName,
        EdgeType edgeType,
        ref readonly CellInfo info,
        InputEvent inputEvent
    )
    {
        if (!info.ViewEdgeType.HasFlag(edgeType) || info.DataSetEdgeType.HasFlag(edgeType)) return false;
        if (!inputEvent.IsAction(actionName, true)) return false;
        return true;
    }

    private bool AccessInfo(out CellInfo info)
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

    public sealed override void _Pressed()
    {
        if (!AccessInfo(out var info)) return;
        CallDelegate(_OnPressedHandler, info, "On Press");
    }

    public sealed override void _Notification(int what)
    {
        if (!AccessInfo(out var info)) return;
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
        }
    }

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

    protected virtual void _OnGridItemDraw(TDataType data, Vector2I gridPosition, TExtraArgument? extraArgument) { }
    protected virtual void _OnGridItemMove(TDataType data, Vector2I gridPosition, TExtraArgument? extraArgument) { }
    protected virtual void _OnGridItemMoveIn(TDataType data, Vector2I gridPosition, TExtraArgument? extraArgument) { }
    protected virtual void _OnGridItemMoveOut(TDataType data, Vector2I gridPosition, TExtraArgument? extraArgument) { }
    protected virtual void _OnGridItemAppear(TDataType data, Vector2I gridPosition, TExtraArgument? extraArgument) { }
    protected virtual void _OnGridItemDisapper(TExtraArgument? extraArgument) { }
    protected virtual void _OnGridItemFocusEntered(TDataType data, Vector2I gridPosition, TExtraArgument? extraArgument) { }
    protected virtual void _OnGridItemFocusExited(TDataType data, Vector2I gridPosition, TExtraArgument? extraArgument) { }
    protected virtual void _OnGridItemPressed(TDataType data, Vector2I gridPosition, TExtraArgument? extraArgument) { }

    public readonly struct CellInfo
    {
        internal CellInfo(IVirtualGridViewParent<TDataType, TExtraArgument> parent, int rowIndex, int columnIndex, EdgeType definedViewEdgeType, EdgeType viewEdgeType, EdgeType dataSetEdgeType, TDataType? data)
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
        public readonly int RowIndex;
        public readonly int ColumnIndex;
        public readonly EdgeType DefinedViewEdgeType;
        public readonly EdgeType ViewEdgeType;
        public readonly EdgeType DataSetEdgeType;
        public readonly TDataType? Data;


        public override string ToString() => $"({RowIndex},{ColumnIndex}), DefinedViewEdge: {DefinedViewEdgeType}, ViewEdge: {ViewEdgeType}, DataEdge: {DataSetEdgeType}, Data: {Data}";
    }
}