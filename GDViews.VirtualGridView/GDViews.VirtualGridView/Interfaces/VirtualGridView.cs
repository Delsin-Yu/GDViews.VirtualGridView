using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Godot;

namespace GodotViews.VirtualGrid;

public enum MoveDirection
{
    Up,
    Down,
    Left,
    Right
}

public interface IDynamicGridViewer<T>
{
    int FixedMetric { get; }

    int GetDynamicMetric();

    bool TryGetGridElement(int fixedMetricIndex, int dynamicMetricIndex, [NotNullWhen(true)] out T? element);
}

public record struct DataSetDefinition<TDataType>(IDynamicGridViewer<TDataType> DataSet, IReadOnlyList<int> DataSpan);

internal enum DataLayoutDirection
{
    Horizontal,
    Vertical
}

internal class VirtualGridViewImpl<TDataType, TButtonType> : IVirtualGridView<TDataType, TButtonType> where TButtonType : Button
{
    private class DataView
    {
        public NullableData<TDataType> Data;
        public TButtonType? AssignedButton;

        public override string ToString() => $"Button: {AssignedButton?.Name ?? "Null"}, Data: {Data}";
    }


    private readonly Vector2I _viewportSize;
    private readonly int _viewportRows;
    private readonly int _viewportColumns;
    private readonly IViewHandler _viewHandler;
    private readonly IElementTweener _elementTweener;
    private readonly IElementFader _elementFader;
    private readonly DataLayoutDirection _dataLayoutDirection;
    private readonly Action<TDataType, TButtonType>? _drawHandler;
    private readonly Action<TDataType, TButtonType>? _focusEnteredHandler;
    private readonly Action<TDataType, TButtonType>? _focusExitedHandler;
    private readonly Action<TDataType, TButtonType>? _pressedHandler;
    private readonly ScrollBar? _horizontalScrollBar;
    private readonly ScrollBar? _verticalScrollBar;
    private readonly IDataInspector<TDataType> _dataInspector;
    private readonly IEqualityComparer<TDataType> _equalityComparer;
    private readonly PackedScene _itemPrefab;
    private readonly Control _itemContainer;
    private readonly IInfiniteLayoutGrid _layoutGrid;

    private readonly Stack<TButtonType> _buttonPool;
    private readonly Action<Control> _collectInvincibleControlHandler;

    private DataView[,] _currentView;
    private DataView[,] _nextView;

    public int ViewColumnIndex { get; private set; }

    public int ViewRowIndex { get; private set; }

    internal VirtualGridViewImpl(int viewportRows,
        int viewportColumns,
        IViewHandler viewHandler,
        IElementTweener elementTweener,
        IElementFader elementFader,
        DataLayoutDirection dataLayoutDirection,
        Action<TDataType, TButtonType>? drawHandler,
        Action<TDataType, TButtonType>? focusEnteredHandler,
        Action<TDataType, TButtonType>? focusExitedHandler,
        Action<TDataType, TButtonType>? pressedHandler,
        ScrollBar? horizontalScrollBar,
        ScrollBar? verticalScrollBar,
        IDataInspector<TDataType> dataInspector,
        IEqualityComparer<TDataType> equalityComparer,
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid)
    {
        _viewportRows = viewportRows;
        _viewportColumns = viewportColumns;
        _viewportSize = new(_viewportRows, _viewportColumns);
        _elementTweener = elementTweener;
        _elementFader = elementFader;
        _viewHandler = viewHandler;
        _dataLayoutDirection = dataLayoutDirection;
        _focusEnteredHandler = focusEnteredHandler;
        _focusExitedHandler = focusExitedHandler;
        _pressedHandler = pressedHandler;
        _horizontalScrollBar = horizontalScrollBar;
        _verticalScrollBar = verticalScrollBar;
        _dataInspector = dataInspector;
        _equalityComparer = equalityComparer;
        _itemPrefab = itemPrefab;
        _itemContainer = itemContainer;
        _layoutGrid = layoutGrid;
        _drawHandler = drawHandler;

        _collectInvincibleControlHandler = CollectionButtonInstance;
        _currentView = new DataView[_viewportRows, _viewportColumns];
        _nextView = new DataView[_viewportRows, _viewportColumns];

        for (var rowIndex = 0; rowIndex < _viewportRows; rowIndex++)
        for (var columnIndex = 0; columnIndex < _viewportColumns; columnIndex++)
        {
            _currentView[rowIndex, columnIndex] = new() { Data = NullableData<TDataType>.Null };
            _nextView[rowIndex, columnIndex] = new() { Data = NullableData<TDataType>.Null };
        }

        ViewColumnIndex = 0;
        ViewRowIndex = 0;

        var preloadAmount = Math.Max(_viewportRows, _viewportColumns) * 2;
        _buttonPool = new(preloadAmount);
        for (var i = 0; i < preloadAmount; i++)
        {
            var instance = itemPrefab.Instantiate<TButtonType>();
            _buttonPool.Push(instance);
            instance.Hide();
            itemContainer.AddChild(instance);
        }
    }

