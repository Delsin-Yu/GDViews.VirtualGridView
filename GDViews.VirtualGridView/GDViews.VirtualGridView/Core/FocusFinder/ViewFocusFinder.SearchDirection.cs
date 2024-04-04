using System;
using Godot;

namespace GodotViews.Core.FocusFinder;

public readonly struct SearchDirection
{
    private readonly Vector2I[] _backing;

    internal SearchDirection(Vector2I[] backing)
    {
        _backing = backing;
    }

    internal ReadOnlySpan<Vector2I> GetSpan() => _backing.AsSpan();
}