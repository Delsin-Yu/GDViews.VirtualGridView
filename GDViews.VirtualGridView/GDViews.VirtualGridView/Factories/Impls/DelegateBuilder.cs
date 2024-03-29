using System;
using Godot;

namespace GodotViews.VirtualGrid;

internal class DelegateBuilder<TDataType, TButtonType>(DataLayoutBuilder<TDataType> dataLayoutBuilder, IDataInspector<TDataType> dataInspector) : IDelegateBuilder<TDataType, TButtonType> where TButtonType : Button
{
    public DataLayoutBuilder<TDataType> DataLayoutBuilder { get; } = dataLayoutBuilder;
    public IDataInspector<TDataType> DataInspector { get; } = dataInspector;

    public Action<TDataType, TButtonType>? DrawHandler { get; private set; }
    public Action<TDataType, TButtonType>? FocusEnteredHandler { get; private set; }
    public Action<TDataType, TButtonType>? FocusExitedHandler { get; private set; }
    public Action<TDataType, TButtonType>? PressedHandler { get; private set; }


    public IDelegateBuilder<TDataType, TButtonType> ConfigureDrawHandler(Action<TDataType, TButtonType> drawHandler)
    {
        DrawHandler = drawHandler;
        return this;
    }

    public IDelegateBuilder<TDataType, TButtonType> ConfigureFocusEnteredHandler(Action<TDataType, TButtonType> focusEnteredHandler)
    {
        FocusEnteredHandler = focusEnteredHandler;
        return this;
    }

    public IDelegateBuilder<TDataType, TButtonType> ConfigureFocusExitedHandler(Action<TDataType, TButtonType> focusExitedHandler)
    {
        FocusExitedHandler = focusExitedHandler;
        return this;
    }

    public IDelegateBuilder<TDataType, TButtonType> ConfigurePressedHandler(Action<TDataType, TButtonType> pressedHandler)
    {
        PressedHandler = pressedHandler;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType> WithArgument(PackedScene itemPrefab, Control itemContainer, IInfiniteLayoutGrid layoutGrid) => new FinishingArgumentBuilder<TDataType, TButtonType>(this, itemPrefab, itemContainer, layoutGrid);
}