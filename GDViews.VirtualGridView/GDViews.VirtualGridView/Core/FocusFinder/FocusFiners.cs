using Godot;

namespace GodotViews.VirtualGrid;


public static partial class FocusFiners
{
    public static IViewFocusFinder<Vector2I> ViewPosition { get; } = new ViewBFSFocusFinder();
    public static IEqualityDataFocusFinder Value { get; } = new DataEqualityFocusFinder();
    public static IPredicateDataFocusFinder Predicate { get; } = new DataPredicateFocusFinder();
    public static IDataFocusFinder<Vector2I> DataPosition { get; } = new DataBFSFocusFinder();
}