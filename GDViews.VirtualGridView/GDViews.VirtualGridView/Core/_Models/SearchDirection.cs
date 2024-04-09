using System;
using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Represents the search direction passed to the <see cref="FocusFinders"/>.
/// </summary>
public readonly struct SearchDirection
{
    private readonly Vector2I[] _backing;

    /// <summary>
    /// Construct an instance of the <see cref="SearchDirection"/>
    /// </summary>
    /// <param name="backing">An array of directions the focus finder is meant to query through.</param>
    /// <remarks>Developers are highly recommended to only use the predefined contents from the <see cref="SearchDirections"/> class, that is:
    /// <list type="bullet">
    /// <item><see cref="SearchDirections.SearchUp"/></item>
    /// <item><see cref="SearchDirections.SearchDown"/></item>
    /// <item><see cref="SearchDirections.SearchLeft"/></item>
    /// <item><see cref="SearchDirections.SearchRight"/></item>
    /// </list></remarks>
    public SearchDirection(Vector2I[] backing)
    {
        _backing = backing;
    }

    internal ReadOnlySpan<Vector2I> GetSpan() => _backing.AsSpan();
}