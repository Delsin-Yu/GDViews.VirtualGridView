using System;
using Godot;
using Range = Godot.Range;

namespace GodotViews.VirtualGrid.Examples.Playground;

public static class DataBindingUtility
{
    public static void Bind<TObject>(Range range, Action<float, TObject> valueChangedHandler, float defaultValue, TObject obj)
    {
        range.ValueChanged += value => valueChangedHandler((float)value, obj);
        range.SetValueNoSignal(defaultValue);
    }

    public static void Bind<TObject>(Button button, Action<TObject> onPressedHandler, TObject obj) => button.Pressed += () => onPressedHandler(obj);

    public static void Bind<T, TObject>((string, T)[] list, OptionButton optionButton, Action<T, TObject> setValueHandler, int defaultSelection, TObject obj)
    {
        foreach (var (name, _) in list) optionButton.AddItem(name);
        optionButton.ItemSelected += index => setValueHandler(list[index].Item2, obj);
        optionButton.Selected = defaultSelection;
    }

    public static void Bind<TObject>(CheckButton button, Action<bool, TObject> onToggleHandler, bool defaultValue, TObject obj)
    {
        button.Toggled += value => onToggleHandler(value, obj);
        button.ButtonPressed = defaultValue;
    }
}