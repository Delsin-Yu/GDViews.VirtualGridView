using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Godot;

namespace GodotViews.VirtualGrid;

public interface IVirtualGridView<TDataType, TButtonType>
{
    void Redraw();
    void Move(MoveDirection moveDirection);
    int ViewColumnIndex { get; }
    int ViewRowIndex { get; }
}

public interface IViewHandler
{
    // Impl！
}

public interface IInfiniteLayoutGrid
{
    Vector2 GetGridElementPosition(Vector2I gridPosition);
}

public interface IElementTweener
{
    void MoveTo(Control control, Vector2 targetPosition);
    void MoveIn(Control control, Vector2 targetPosition);
    void MoveOut(Control control, Vector2 targetPosition, Action<Control> onFinish);
    void KillTween(Control control);
}

public interface IElementFader
{
    void Disappear(Control control, Action<Control> onFinish);
    void Appear(Control control);
    void KillTween(Control control);
}

public interface IViewHandlerBuilder
{
    IDataLayoutBuilder WithViewHandler(IViewHandler viewHandler, IElementTweener elementTweener, IElementFader elementFader);
}

internal class ViewHandlerBuilder : IViewHandlerBuilder
{
    public ViewHandlerBuilder(int viewportColumns, int viewportRows)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(viewportColumns);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(viewportRows);

        ViewportColumns = viewportColumns;
        ViewportRows = viewportRows;
    }

    public int ViewportColumns { get; }
    public int ViewportRows { get; }

    public IDataLayoutBuilder WithViewHandler(IViewHandler viewHandler, IElementTweener elementTweener, IElementFader elementFader) => new DataLayoutSelectionBuilder(this, viewHandler, elementTweener, elementFader);
}

public interface IDataLayoutBuilder
{
    IHorizontalDataLayoutBuilder<TDataType> WithHorizontalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false);
    IVerticalDataLayoutBuilder<TDataType> WithVerticalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false);
}

internal class DataLayoutSelectionBuilder(ViewHandlerBuilder viewHandlerBuilder, IViewHandler viewHandler, IElementTweener elementTweener, IElementFader elementFader) : IDataLayoutBuilder
{
    public ViewHandlerBuilder ViewHandlerBuilder { get; } = viewHandlerBuilder;
    public IViewHandler ViewHandler { get; } = viewHandler;
    public IElementTweener ElementTweener { get; } = elementTweener;
    public IElementFader ElementFader { get; } = elementFader;

    public IHorizontalDataLayoutBuilder<TDataType> WithHorizontalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false) => new DataLayoutBuilder<TDataType>(this, DataLayoutDirection.Horizontal, equalityComparer, reverseLocalLayout);

    public IVerticalDataLayoutBuilder<TDataType> WithVerticalDataLayout<TDataType>(IEqualityComparer<TDataType>? equalityComparer = null, bool reverseLocalLayout = false) => new DataLayoutBuilder<TDataType>(this, DataLayoutDirection.Vertical, equalityComparer, reverseLocalLayout);
}

public interface IDelegateBuilderAccess<TDataType>
{
    IDelegateBuilder<TDataType, TButtonType> WithDelegate<TButtonType>() where TButtonType : Button;
}

public interface IHorizontalDataLayoutBuilder<TDataType> : IDelegateBuilderAccess<TDataType>
{
    IHorizontalDataLayoutBuilder<TDataType> AddRowDataSource(DataSetDefinition<TDataType> dataSetDefinition);
}

public interface IVerticalDataLayoutBuilder<TDataType> : IDelegateBuilderAccess<TDataType>
{
    IVerticalDataLayoutBuilder<TDataType> AddColumnDataSource(DataSetDefinition<TDataType> dataSetDefinition);
}

internal class DataLayoutBuilder<TDataType>(DataLayoutSelectionBuilder dataLayoutSelectionBuilder, DataLayoutDirection dataLayoutDirection, IEqualityComparer<TDataType>? equalityComparer, bool reverseLocalLayout) : IHorizontalDataLayoutBuilder<TDataType>, IVerticalDataLayoutBuilder<TDataType>
{
    public DataLayoutDirection DataLayoutDirection { get; } = dataLayoutDirection;
    public DataLayoutSelectionBuilder DataLayoutSelectionBuilder { get; } = dataLayoutSelectionBuilder;
    public IEqualityComparer<TDataType> EqualityComparer { get; } = equalityComparer ?? EqualityComparer<TDataType>.Default;

