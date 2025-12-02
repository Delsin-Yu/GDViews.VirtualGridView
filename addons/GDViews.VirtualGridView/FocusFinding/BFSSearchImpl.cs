using System;
using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid.FocusFinding;

public static partial class FocusFinders
{
    internal static class BFSSearch
    {
        private static readonly Queue<Vector2I> _pending = new(256);
        private static readonly HashSet<Vector2I> _visited = new(256);

        private static bool IsValid(
            ReadOnlyViewArray currentView,
            Vector2I candidate,
            ref int xIndex,
            ref int yIndex)
        {
            if (currentView[candidate.X, candidate.Y])
            {
                xIndex = candidate.X;
                yIndex = candidate.Y;
                return true;
            }

            return false;
        }

        private static bool IsValid<TDataType>(
            ReadOnlyDataArray<TDataType> currentView,
            Vector2I candidate,
            ref int xIndex,
            ref int yIndex)
        {
            if (currentView.TryGetData(candidate.X, candidate.Y, out _))
            {
                xIndex = candidate.X;
                yIndex = candidate.Y;
                return true;
            }

            return false;
        }

        public static bool BFSCore(
            Vector2I start,
            ReadOnlyViewArray currentView,
            ReadOnlySpan<Vector2I> neighborOffsetCollection,
            out int xIndex,
            out int yIndex)
        {
            var result = BFSCoreImpl(start, currentView, neighborOffsetCollection, out xIndex, out yIndex);
            _pending.Clear();
            _visited.Clear();
            return result;
        }

        private static bool BFSCoreImpl(
            Vector2I start,
            ReadOnlyViewArray currentView,
            ReadOnlySpan<Vector2I> neighborOffsetCollection,
            out int xIndex,
            out int yIndex
        )
        {
            xIndex = -1;
            yIndex = -1;

            if (IsValid(currentView, start, ref xIndex, ref yIndex)) return true;

            _pending.Enqueue(start);

            while (_pending.TryDequeue(out var candidate))
            {
                _visited.Add(candidate);

                foreach (var neighborOffset in neighborOffsetCollection)
                {
                    var neighborPosition = candidate + neighborOffset;
                    if (_visited.Contains(neighborPosition)) continue;

                    if (neighborPosition.X < 0 || neighborPosition.X >= currentView.ViewYCount || neighborPosition.Y < 0 || neighborPosition.Y >= currentView.ViewXCount)
                        continue;

                    if (IsValid(currentView, neighborPosition, ref xIndex, ref yIndex)) return true;
                    _pending.Enqueue(neighborPosition);
                }
            }

            return false;
        }

        public static bool BFSCore<TDataType>(
            Vector2I start,
            ReadOnlyDataArray<TDataType> currentView,
            ReadOnlySpan<Vector2I> neighborOffsetCollection,
            out int xIndex,
            out int yIndex)
        {
            var result = BFSCoreImpl(start, currentView, neighborOffsetCollection, out xIndex, out yIndex);
            _pending.Clear();
            _visited.Clear();
            return result;
        }

        private static bool BFSCoreImpl<TDataType>(
            Vector2I start,
            ReadOnlyDataArray<TDataType> currentView,
            ReadOnlySpan<Vector2I> neighborOffsetCollection,
            out int xIndex,
            out int yIndex)
        {
            xIndex = -1;
            yIndex = -1;

            if (IsValid(currentView, start, ref xIndex, ref yIndex)) return true;

            _pending.Enqueue(start);

            while (_pending.TryDequeue(out var candidate))
            {
                _visited.Add(candidate);

                foreach (var neighborOffset in neighborOffsetCollection)
                {
                    var neighborPosition = candidate + neighborOffset;
                    if (_visited.Contains(neighborPosition)) continue;

                    if (neighborPosition.X < 0 || neighborPosition.X >= currentView.DataSetXCount || neighborPosition.Y < 0 || neighborPosition.Y >= currentView.DataSetYCount)
                        continue;

                    if (IsValid(currentView, neighborPosition, ref xIndex, ref yIndex)) return true;
                    _pending.Enqueue(neighborPosition);
                }
            }

            return false;
        }
    }
}