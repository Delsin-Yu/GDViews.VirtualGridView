using Godot;

namespace GodotViews.VirtualGrid;

public static class VirtualGridView
{
    public static IViewHandlerBuilder Create(int viewportColumns, int viewportRows) => 
        new ViewHandlerBuilder(viewportColumns, viewportRows);
    
    internal static bool TryGetMoveDirection(ref Vector2I vector, out Vector2I moveDirection)
    {
        moveDirection = Vector2I.Zero;
        if (vector == Vector2I.Zero)
        {
            return false;
        }
        
        switch (vector.X)
        {
            case > 0:
                moveDirection.X = -1;
                vector.X -= 1;
                return true;
            case < 0:
                moveDirection.X = 1;
                vector.X += 1;
                return true;
        }
        switch (vector.Y)
        {
            case > 0:
                moveDirection.Y = -1;
                vector.Y -= 1;
                return true;
            case < 0:
                moveDirection.Y = 1;
                vector.Y += 1;
                return true;
        }

        return true;
    }
    
    internal static Vector2I CreatePosition(int rowIndex, int columnIndex) => new(columnIndex, rowIndex);
    
    private static readonly StringName _uiUp = "ui_up";
    private static readonly StringName _uiDown = "ui_down";
    private static readonly StringName _uiLeft = "ui_left";
    private static readonly StringName _uiRight = "ui_right";

    internal static object? CurrentActiveGridView { private get; set; }
    
    internal static bool SimulateScrollWheelNavigation(InputEvent inputEvent, object? gridViewOwner)
    {
        if (gridViewOwner is null || gridViewOwner != CurrentActiveGridView) return false;
        
        if (inputEvent is not InputEventMouseButton mouseButton) return false;

        if (!mouseButton.Pressed) return false;

        var mapVH =
            mouseButton
                .GetModifiersMask()
                .HasFlag(KeyModifierMask.MaskShift);

        var actionName = mouseButton.ButtonIndex switch
        {
            MouseButton.WheelUp => mapVH ? _uiLeft : _uiUp,
            MouseButton.WheelDown => mapVH ? _uiRight : _uiDown,
            MouseButton.WheelLeft => _uiLeft,
            MouseButton.WheelRight => _uiRight,
            _ => null
        };

        if (actionName is null) return false;

        Input.ParseInputEvent(new InputEventAction { Pressed = true, Action = actionName });
        
        return true;
    }
}