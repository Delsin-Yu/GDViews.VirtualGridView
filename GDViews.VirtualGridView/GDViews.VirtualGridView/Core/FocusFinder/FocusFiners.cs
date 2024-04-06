using System;
using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;


public static partial class FocusFiners
{
    public static IViewFocusFinder<Vector2I> ViewPosition { get; } = new BFSViewFocusFinder();
    public static IEqualityDataFocusFinder Value { get; } = new EqualityDataFocusFinder();
    public static IPredicateDataFocusFinder Predicate { get; } = new PredicateDataFocusFinder();
    public static IDataFocusFinder<Vector2I> DataPosition { get; } = new BFSDataSetFocusFinder();
}