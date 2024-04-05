using System;
using System.Collections.Generic;
using Godot;

namespace GodotViews.VirtualGrid;

public static class VirtualGridView
{
    public static IViewHandlerBuilder Create(int viewportColumns, int viewportRows) => 
        new ViewHandlerBuilder(viewportColumns, viewportRows);
    
    internal static bool TryGetMoveDirection(ref Vector2I vector, out Vector2I moveDirection)
    {
        moveDirection = Vector2I.Zero;
        if (vector == Vector2I.Zero)
        {
            return false;
        }
        
        switch (vector.X)
        {
            case > 0:
                moveDirection.X = -1;
                vector.X -= 1;
                return true;
            case < 0:
                moveDirection.X = 1;
                vector.X += 1;
                return true;
        }
        switch (vector.Y)
        {
            case > 0:
                moveDirection.Y = -1;
                vector.Y -= 1;
                return true;
            case < 0:
                moveDirection.Y = 1;
                vector.Y += 1;
                return true;
        }

        return true;
    }
    
    internal static Vector2I CreatePosition(int rowIndex, int columnIndex) => new(columnIndex, rowIndex);
    
    internal static readonly StringName _uiUp = "ui_up";
    internal static readonly StringName _uiDown = "ui_down";
    internal static readonly StringName _uiLeft = "ui_left";
    internal static readonly StringName _uiRight = "ui_right";

    internal static object? CurrentActiveGridView { get; set; }
    internal record struct ViewData(int RowIndex, int ColumnOffset, int RowOffset, int ColumnIndex);

    internal static bool SearchForData<TDataType, TMatchArgument>(IDataInspector<TDataType> dataInspector, int viewRows, int viewColumns, out ViewData viewData, Func<TDataType, TMatchArgument, bool> comparer, TMatchArgument matchArgument)
    {
        dataInspector.GetDataSetCurrentMetrics(out var rows, out var columns);
        for (var rowOffset = 0; rowOffset < rows; rowOffset += viewRows)
        for (var columnOffset = 0; columnOffset < columns; columnOffset += viewColumns)
        {
            var maxSpan = Mathf.Min(viewColumns, columns - columnOffset);
            for (var rowIndex = 0; rowIndex + rowOffset < rows; rowIndex++)
            {
                var columnSpan = dataInspector.InspectViewColumn(rowIndex, columnOffset, rowOffset);
                for (var columnIndex = 0; columnIndex < maxSpan; columnIndex++)
                {
                    var cellData = columnSpan[columnIndex];
                    if (!cellData.TryUnwrap(out var cellDataValue)) continue;
                    if (!DelegateRunner.RunProtected(comparer, cellDataValue, matchArgument, "Predicate", "Data Focus Finding")) continue;
                    viewData = new(rowIndex, columnOffset, rowOffset, columnIndex);
                    return true;
                }
            }
        }

        viewData = default;
        return false;
    }
}