    private readonly List<DataSetDefinition<TDataType>> _dataSetDefinitions = [];

    public IHorizontalDataLayoutBuilder<TDataType> AddRowDataSource(DataSetDefinition<TDataType> dataSetDefinition)
    {
        _dataSetDefinitions.Add(dataSetDefinition);
        return this;
    }

    public IVerticalDataLayoutBuilder<TDataType> AddColumnDataSource(DataSetDefinition<TDataType> dataSetDefinition)
    {
        _dataSetDefinitions.Add(dataSetDefinition);
        return this;
    }

    private record struct AnnotatedDataSet<T>(IDynamicGridViewer<T> DataSet, int LocalIndex);

    public IDelegateBuilder<TDataType, TButtonType> WithDelegate<TButtonType>() where TButtonType : Button
    {
        HashSet<int> existingIndexes = [];

        var maxIndex = -1;
        var dataSetDefinitions = CollectionsMarshal.AsSpan(_dataSetDefinitions);
        foreach (ref readonly var dataSetDefinition in dataSetDefinitions)
        {
            if (dataSetDefinition.DataSet.FixedMetric != dataSetDefinition.DataSpan.Count)
                throw new ArgumentException("A data set's fixed metric differs from the declared data span count!");

            if (dataSetDefinition.DataSpan.Any(x => x < 0))
                throw new ArgumentException("A data set's declared data span contains index(es) value less than 0!");

            if (dataSetDefinition.DataSpan.Distinct().Count() != dataSetDefinition.DataSpan.Count)
                throw new ArgumentException("A data set's declared data span contains duplicate index(es)!");

            for (var spanIndex = 0; spanIndex < dataSetDefinition.DataSpan.Count; spanIndex++)
            {
                var dataIndex = dataSetDefinition.DataSpan[spanIndex];
                if (!existingIndexes.Add(dataIndex))
                    throw new ArgumentException($"A data set has already occupied the specified index {dataIndex}");
                maxIndex = Math.Max(dataIndex, maxIndex);
            }
        }

        var diff = existingIndexes.Count - 1 - maxIndex;

        if (diff > 0)
        {
            int[] missingIndexes = [diff];
            var localCounter = 0;
            for (var i = 0; i <= maxIndex; i++)
            {
                if (existingIndexes.Contains(i)) continue;
                missingIndexes[localCounter++] = i;
            }

            throw new ArgumentException($"One or more index(es) has not yet occupied by any data set(s): [{string.Join(", ", missingIndexes)}]");
        }

        var dataMap = new AnnotatedDataSet<TDataType>[existingIndexes.Count];

        Dictionary<IDynamicGridViewer<TDataType>, int> dataSetCounter = new();

        foreach (ref readonly var dataSetDefinition in dataSetDefinitions)
        {
            for (var spanIndex = 0; spanIndex < dataSetDefinition.DataSpan.Count; spanIndex++)
            {
                var dataIndex = dataSetDefinition.DataSpan[spanIndex];

                ref var currentCount = ref CollectionsMarshal.GetValueRefOrAddDefault(dataSetCounter, dataSetDefinition.DataSet, out var exists);
                if (!exists) currentCount = 0;
                dataMap[dataIndex] = new(dataSetDefinition.DataSet, currentCount++);
            }
        }

        if (reverseLocalLayout)
        {
            foreach (ref var annotatedDataSet in dataMap.AsSpan())
            {
                var count = dataSetCounter[annotatedDataSet.DataSet] - 1;
                annotatedDataSet.LocalIndex = count - annotatedDataSet.LocalIndex;
            }
        }

        var viewColumns = DataLayoutSelectionBuilder.ViewHandlerBuilder.ViewportColumns;
        var viewRows = DataLayoutSelectionBuilder.ViewHandlerBuilder.ViewportRows;

        IDataInspector<TDataType> dataInspector = DataLayoutDirection == DataLayoutDirection.Horizontal ?
            new HorizontalDataInspector<TDataType>(dataMap, viewColumns, viewRows) :
            new VerticalDataInspector<TDataType>(dataMap, viewColumns, viewRows);

        return new DelegateBuilder<TDataType, TButtonType>(this, dataInspector);
    }

