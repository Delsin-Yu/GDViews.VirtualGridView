using System;
using Godot;

namespace GodotViews.VirtualGrid;

public abstract partial class VirtualGridViewItem<TDataType, TExtraArgument> : Button
{
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
    
    internal readonly Action<TDataType, Vector2I, TExtraArgument?> _OnDrawHandler; 
    internal readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveHandler; 
    internal readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveInHandler; 
    internal readonly Action<TDataType, Vector2I, TExtraArgument?> _OnMoveOutHandler; 
    internal readonly Action<TDataType, Vector2I, TExtraArgument?> _OnAppearHandler; 
    internal readonly Action<TExtraArgument?> _OnDisappearHandler; 
    internal readonly Action<TDataType, Vector2I, TExtraArgument?> _OnFocusEnteredHandler; 
    internal readonly Action<TDataType, Vector2I, TExtraArgument?> _OnFocusExitedHandler; 
    internal readonly Action<TDataType, Vector2I, TExtraArgument?> _OnPressedHandler; 

    private string? _cachedName;
    
    internal string LocalName => _cachedName ??= Name;
    
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