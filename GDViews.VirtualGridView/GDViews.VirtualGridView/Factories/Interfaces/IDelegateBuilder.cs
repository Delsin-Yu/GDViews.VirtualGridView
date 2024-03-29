using System;
using Godot;

namespace GodotViews.VirtualGrid;

public interface IDelegateBuilder<TDataType, TButtonType> where TButtonType : Button
{
    public IDelegateBuilder<TDataType, TButtonType> ConfigureDrawHandler(Action<TDataType, TButtonType> drawHandler);
    IDelegateBuilder<TDataType, TButtonType> ConfigureFocusEnteredHandler(Action<TDataType, TButtonType> focusEnteredHandler);
    IDelegateBuilder<TDataType, TButtonType> ConfigureFocusExitedHandler(Action<TDataType, TButtonType> focusExitedHandler);
    IDelegateBuilder<TDataType, TButtonType> ConfigurePressedHandler(Action<TDataType, TButtonType> pressedHandler);
    IFinishingArgumentBuilder<TDataType, TButtonType> WithArgument(PackedScene itemPrefab, Control itemContainer, IInfiniteLayoutGrid layoutGrid);
}