    private class HorizontalDataInspector<T>(AnnotatedDataSet<T>[] dataMap, int viewColumns, int viewRows) : IDataInspector<T>
    {
        private readonly NullableData<T>[] _view = new NullableData<T>[viewColumns];

        public int ViewRowCount { get; } = viewRows;

        /*
         * [0] DataSet1 <====> [0]
         * [1] DataSet1 <====> [1]
         * [2] DataSet2 <====> [0]
         * [3] DataSet1 <====> [2]
         * [4] DataSet2 <====> [1]
         * [5] DataSet1 <====> [3]
         */

        public ReadOnlySpan<NullableData<T>> InspectViewColumn(int rowIndex, int columnOffset, int rowOffset)
        {
            var viewSpan = _view.AsSpan();
            NullableData<T>.Clear(ref viewSpan);

            var actualRow = rowOffset + rowIndex;

            if (dataMap.Length <= actualRow)
                return viewSpan;

            var dstColumnIndex = columnOffset + viewColumns;

            var (dataSet, localIndex) = dataMap[actualRow];

            for (var i = columnOffset; i < dstColumnIndex; i++)
            {
                ref var current = ref viewSpan[i - columnOffset];

                if (dataSet.TryGetGridElement(localIndex, i, out var element))
                    current = new(true, element);
                else
                    current = NullableData<T>.Null;
            }

            return viewSpan;
        }
    }

    private class VerticalDataInspector<T>(AnnotatedDataSet<T>[] dataMap, int viewColumns, int viewRows) : IDataInspector<T>
    {
        private readonly NullableData<T>[] _view = new NullableData<T>[viewColumns];

        public int ViewRowCount { get; } = viewRows;

        /*
         * [0] [1] [2] [3] [4] [5]
         * DS1 DS1 DS2 DS1 DS2 DS1
         *  ^   ^   ^   ^   ^   ^
         *  |   |   |   |   |   |
         *  v   v   v   v   v   v
         * [0] [1] [0] [2] [1] [3]
         */

        public ReadOnlySpan<NullableData<T>> InspectViewColumn(int rowIndex, int columnOffset, int rowOffset)
        {
            var viewSpan = _view.AsSpan();
            NullableData<T>.Clear(ref viewSpan);

            if (dataMap.Length <= columnOffset)
                return viewSpan;

            var actualRow = rowOffset + rowIndex;

            var dstColumnIndex = columnOffset + viewColumns;

            for (var i = columnOffset; i < dstColumnIndex; i++)
            {
                ref var current = ref viewSpan[i - columnOffset];

                if (dataMap.Length <= i)
                {
                    current = NullableData<T>.Null;
                    continue;
                }

                var (dataSet, localIndex) = dataMap[i];

                if (dataSet.TryGetGridElement(localIndex, actualRow, out var element))
                    current = new(true, element);
                else
                    current = NullableData<T>.Null;
            }

            return viewSpan;
        }
    }
}

public interface IDelegateBuilder<TDataType, TButtonType> where TButtonType : Button
{
    public IDelegateBuilder<TDataType, TButtonType> ConfigureDrawHandler(Action<TDataType, TButtonType> drawHandler);
    IDelegateBuilder<TDataType, TButtonType> ConfigureFocusEnteredHandler(Action<TDataType, TButtonType> focusEnteredHandler);
    IDelegateBuilder<TDataType, TButtonType> ConfigureFocusExitedHandler(Action<TDataType, TButtonType> focusExitedHandler);
    IDelegateBuilder<TDataType, TButtonType> ConfigurePressedHandler(Action<TDataType, TButtonType> pressedHandler);
    IFinishingArgumentBuilder<TDataType, TButtonType> WithArgument(PackedScene itemPrefab, Control itemContainer, IInfiniteLayoutGrid layoutGrid);
}

