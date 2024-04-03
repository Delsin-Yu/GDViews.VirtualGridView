using System;
using System.Collections.Generic;
using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public static class ViewFocusFinders
{
    public static class Directional
    {
        public static IViewFocusFinder TopLeft { get; } = new TopLeftImpl();
        public static IViewFocusFinder TopRight { get; } = new TopRightImpl();
        public static IViewFocusFinder BottomLeft { get; } = new BottomLeftImpl();
        public static IViewFocusFinder BottomRight { get; } = new BottomRightImpl();
        public static IViewFocusFinder LeftTop { get; } = new LeftTopImpl();
        public static IViewFocusFinder RightTop { get; } = new RightTopImpl();
        public static IViewFocusFinder LeftBottom { get; } = new LeftBottomImpl();
        public static IViewFocusFinder RightBottom { get; } = new RightBottomImpl();


        private class TopLeftImpl : TopLeftImplCore
        {
            protected override void GetNeighborOffsets(out Vector2I first, out Vector2I second)
            {
                first = new(0, 1);
                second = new(1, 0);
            }
        }

        private class TopRightImpl : TopRightImplCore
        {
            protected override void GetNeighborOffsets(out Vector2I first, out Vector2I second)
            {
                first = new(0, -1);
                second = new(1, 0);
            }
        }

        private class BottomLeftImpl : BottomLeftImplCore
        {
            protected override void GetNeighborOffsets(out Vector2I first, out Vector2I second)
            {
                first = new(0, 1);
                second = new(-1, 0);
            }
        }

        private class BottomRightImpl : BottomRightImplCore
        {
            protected override void GetNeighborOffsets(out Vector2I first, out Vector2I second)
            {
                first = new(0, -1);
                second = new(-1, 0);
            }
        }

        private class LeftTopImpl : TopLeftImplCore
        {
            protected override void GetNeighborOffsets(out Vector2I first, out Vector2I second)
            {
                first = new(1, 0);
                second = new(0, 1);
            }
        }

        private class RightTopImpl : TopRightImplCore
        {
            protected override void GetNeighborOffsets(out Vector2I first, out Vector2I second)
            {
                first = new(1, 0);
                second = new(0, -1);
            }
        }

        private class LeftBottomImpl : BottomLeftImplCore
        {
            protected override void GetNeighborOffsets(out Vector2I first, out Vector2I second)
            {
                first = new(-1, 0);
                second = new(0, 1);
            }
        }

        private class RightBottomImpl : BottomRightImplCore
        {
            protected override void GetNeighborOffsets(out Vector2I first, out Vector2I second)
            {
                first = new(-1, 0);
                second = new(0, -1);
            }
        }

        private abstract class TopLeftImplCore : BFSImpl
        {
            protected override Vector2I GetStart(ref readonly ReadOnly2DArray currentView) => Vector2I.Zero;
        }

        private abstract class TopRightImplCore : BFSImpl
        {
            protected override Vector2I GetStart(ref readonly ReadOnly2DArray currentView) => new(0, currentView.ViewColumns - 1);
        }

        private abstract class BottomLeftImplCore : BFSImpl
        {
            protected override Vector2I GetStart(ref readonly ReadOnly2DArray currentView) => new(currentView.ViewRows - 1, 0);
        }

        private abstract class BottomRightImplCore : BFSImpl
        {
            protected override Vector2I GetStart(ref readonly ReadOnly2DArray currentView) => new(currentView.ViewRows - 1, currentView.ViewColumns - 1);
        }

        private abstract class BFSImpl : IViewFocusFinder
        {
            private readonly Queue<Vector2I> _pending = [];
            private readonly HashSet<Vector2I> _visited = [];

            protected abstract Vector2I GetStart(ref readonly ReadOnly2DArray currentView);
            protected abstract void GetNeighborOffsets(out Vector2I first, out Vector2I second);

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

            public bool TryResolveFocus(ref readonly ReadOnly2DArray currentView, out int rowIndex, out int columnIndex)
            {
                var result = TryResolveFocusImpl(in currentView, out rowIndex, out columnIndex);
                _pending.Clear();
                _visited.Clear();
                return result;
            }

            private bool TryResolveFocusImpl(ref readonly ReadOnly2DArray currentView, out int rowIndex, out int columnIndex)
            {
                rowIndex = -1;
                columnIndex = -1;

                var start = GetStart(in currentView);

                if (IsValid(in currentView, in start, ref rowIndex, ref columnIndex)) return true;

                GetNeighborOffsets(
                    out var first,
                    out var second
                );

                ReadOnlySpan<Vector2I> neighborOffsetsCollection = [first, second];

                _pending.Enqueue(start);

                while (_pending.TryDequeue(out var candidate))
                {
                    _visited.Add(candidate);

                    foreach (var neighborOffset in neighborOffsetsCollection)
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
    }
}