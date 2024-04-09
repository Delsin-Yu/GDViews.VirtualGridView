using System;
using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid;

public static partial class FocusFinders
{
    private readonly struct MinimalVector2I
    {
        public readonly int X;
        public readonly int Y;

        public MinimalVector2I(Vector2I vector2I) : this(vector2I.X, vector2I.Y) { }

        public MinimalVector2I(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static MinimalVector2I operator +(MinimalVector2I a, Vector2I b) => new(a.X + b.X, a.Y + b.Y);

        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"<{X},{Y}>";
    }

    internal static class BFSSearch
    {
        private static readonly Queue<MinimalVector2I> _pending = new(256);
        private static readonly HashSet<MinimalVector2I> _visited = new(256);

        private static bool IsValid(
            ref readonly ReadOnlyViewArray currentView,
            ref readonly MinimalVector2I candidate,
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

        private static bool IsValid<TDataType>(
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            ref readonly MinimalVector2I candidate,
            ref int rowIndex,
            ref int columnIndex
        )
        {
            if (currentView.TryGetData(candidate.X, candidate.Y, out _))
            {
                rowIndex = candidate.X;
                columnIndex = candidate.Y;
                return true;
            }

            return false;
        }

        public static bool BFSCore(ref readonly Vector2I start, ref readonly ReadOnlyViewArray currentView, ref readonly ReadOnlySpan<Vector2I> neighborOffsetCollection, out int rowIndex, out int columnIndex)
        {
            var result = BFSCoreImpl(in start, in currentView, in neighborOffsetCollection, out rowIndex, out columnIndex);
            _pending.Clear();
            _visited.Clear();
            return result;
        }

        private static bool BFSCoreImpl(
            ref readonly Vector2I start,
            ref readonly ReadOnlyViewArray currentView,
            ref readonly ReadOnlySpan<Vector2I> neighborOffsetCollection,
            out int rowIndex,
            out int columnIndex
        )
        {
            rowIndex = -1;
            columnIndex = -1;

            var startVector = new MinimalVector2I(start);

            if (IsValid(in currentView, in startVector, ref rowIndex, ref columnIndex)) return true;

            _pending.Enqueue(startVector);

            while (_pending.TryDequeue(out var candidate))
            {
                _visited.Add(candidate);

                foreach (var neighborOffset in neighborOffsetCollection)
                {
                    var neighborPosition = candidate + neighborOffset;
                    if (_visited.Contains(neighborPosition)) continue;

                    if (neighborPosition.X < 0 ||
                        neighborPosition.X >= currentView.ViewRows ||
                        neighborPosition.Y < 0 ||
                        neighborPosition.Y >= currentView.ViewColumns)
                        continue;

                    if (IsValid(in currentView, in neighborPosition, ref rowIndex, ref columnIndex)) return true;
                    _pending.Enqueue(neighborPosition);
                }
            }

            return false;
        }

        public static bool BFSCore<TDataType>(ref readonly Vector2I start, ref readonly ReadOnlyDataArray<TDataType> currentView, ref readonly ReadOnlySpan<Vector2I> neighborOffsetCollection, out int rowIndex, out int columnIndex)
        {
            var result = BFSCoreImpl(in start, in currentView, in neighborOffsetCollection, out rowIndex, out columnIndex);
            _pending.Clear();
            _visited.Clear();
            return result;
        }

        private static bool BFSCoreImpl<TDataType>(
            ref readonly Vector2I start,
            ref readonly ReadOnlyDataArray<TDataType> currentView,
            ref readonly ReadOnlySpan<Vector2I> neighborOffsetCollection,
            out int rowIndex,
            out int columnIndex
        )
        {
            rowIndex = -1;
            columnIndex = -1;

            var startVector = new MinimalVector2I(start);

            if (IsValid(in currentView, in startVector, ref rowIndex, ref columnIndex)) return true;

            _pending.Enqueue(startVector);

            while (_pending.TryDequeue(out var candidate))
            {
                _visited.Add(candidate);

                foreach (var neighborOffset in neighborOffsetCollection)
                {
                    var neighborPosition = candidate + neighborOffset;
                    if (_visited.Contains(neighborPosition)) continue;

                    if (neighborPosition.X < 0 ||
                        neighborPosition.X >= currentView.DataSetRows ||
                        neighborPosition.Y < 0 ||
                        neighborPosition.Y >= currentView.DataSetColumns)
                        continue;

                    if (IsValid(in currentView, in neighborPosition, ref rowIndex, ref columnIndex)) return true;
                    _pending.Enqueue(neighborPosition);
                }
            }

            return false;
        }
    }
}