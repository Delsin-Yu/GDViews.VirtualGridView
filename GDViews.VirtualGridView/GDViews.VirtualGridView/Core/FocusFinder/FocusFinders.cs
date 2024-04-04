using System;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public static partial class FocusFinders
{
    public static IViewFocusFinder ByPosition { get; } = new BFSViewFocusFinder();
    public static IEqualityDataFocusFinder ByValue { get; } = new EqualityDataFocusFinder();
    public static IPredicateDataFocusFinder ByPredicate { get; } = new PredicateDataFocusFinder();
}