using System;
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
    
    internal static readonly StringName _uiUp = "ui_up";
    internal static readonly StringName _uiDown = "ui_down";
    internal static readonly StringName _uiLeft = "ui_left";
    internal static readonly StringName _uiRight = "ui_right";

    internal static object? CurrentActiveGridView { private get; set; }

    internal static void SimulateMouseDragNavigation(
        InputEvent inputEvent,
        ref Vector2? mouseStartDragPosition,
        ref readonly Vector2 objectDistance,
        object? gridViewOwner,
        out MoveDirection? simulatedDirection
    )
    {
        simulatedDirection = null;

        if (gridViewOwner is null || gridViewOwner != CurrentActiveGridView || mouseStartDragPosition is null) return;

        Vector2 position;
        if (inputEvent is InputEventMouseMotion { Pressure: > 0f } mouseMotion) position = mouseMotion.GlobalPosition;
        else if (inputEvent is InputEventScreenDrag screenDrag) position = screenDrag.Position;
        else return;
        
        var startDragPosition = mouseStartDragPosition.Value;
        var mouseTravelDistance = startDragPosition - position;
        var sign = mouseTravelDistance.Sign();
        var absDistance = mouseTravelDistance.Abs();
        var diff = absDistance - objectDistance;

        if (diff.X > 0)
        {
            mouseStartDragPosition += new Vector2(objectDistance.X * -sign.X, 0);
            simulatedDirection = sign.X > 0 ? MoveDirection.Right : MoveDirection.Left;
        }
        else if (diff.Y > 0)
        {
            mouseStartDragPosition += new Vector2(0, objectDistance.Y * -sign.Y);
            simulatedDirection = sign.Y > 0 ? MoveDirection.Down : MoveDirection.Up;
        }
    }

    internal static void TryApplyInputSimulation(MoveDirection moveDirection)
    {
        var eventName = (moveDirection) switch
        {
            MoveDirection.Up => _uiUp,
            MoveDirection.Down => _uiDown,
            MoveDirection.Left => _uiLeft,
            MoveDirection.Right => _uiRight,
            _ => throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null)
        };
        Input.ParseInputEvent(new InputEventAction { Pressed = true, Action = eventName });
    }


    internal static void SimulateScrollWheelNavigation(
        InputEvent inputEvent,
        ref Vector2? mouseStartDragPosition,
        object? gridViewOwner,
        out MoveDirection? simulatedMoveDirection
    )
    {
        simulatedMoveDirection = null;
        if (gridViewOwner is null || gridViewOwner != CurrentActiveGridView) return;

        if (inputEvent is not InputEventMouseButton mouseButton) return;
        
        var mouseButtonButtonIndex = mouseButton.ButtonIndex;

        if (!mouseButton.Pressed)
        {
            if (mouseButtonButtonIndex == MouseButton.Left) mouseStartDragPosition = null;
            return;
        }

        var mapVH = mouseButton.GetModifiersMask().HasFlag(KeyModifierMask.MaskShift);

        switch (mouseButtonButtonIndex)
        {
            case MouseButton.WheelUp:
                simulatedMoveDirection = mapVH ? MoveDirection.Left : MoveDirection.Up;
                break;
            case MouseButton.WheelDown:
                simulatedMoveDirection = mapVH ? MoveDirection.Right : MoveDirection.Down;
                break;
            case MouseButton.WheelLeft:
                simulatedMoveDirection = MoveDirection.Left;
                break;
            case MouseButton.WheelRight:
                simulatedMoveDirection = MoveDirection.Right;
                break;
            case MouseButton.Left:
                mouseStartDragPosition = mouseButton.GlobalPosition;
                break;
        }
    }
}