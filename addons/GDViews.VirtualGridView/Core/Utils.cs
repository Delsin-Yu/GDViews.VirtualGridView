using System;
using System.Runtime.CompilerServices;
using Godot;
using GodotViews.VirtualGrid.Builder;

namespace GodotViews.VirtualGrid;

static class Utils
{
    internal static readonly StringName UIUp = "ui_up";
    internal static readonly StringName UIDown = "ui_down";
    internal static readonly StringName UILeft = "ui_left";
    internal static readonly StringName UIRight = "ui_right";

    internal static object? CurrentActiveGridView { get; set; }

    internal static bool TryGetMoveDirection(ref Vector2I vector, out Vector2I moveDirection)
    {
        moveDirection = Vector2I.Zero;
        if (vector == Vector2I.Zero) return false;

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

    internal static bool SearchForData<TDataType, TMatchArgument>(
        IDataInspector<TDataType> dataInspector,
        int viewXCount,
        int viewYCount,
        out ViewData viewData,
        Func<TDataType, TMatchArgument, bool> comparer,
        TMatchArgument matchArgument
    )
    {
        dataInspector.GetDataSetCurrentMetrics(out var xs, out var ys);

        for (var yOffset = 0; yOffset < ys; yOffset += viewYCount)
        for (var xOffset = 0; xOffset < xs; xOffset += viewXCount)
        {
            var maxSpan = Mathf.Min(viewXCount, xs - xOffset);

            for (var yIndex = 0; yIndex + yOffset < ys; yIndex++)
            {
                var xSpan = dataInspector.InspectViewX(yIndex, xOffset, yOffset);

                for (var xIndex = 0; xIndex < maxSpan; xIndex++)
                {
                    var cellData = xSpan[xIndex];
                    if (!cellData.TryUnwrap(out var cellDataValue)) continue;
                    if (!DelegateRunner.RunProtected(comparer, cellDataValue, matchArgument, "Predicate", "Data Focus Finding")) continue;
                    viewData = new(yIndex, xOffset, yOffset, xIndex);
                    return true;
                }
            }
        }

        viewData = default;
        return false;
    }

    internal record struct ViewData(int YIndex, int XOffset, int YOffset, int XIndex);
}

static class DelegateRunner
{
    internal static bool RunProtected<T>(Action<T>? call, in T arg, string actionName, string targetName, [CallerArgumentExpression(nameof(call))] string? methodName = null)
    {
        try
        {
            call?.Invoke(arg);
            return true;
        }
        catch (Exception e)
        {
            ReportException(e, actionName, targetName, methodName);
            return false;
        }
    }

    internal static TR? RunProtected<T1, T2, TR>(Func<T1, T2, TR> call, in T1 arg1, in T2 arg2, string actionName, string targetName, [CallerArgumentExpression(nameof(call))] string? methodName = null)
    {
        try { return call.Invoke(arg1, arg2); }
        catch (Exception e)
        {
            ReportException(e, actionName, targetName, methodName);
            return default;
        }
    }

    internal static bool RunProtected<T1, T2, T3>(Action<T1, T2, T3>? call, in T1 arg1, in T2 arg2, in T3 arg3, string actionName, string targetName, [CallerArgumentExpression(nameof(call))] string? methodName = null)
    {
        try
        {
            call?.Invoke(arg1, arg2, arg3);
            return true;
        }
        catch (Exception e)
        {
            ReportException(e, actionName, targetName, methodName);
            return false;
        }
    }

    internal static bool RunProtected(Action? call, string actionName, string targetName, [CallerArgumentExpression(nameof(call))] string? methodName = null)
    {
        try
        {
            call?.Invoke();
            return true;
        }
        catch (Exception e)
        {
            ReportException(e, actionName, targetName, methodName);
            return false;
        }
    }

    internal static void ReportException(Exception e, string actionName, string targetName, string? methodName) =>
        GD.PushError(
            $"""

             ┌┈┈┈┈ {actionName} Error ┈┈┈┈
             │ {e.GetType().Name} on {targetName}.{methodName ?? "UnknownFunction"}
             │ Message:
             │   {e.Message}
             └┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈
             {e.StackTrace}
             """
        );
}