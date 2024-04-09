using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Godot;

namespace GodotViews.VirtualGrid;

/// <summary>
/// Use the <see cref="Create"/> method to initiate a build process of the <see cref="IVirtualGridView{TDataType}"/> instance.
/// </summary>
public static class VirtualGridView
{
    /// <summary>
    /// Initiate a build process of the <see cref="IVirtualGridView{TDataType}"/> instance
    /// by setting up the viewport metrics, or the amount of elements displayed concurrently by the control.
    /// </summary>
    /// <param name="viewportColumns">The number of concurrently displaying columns.</param>
    /// <param name="viewportRows">The number of concurrently displaying rows.</param>
    /// <returns>A builder that continues the building process of the <see cref="IVirtualGridView{TDataType}"/> instance.</returns>
    public static IViewHandlerBuilder Create(int viewportColumns, int viewportRows) => new ViewHandlerBuilder(viewportColumns, viewportRows);
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

    public IDataLayoutBuilder WithHandlers(
        IElementPositioner elementPositioner,
        IElementTweener elementTweener,
        IElementFader elementFader
    ) =>
        new DataLayoutSelectionBuilder(
            this,
            elementPositioner,
            elementTweener,
            elementFader
        );
}

internal class DataLayoutSelectionBuilder(ViewHandlerBuilder viewHandlerBuilder, IElementPositioner elementPositioner, IElementTweener elementTweener, IElementFader elementFader) : IDataLayoutBuilder
{
    public ViewHandlerBuilder ViewHandlerBuilder { get; } = viewHandlerBuilder;
    public IElementPositioner ElementPositioner { get; } = elementPositioner;
    public IElementTweener ElementTweener { get; } = elementTweener;
    public IElementFader ElementFader { get; } = elementFader;

    public IHorizontalDataLayoutBuilder<TDataType> WithHorizontalDataLayout<TDataType>(
        IEqualityComparer<TDataType>? equalityComparer = null,
        bool reverseLocalLayout = false
    ) =>
        new DataLayoutBuilder<TDataType>(
            this,
            equalityComparer,
            reverseLocalLayout,
            true
        );

    public IVerticalDataLayoutBuilder<TDataType> WithVerticalDataLayout<TDataType>(
        IEqualityComparer<TDataType>? equalityComparer = null,
        bool reverseLocalLayout = false
    ) =>
        new DataLayoutBuilder<TDataType>(
            this,
            equalityComparer,
            reverseLocalLayout,
            false
        );
}

internal partial class DataLayoutBuilder<TDataType>(
    DataLayoutSelectionBuilder dataLayoutSelectionBuilder,
    IEqualityComparer<TDataType>? equalityComparer,
    bool reverseLocalLayout,
    bool isHorizontalDataLayout) : IHorizontalDataLayoutBuilder<TDataType>, IVerticalDataLayoutBuilder<TDataType>
{
    private record struct AnnotatedDataSet<T>(IDynamicGridViewer<T> DataSet, int LocalIndex);

    private readonly List<IDynamicGridViewer<TDataType>> _dataSetDefinitions = [];
    public DataLayoutSelectionBuilder DataLayoutSelectionBuilder { get; } = dataLayoutSelectionBuilder;
    public IEqualityComparer<TDataType> EqualityComparer { get; } = equalityComparer ?? EqualityComparer<TDataType>.Default;

    public IHorizontalDataLayoutBuilder<TDataType> AppendRowDataSet(IDynamicGridViewer<TDataType> dataSetDefinition, int repeatCount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(repeatCount);
        for(var i = 0; i < repeatCount; i++)
            _dataSetDefinitions.Add(dataSetDefinition);
        return this;
    }

    public IVerticalDataLayoutBuilder<TDataType> AppendColumnDataSet(IDynamicGridViewer<TDataType> dataSetDefinition, int repeatCount = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(repeatCount);
        for(var i = 0; i < repeatCount; i++)
            _dataSetDefinitions.Add(dataSetDefinition);
        return this;
    }
    
    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> WithArgument<TButtonType, TExtraArgument>(
        PackedScene itemPrefab,
        Control itemContainer,
        IInfiniteLayoutGrid layoutGrid
    ) where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
    {
        var dataSetDefinitions = CollectionsMarshal.AsSpan(_dataSetDefinitions);

        var dataMap = new AnnotatedDataSet<TDataType>[dataSetDefinitions.Length];

        Dictionary<IDynamicGridViewer<TDataType>, int> dataSetCounter = new();

        for (var index = 0; index < dataSetDefinitions.Length; index++)
        {
            var dataSetDefinition = dataSetDefinitions[index];
            ref var currentCount = ref CollectionsMarshal.GetValueRefOrAddDefault(dataSetCounter, dataSetDefinition, out var exists);
            if (!exists) currentCount = 0;
            dataMap[index] = new(dataSetDefinition, currentCount++);
        }

        foreach (var (dynamicGridViewer, count) in dataSetCounter) 
            dynamicGridViewer.FixedMetric = count;
        
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

        IDataInspector<TDataType> dataInspector = isHorizontalDataLayout ?
            new HorizontalDataInspector<TDataType>(dataMap, viewColumns, viewRows) :
            new VerticalDataInspector<TDataType>(dataMap, viewColumns, viewRows);

        return new FinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument>(
            this,
            dataInspector,
            itemPrefab,
            itemContainer,
            layoutGrid
        );
    }
}

