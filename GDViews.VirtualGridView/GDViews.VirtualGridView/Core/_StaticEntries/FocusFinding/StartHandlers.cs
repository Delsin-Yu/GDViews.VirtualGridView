using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Provides a set of predefined start handlers for obtaining the start position of the focus finding logic.
/// </summary>
public static partial class StartHandlers
{
    /// <summary>
    /// Gets the start position from the datasets coordinate by the specified dataset position. 
    /// </summary>
    public static readonly IDataStartHandler<Vector2I> DataPosition = new DataImpl();
    
    /// <summary>
    /// Gets the start position from the viewport coordinate by the specified viewport position.
    /// </summary>
    public static readonly IViewStartHandler<Vector2I> ViewPosition = new ViewImpl();
    
    /// <summary>
    /// Get the start position from the viewport coordinate with the specified offset.
    /// </summary>
    public static readonly IViewStartHandler<Vector2I> ViewCenter = new ViewCenterImpl();
}