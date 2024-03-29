﻿using System;
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