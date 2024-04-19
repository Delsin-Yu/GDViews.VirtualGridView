using Godot;

namespace GodotViews.VirtualGrid.FocusFinding;

/// <summary>
/// Provides a set of predefined focus finders for handling the focus finding logic.
/// </summary>
public static partial class FocusFinders
{
    /// <summary>
    /// Try to find a valid grid element as the focus candidate by the specified viewport position.  
    /// </summary>
    public static readonly IViewFocusFinder<Vector2I> ViewPosition = new ViewBFSImpl();
    
    /// <summary>
    /// Try to find a valid grid element as the focus candidate by the specified datasets position.
    /// </summary>
    public static readonly IDataFocusFinder<Vector2I> DataPosition = new DataBFSImpl();
    
    /// <summary>
    /// Try to find a valid grid element as the focus candidate by the specified value equality. 
    /// </summary>
    public static readonly IEqualityDataFocusFinder Value = new DataEqualityImpl();
    
    /// <summary>
    /// Try to find a valid grid element as the focus candidate by the specified predicate.
    /// </summary>
    public static readonly IPredicateDataFocusFinder Predicate = new DataPredicateImpl();
}