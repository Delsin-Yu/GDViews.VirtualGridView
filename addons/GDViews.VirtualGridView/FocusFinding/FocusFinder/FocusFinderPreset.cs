using Godot;

namespace GodotViews.VirtualGrid.FocusFinding;

/// <summary>
/// A focus finder preset contains a set of predefined combinations 
/// of arguments to simplify the arguments required to pass into
/// the <see cref="IVirtualGridView{TDataType}"/>
/// for the developers.  
/// </summary>
public record struct DataFocusFinderPreset(
    DataFocusFinderPreset<Vector2I> Preset,
    Vector2I Argument,
    SearchDirection SearchDirection
);

/// <inheritdoc cref="DataFocusFinderPreset"/>
public record struct DataFocusFinderPreset<TArgument>(
    IDataFocusFinder<TArgument> FocusFinder,
    IDataStartHandler<TArgument> StartHandler
);

/// <inheritdoc cref="DataFocusFinderPreset"/>
public record struct ViewFocusFinderPreset(
    ViewFocusFinderPreset<Vector2I> Preset,
    Vector2I Argument,
    SearchDirection SearchDirection
);

/// <inheritdoc cref="DataFocusFinderPreset"/>
public record struct ViewFocusFinderPreset<TArgument>(
    IViewFocusFinder<TArgument> FocusFinder,
    IViewStartHandler<TArgument> StartHandler
);