internal class DelegateBuilder<TDataType, TButtonType>(DataLayoutBuilder<TDataType> dataLayoutBuilder, IDataInspector<TDataType> dataInspector) : IDelegateBuilder<TDataType, TButtonType> where TButtonType : Button
{
    public DataLayoutBuilder<TDataType> DataLayoutBuilder { get; } = dataLayoutBuilder;
    public IDataInspector<TDataType> DataInspector { get; } = dataInspector;

    public Action<TDataType, TButtonType>? DrawHandler { get; private set; }
    public Action<TDataType, TButtonType>? FocusEnteredHandler { get; private set; }
    public Action<TDataType, TButtonType>? FocusExitedHandler { get; private set; }
    public Action<TDataType, TButtonType>? PressedHandler { get; private set; }


    public IDelegateBuilder<TDataType, TButtonType> ConfigureDrawHandler(Action<TDataType, TButtonType> drawHandler)
    {
        DrawHandler = drawHandler;
        return this;
    }

    public IDelegateBuilder<TDataType, TButtonType> ConfigureFocusEnteredHandler(Action<TDataType, TButtonType> focusEnteredHandler)
    {
        FocusEnteredHandler = focusEnteredHandler;
        return this;
    }

    public IDelegateBuilder<TDataType, TButtonType> ConfigureFocusExitedHandler(Action<TDataType, TButtonType> focusExitedHandler)
    {
        FocusExitedHandler = focusExitedHandler;
        return this;
    }

    public IDelegateBuilder<TDataType, TButtonType> ConfigurePressedHandler(Action<TDataType, TButtonType> pressedHandler)
    {
        PressedHandler = pressedHandler;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType> WithArgument(PackedScene itemPrefab, Control itemContainer, IInfiniteLayoutGrid layoutGrid) => new FinishingArgumentBuilder<TDataType, TButtonType>(this, itemPrefab, itemContainer, layoutGrid);
}

public interface IFinishingArgumentBuilder<TDataType, TButtonType>
{
    IVirtualGridView<TDataType, TButtonType> Build();
    IFinishingArgumentBuilder<TDataType, TButtonType> ConfigureHorizontalScrollBar(ScrollBar scrollBar);
    IFinishingArgumentBuilder<TDataType, TButtonType> ConfigureVerticalScrollBar(ScrollBar scrollBar);
}

internal class FinishingArgumentBuilder<TDataType, TButtonType>(
    DelegateBuilder<TDataType, TButtonType> delegateBuilder,
    PackedScene itemPrefab,
    Control itemContainer,
    IInfiniteLayoutGrid layoutGrid) : IFinishingArgumentBuilder<TDataType, TButtonType> where TButtonType : Button
{
    private ScrollBar? _horizontalScrollBar;
    private ScrollBar? _verticalScrollBar;

    public IFinishingArgumentBuilder<TDataType, TButtonType> ConfigureVerticalScrollBar(ScrollBar verticalScrollBar)
    {
        _verticalScrollBar = verticalScrollBar;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType> ConfigureHorizontalScrollBar(ScrollBar horizontalScrollBar)
    {
        _horizontalScrollBar = horizontalScrollBar;
        return this;
    }

    public IVirtualGridView<TDataType, TButtonType> Build()
    {
        var dataLayoutBuilder = delegateBuilder.DataLayoutBuilder;
        var dataLayoutSelectionBuilder = dataLayoutBuilder.DataLayoutSelectionBuilder;
        var viewAlignmentBuilder = dataLayoutSelectionBuilder.ViewHandlerBuilder;

        return new VirtualGridViewImpl<TDataType, TButtonType>(
            viewAlignmentBuilder.ViewportRows,
            viewAlignmentBuilder.ViewportColumns,
            dataLayoutSelectionBuilder.ViewHandler,
            dataLayoutSelectionBuilder.ElementTweener,
            dataLayoutSelectionBuilder.ElementFader,
            dataLayoutBuilder.DataLayoutDirection,
            delegateBuilder.DrawHandler,
            delegateBuilder.FocusEnteredHandler,
            delegateBuilder.FocusExitedHandler,
            delegateBuilder.PressedHandler,
            _horizontalScrollBar,
            _verticalScrollBar,
            delegateBuilder.DataInspector,
            dataLayoutBuilder.EqualityComparer,
            itemPrefab,
            itemContainer,
            layoutGrid
        );
    }
}