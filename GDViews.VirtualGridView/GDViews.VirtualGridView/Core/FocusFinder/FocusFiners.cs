using Godot;

namespace GodotViews.VirtualGrid;

public static partial class FocusFiners
{
    public static readonly IViewFocusFinder<Vector2I> ViewPosition = new ViewBFSFocusFinder();
    public static readonly IEqualityDataFocusFinder Value = new DataEqualityFocusFinder();
    public static readonly IPredicateDataFocusFinder Predicate = new DataPredicateFocusFinder();
    public static readonly IDataFocusFinder<Vector2I> DataPosition = new DataBFSFocusFinder();
}