using System;
using Godot;

namespace GodotViews.VirtualGrid;

public abstract partial class VirtualGridViewItem<TDataType, TExtraArgument> : Button
{
    internal record struct CurrentInfo(IVirtualGridViewParent<TDataType, TExtraArgument> Parent, int RowIndex, int ColumnIndex, TDataType? Data);
    
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
        if(!AccessInfo(out var info)) return;
        CallDelegate(_OnPressedHandler, info, "On Press");
    }
    
    public sealed override void _Notification(int what)
    {
        if(!AccessInfo(out var info)) return;
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
        Info = new(parent, -1, -1, default);
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