internal class FinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument>(
    DataLayoutBuilder<TDataType> dataLayoutBuilder,
    IDataInspector<TDataType> dataInspector,
    PackedScene itemPrefab,
    Control itemContainer,
    IInfiniteLayoutGrid layoutGrid) : IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument>
    where TButtonType : VirtualGridViewItem<TDataType, TExtraArgument>
{
    private bool _autoHideHorizontalScrollBar;
    private bool _autoHideVerticalScrollBar;

    private TExtraArgument? _extraArgument;
    private ScrollBar? _horizontalScrollBar;
    private IElementFader? _horizontalScrollBarFader;
    private IScrollBarTweener? _horizontalScrollBarTweener;

    private ScrollBar? _verticalScrollBar;
    private IElementFader? _verticalScrollBarFader;
    private IScrollBarTweener? _verticalScrollBarTweener;

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureVerticalScrollBar(
        ScrollBar verticalScrollBar,
        IScrollBarTweener? tweener,
        IElementFader? fader,
        bool autoHide = false
    )
    {
        _verticalScrollBar = verticalScrollBar;
        _autoHideHorizontalScrollBar = autoHide;
        _verticalScrollBarTweener = tweener;
        _verticalScrollBarFader = fader;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureHorizontalScrollBar(
        ScrollBar horizontalScrollBar,
        IScrollBarTweener? tweener,
        IElementFader? fader,
        bool autoHide = false
    )
    {
        _horizontalScrollBar = horizontalScrollBar;
        _autoHideVerticalScrollBar = autoHide;
        _horizontalScrollBarTweener = tweener;
        _horizontalScrollBarFader = fader;
        return this;
    }

    public IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument> ConfigureExtraArgument(TExtraArgument extraArgument)
    {
        _extraArgument = extraArgument;
        return this;
    }

    public IVirtualGridView<TDataType> Build()
    {
        var dataLayoutSelectionBuilder = dataLayoutBuilder.DataLayoutSelectionBuilder;
        var viewAlignmentBuilder = dataLayoutSelectionBuilder.ViewHandlerBuilder;

        return new VirtualGridViewImpl<TDataType, TButtonType, TExtraArgument>(
            viewAlignmentBuilder.ViewportRows,
            viewAlignmentBuilder.ViewportColumns,
            dataLayoutSelectionBuilder.ElementPositioner,
            dataLayoutSelectionBuilder.ElementTweener,
            dataLayoutSelectionBuilder.ElementFader,
            _horizontalScrollBar,
            _autoHideHorizontalScrollBar,
            _horizontalScrollBarTweener ?? ScrollBarTweeners.None,
            _horizontalScrollBarFader ?? ElementFaders.None,
            _verticalScrollBar,
            _autoHideVerticalScrollBar,
            _verticalScrollBarTweener ?? ScrollBarTweeners.None,
            _verticalScrollBarFader ?? ElementFaders.None,
            dataInspector,
            dataLayoutBuilder.EqualityComparer,
            itemPrefab,
            itemContainer,
            layoutGrid,
            _extraArgument
        );
    }
}