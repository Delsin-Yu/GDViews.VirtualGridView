using System;
using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public static partial class FocusBy
{
    public static IViewFocusFinder<Vector2I> View { get; } = new BFSViewFocusFinder();
    public static IEqualityDataFocusFinder DataSetValue { get; } = new EqualityDataFocusFinder();
    public static IPredicateDataFocusFinder DataSetPredicate { get; } = new PredicateDataFocusFinder();
    public static IDataFocusFinder<Vector2I> ByDataSetPosition { get; } = new BFSDataSetFocusFinder();
}