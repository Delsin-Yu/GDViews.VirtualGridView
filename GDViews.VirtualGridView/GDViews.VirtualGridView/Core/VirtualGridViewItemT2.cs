using System;
using System.Runtime.CompilerServices;
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
internal enum EdgeElementType : short
{
    Up = 1 << 0,
    Down = 1 << 2,
    Left = 1 << 3,
    Right = 1 << 4, 
    GlobalUp = 1 << 5,
    GlobalDown = 1 << 6,
    GlobalLeft = 1 << 7,
    GlobalRight = 1 << 8,
    None = 0
}


public abstract partial class VirtualGridViewItem<TDataType, TExtraArgument> : Button
{
    internal readonly record struct CurrentInfo(
        IVirtualGridViewParent<TDataType, TExtraArgument> Parent,
        int RowIndex,
        int ColumnIndex,
        EdgeElementType ElementType,
        TDataType? Data);

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

    internal CurrentInfo? Info { private get; set; }

    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnDrawHandler;
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveHandler;
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveInHandler;
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveOutHandler;
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnAppearHandler;
    private readonly Action<TExtraArgument?> _OnDisappearHandler;
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnFocusEnteredHandler;
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnFocusExitedHandler;
    private readonly Action<TDataType, Vector2I, TExtraArgument?> _OnPressedHandler;

    private string? _cachedName;

    internal string LocalName => _cachedName ??= Name;
    
    public sealed override void _GuiInput(InputEvent inputEvent)
    {
        using (inputEvent)
        {
            if(inputEvent.IsReleased()) return;
            if (!AccessInfo(out var info)) return;

            if (Check(UIInputActionNames.UIDown, EdgeElementType.Down, in info, inputEvent))
                info.Parent.MoveAndGrabFocus(MoveDirection.Down, info.RowIndex, info.ColumnIndex);
            else if (Check(UIInputActionNames.UIUp, EdgeElementType.Up, in info, inputEvent))
                info.Parent.MoveAndGrabFocus(MoveDirection.Up, info.RowIndex, info.ColumnIndex);
            else if (Check(UIInputActionNames.UILeft, EdgeElementType.Left, in info, inputEvent))
                info.Parent.MoveAndGrabFocus(MoveDirection.Left, info.RowIndex, info.ColumnIndex);
            else if(Check(UIInputActionNames.UIRight, EdgeElementType.Right, in info, inputEvent)) 
                info.Parent.MoveAndGrabFocus(MoveDirection.Right, info.RowIndex, info.ColumnIndex);
        }
    }

    private static bool Check(StringName actionName,
        EdgeElementType edgeElementType,
        ref readonly CurrentInfo info,
        InputEvent inputEvent)
    {
        if (!inputEvent.IsAction(actionName, true)) return false;
        if (!info.ElementType.HasFlag(edgeElementType)) return false;
        return true;
    }

    private bool AccessInfo(out CurrentInfo info)
    {
        if (Info is null)
        {
            info = default;
            return false;
        }

        info = Info.Value;
        return true;
    }

    private void CallDelegate(Action<TExtraArgument?> call, in CurrentInfo info, string methodName)
    {
        DelegateRunner.RunProtected(
            call,
            info.Parent.ExtraArgument,
            methodName,
            LocalName
        );
    }

    private void CallDelegate(Action<TDataType, Vector2I, TExtraArgument?> call, in CurrentInfo info, string methodName)
    {
        DelegateRunner.RunProtected(
            call,
            info.Data!,
            VirtualGridView.CreatePosition(info.RowIndex, info.ColumnIndex),
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
                info.Parent.FocusTo(info);
                CallDelegate(_OnFocusEnteredHandler, info, "On Focus Enter");
                break;
            case NotificationFocusExit:
                CallDelegate(_OnFocusExitedHandler, info, "On Focus Exit");
                break;
        }
    }

    internal void DrawGridItem(CurrentInfo info)
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

    internal void CallMove(CurrentInfo info)
    {
        Info = info;
        CallDelegate(_OnMoveHandler, Info!.Value, "On Move");
    }

    internal void CallMoveIn() => CallDelegate(_OnMoveInHandler, Info!.Value, "On Move In");

    internal void CallMoveOut()
    {
        var currentInfo = Info!.Value;
        CallDelegate(_OnMoveOutHandler, currentInfo, "On Move Out");
        var parent = currentInfo.Parent;
        Info = new(parent, -1, -1, EdgeElementType.None, default);
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
}