    private TButtonType GetAndInitializeButtonInstance(TDataType data)
    {
        if (!_buttonPool.TryPop(out var instance))
        {
            instance = _itemPrefab.Instantiate<TButtonType>();
            _itemContainer.AddChild(instance);
        }
        else
        {
            instance.Show();
        }

        _drawHandler?.Invoke(data, instance);

        return instance;
    }

    private void CollectionButtonInstance(Control button)
    {
        _buttonPool.Push((TButtonType)button);
        button.Hide();
    }

    public void Redraw()
    {
        for (var rowIndex = 0; rowIndex < _viewportRows; rowIndex++)
        {
            var span = _dataInspector.InspectViewColumn(rowIndex, ViewColumnIndex, ViewRowIndex);
            for (var columnIndex = 0; columnIndex < _viewportColumns; columnIndex++)
            {
                var currentDataValue = span[columnIndex];
                var currentViewItem = _currentView[rowIndex, columnIndex];

                if (IsMatch(
                        ref currentDataValue,
                        ref currentViewItem.Data,
                        out var currentDataIsNull,
                        out var currentViewDataIsNull
                    )) continue;

                if (!currentViewDataIsNull)
                {
                    _elementTweener.KillTween(currentViewItem.AssignedButton!);
                    _elementFader.Disappear(currentViewItem.AssignedButton!, _collectInvincibleControlHandler);
                    currentViewItem.AssignedButton = null;
                    currentViewItem.Data = NullableData<TDataType>.Null;
                }

                if (currentDataIsNull) continue;

                currentViewItem.Data = currentDataValue;

                var button = GetAndInitializeButtonInstance(currentDataValue.Unwrap());
                button.Position = _layoutGrid.GetGridElementPosition(new(columnIndex, rowIndex));
                _elementTweener.KillTween(button);
                _elementFader.Appear(button);
                
                currentViewItem.AssignedButton = button;
            }
        }
    }

    private bool IsMatch(ref readonly NullableData<TDataType> a, ref readonly NullableData<TDataType> b, out bool aIsNull, out bool bIsNull)
    {
        aIsNull = a.IsNull;
        bIsNull = b.IsNull;

        if (aIsNull == bIsNull)
        {
            if (aIsNull) return true;
            return _equalityComparer.Equals(a.Unwrap(), b.Unwrap());
        }
        return false;
    }

    public void Move(MoveDirection moveDirection)
    {
        Redraw();
        Vector2I moveDirectionVector;
        int isMovingOutLineIndex;
        int isMovingInLineIndex;
        int movingOutLineIndex;
        int movingInLineIndex;
        bool isRow;
        switch (moveDirection)
        {
            case MoveDirection.Up:
                isRow = true;
                isMovingOutLineIndex = _viewportRows - 1;
                isMovingInLineIndex = 0;
                movingOutLineIndex = _viewportRows;
                movingInLineIndex = -1;
                moveDirectionVector = new(-1, 0);
                break;
            case MoveDirection.Down:
                isRow = true;
                isMovingOutLineIndex = 0;
                isMovingInLineIndex = _viewportRows - 1;
                movingOutLineIndex = -1;
                movingInLineIndex = _viewportRows;
                moveDirectionVector = new(1, 0);
                break;
            case MoveDirection.Left:
                isRow = false;
                isMovingOutLineIndex = _viewportColumns - 1;
                isMovingInLineIndex = 0;
                movingOutLineIndex = _viewportColumns;
                movingInLineIndex = -1;
                moveDirectionVector = new(0, -1);
                break;
            case MoveDirection.Right:
                isRow = false;
                isMovingOutLineIndex =0;
                isMovingInLineIndex =  _viewportColumns - 1;
                movingOutLineIndex = -1;
                movingInLineIndex = _viewportColumns;
                moveDirectionVector = new(0, 1);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(moveDirection), moveDirection, null);
        }

