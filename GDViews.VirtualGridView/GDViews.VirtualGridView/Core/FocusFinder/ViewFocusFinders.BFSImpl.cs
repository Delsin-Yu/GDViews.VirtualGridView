using System;
using System.Collections.Generic;
using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public delegate Vector2I StartPositionHandler(ref readonly ReadOnly2DArray currentView);

public static partial class ViewFocusFinders
{
    internal static class BFSSearch
    {
        private static bool IsValid(
            ref readonly ReadOnly2DArray currentView,
            ref readonly Vector2I candidate,
            ref int rowIndex,
            ref int columnIndex
        )
        {
            if (currentView[candidate.X, candidate.Y])
            {
                rowIndex = candidate.X;
                columnIndex = candidate.Y;
                return true;
            }

            return false;
        }

        private static readonly Queue<Vector2I> _pending = [];
        private static readonly HashSet<Vector2I> _visited = [];

        public static bool BFSCore(ref readonly Vector2I start, ref readonly ReadOnly2DArray currentView, ref readonly ReadOnlySpan<Vector2I> neighborOffsetCollection, out int rowIndex, out int columnIndex)
        {
            var result = BFSCoreImpl(in start, in currentView, in neighborOffsetCollection, out rowIndex, out columnIndex);
            _pending.Clear();
            _visited.Clear();
            return result;
        }

        private static bool BFSCoreImpl(
            ref readonly Vector2I start,
            ref readonly ReadOnly2DArray currentView,
            ref readonly ReadOnlySpan<Vector2I> neighborOffsetCollection,
            out int rowIndex,
            out int columnIndex
        )
        {
            rowIndex = -1;
            columnIndex = -1;

            if (IsValid(in currentView, in start, ref rowIndex, ref columnIndex)) return true;

            _pending.Enqueue(start);

            while (_pending.TryDequeue(out var candidate))
            {
                _visited.Add(candidate);

                foreach (var neighborOffset in neighborOffsetCollection)
                {
                    var neighborPosition = candidate + neighborOffset;
                    if (_visited.Contains(neighborPosition)) continue;
                    if (IsValid(in currentView, in neighborPosition, ref rowIndex, ref columnIndex)) return true;
                    _pending.Enqueue(neighborPosition);
                }
            }

            return false;
        }
    }


    internal class BFSViewFocusFinder : IViewFocusFinder
    {
        public bool TryResolveFocus(
            ref readonly ReadOnly2DArray currentView,
            ref readonly ReadOnlySpan<Vector2I> searchDirection,
            StartPositionHandler startPositionHandler,
            out int rowIndex,
            out int columnIndex)
        {
            var start = startPositionHandler(in currentView);
            return BFSSearch.BFSCore(
                in start,
                in currentView,
                in searchDirection,
                out rowIndex,
                out columnIndex
            );
        }
    }
}