using Godot;

namespace GodotViews.VirtualGrid.FocusFinding;

/// <summary>
/// Data Start Handler is responsible for resolving the start position from the datasets when finding focus.
/// </summary>
public interface IDataStartHandler<in TArgument>
{
    /// <summary>
    /// Resolves the start position relative to the datasets.
    /// </summary>
    /// <param name="currentView">Provides assess to the current datasets.</param>
    /// <param name="argument">An extra argument passes to the handler.</param>
    /// <typeparam name="TDataType">The type for the data <see cref="IVirtualGridView{TDataType}"/> focuses on.</typeparam>
    /// <returns>The resolved start position, relative to the datasets position.</returns>
    Vector2I ResolveStartPosition<TDataType>(ref readonly ReadOnlyDataArray<TDataType> currentView, TArgument argument);
}