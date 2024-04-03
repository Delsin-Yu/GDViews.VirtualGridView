using System;
using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;

public static class ViewFocusFinders
{
    public static class Directional
    {
        public static IViewFocusFinder TopLeft { get; } = new TopLeftImpl();
        public static IViewFocusFinder TopRight { get; }
        public static IViewFocusFinder BottomLeft { get; }
        public static IViewFocusFinder BottomRight { get; }
        public static IViewFocusFinder LeftTop { get; } = new LeftTopImpl();
        public static IViewFocusFinder RightTop { get; }
        public static IViewFocusFinder LeftBottom { get; }
        public static IViewFocusFinder RightBottom { get; }

        private class TopLeftImpl : IViewFocusFinder
        {
            public bool TryResolveFocus(ReadOnly2DArray currentView, out int rowIndex, out int columnIndex)
            {
                var maxRowIndex = currentView.ViewRows - 1;
                var passCount = currentView.ViewRows + currentView.ViewColumns - 1;
                for (var i = 0; i < passCount; i++)
                {
                    var x = Math.Min(i, maxRowIndex);
                    var y = i - x;
                    for (; x >= 0 && y < currentView.ViewColumns; x--, y++)
                    {
                        if (currentView[x, y])
                        {
                            rowIndex = y;
                            columnIndex = x;
                            return true;
                        }
                    }
                }

                rowIndex = -1;
                columnIndex = -1;
                return false;
            }
        }

        private class LeftTopImpl : IViewFocusFinder
        {
            public bool TryResolveFocus(ReadOnly2DArray currentView, out int rowIndex, out int columnIndex)
            {
                var maxColumnIndex = currentView.ViewColumns - 1;
                var passCount = currentView.ViewRows + currentView.ViewColumns - 1;
                for (var i = 0; i < passCount; i++)
                {
                    var y = Math.Min(i, maxColumnIndex);
                    var x = i - y;
                    for (; y >= 0 && x < currentView.ViewRows; x++, y--)
                    {
                        if (currentView[x, y])
                        {
                            rowIndex = y;
                            columnIndex = x;
                            return true;
                        }
                    }
                }

                rowIndex = -1;
                columnIndex = -1;
                return false;
            }
        }
    }
}