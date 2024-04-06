using System;
using System.Runtime.CompilerServices;
using Godot;

namespace GodotViews.VirtualGrid;

internal static class DelegateRunner
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
        try
        {
            return call.Invoke(arg1, arg2);
        }
        catch (Exception e)
        {
            ReportException(e, actionName, targetName, methodName);
            return default;
        }
    }
    
    internal static bool RunProtected<T1, T2, T3>(Action<T1, T2?, T3?>? call, in T1 arg1, in T2? arg2, in T3? arg3, string actionName, string targetName, [CallerArgumentExpression(nameof(call))] string? methodName = null)
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

    internal static void ReportException(Exception e, string actionName, string targetName, string? methodName)
    {
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
}