        ViewRowIndex += moveDirectionVector.X;
        ViewColumnIndex += moveDirectionVector.Y;

        if(ViewRowIndex < 0) throw new ArgumentOutOfRangeException(nameof(moveDirection));
        if(ViewColumnIndex < 0) throw new ArgumentOutOfRangeException(nameof(moveDirection));
        
        const byte EDGE_NORMAL = 0;
        const byte EDGE_OUT = 1;
        const byte EDGE_IN = 2;
        
        // Move in-view items to new positions
        for (var rowIndex = 0; rowIndex < _viewportRows; rowIndex++)
        for (var columnIndex = 0; columnIndex < _viewportColumns; columnIndex++)
        {
            var currentViewItem = _currentView[rowIndex, columnIndex];
            var currentButton = currentViewItem.AssignedButton;

            if (currentButton is null) continue;

            var currentGridIndex = new Vector2I(rowIndex, columnIndex);
            var movementType = GetEdgeType(rowIndex, columnIndex);

            Vector2I targetGridPosition;
            switch (movementType)
            {
                case EDGE_NORMAL:
                case EDGE_IN:
                    targetGridPosition = currentGridIndex - moveDirectionVector;
                    _elementFader.KillTween(currentButton);
                    _elementTweener.MoveTo(currentButton, _layoutGrid.GetGridElementPosition(SwapXY(targetGridPosition)));
                    break;
                case EDGE_OUT:
                    targetGridPosition = isRow ? new(movingOutLineIndex, columnIndex) : new(rowIndex, movingOutLineIndex);
                    _elementFader.KillTween(currentButton);
                    _elementTweener.MoveOut(currentButton, _layoutGrid.GetGridElementPosition(SwapXY(targetGridPosition)), _collectInvincibleControlHandler);
                    break;
                default:
                    throw new ArgumentException(nameof(movementType));
            }

            if (movementType is EDGE_NORMAL or EDGE_IN)
            {
                var nextViewItem = _nextView[targetGridPosition.X, targetGridPosition.Y];
                nextViewItem.AssignedButton = currentButton;
                nextViewItem.Data = currentViewItem.Data;
            }
        }

        // Resolve newly occured item
        for (var rowIndex = 0; rowIndex < _viewportRows; rowIndex++)
        {
            var span = _dataInspector.InspectViewColumn(rowIndex, ViewColumnIndex, ViewRowIndex);
            for (var columnIndex = 0; columnIndex < _viewportColumns; columnIndex++)
            {
                var nextViewItem = _nextView[rowIndex, columnIndex];

                if (nextViewItem.AssignedButton is not null) continue;

                var currentValue = span[columnIndex];

                if (!currentValue.TryUnwrap(out var data)) continue;

                nextViewItem.Data = currentValue;

                var movementType = GetEdgeType(rowIndex, columnIndex);

                nextViewItem.AssignedButton = GetAndInitializeButtonInstance(data);

                switch (movementType)
                {
                    case EDGE_IN:
                        Vector2I emulatedStartPosition =
                            isRow ?
                                new(movingInLineIndex, columnIndex) :
                                new(rowIndex, movingInLineIndex);

                        nextViewItem.AssignedButton.Position = _layoutGrid.GetGridElementPosition(SwapXY(emulatedStartPosition));

                        _elementFader.KillTween(nextViewItem.AssignedButton);
                        _elementTweener.MoveIn(nextViewItem.AssignedButton, _layoutGrid.GetGridElementPosition(new(columnIndex, rowIndex)));

                        break;
                    default:
                        throw new ArgumentException(nameof(movementType));
                }
            }
        }

        (_currentView, _nextView) = (_nextView, _currentView);

        foreach (var dataView in _nextView)
        {
            dataView.Data = NullableData<TDataType>.Null;
            dataView.AssignedButton = null;
        }

        return;

        byte GetEdgeType(int rowIndex, int columnIndex)
        {
            if (isRow)
            {
                if (rowIndex == isMovingOutLineIndex) return EDGE_OUT;
                if (rowIndex == isMovingInLineIndex) return EDGE_IN;
                return EDGE_NORMAL;
            }

            if (columnIndex == isMovingOutLineIndex) return EDGE_OUT;
            if (columnIndex == isMovingInLineIndex) return EDGE_IN;
            return EDGE_NORMAL;
        }
    }

    private static Vector2I SwapXY(Vector2I vector2I) => new(vector2I.Y, vector2I.X);
}