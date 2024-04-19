# <a id="GodotViews_VirtualGrid_IVirtualGridView_1"></a> Interface IVirtualGridView<TDataType\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Represents a controller that provides feature
to navigate through and customise the virtualized grid view.

```csharp
public interface IVirtualGridView<TDataType>
```

#### Type Parameters

`TDataType` 

The type for the data this controller focuses on.

## Properties

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_AutoHideHScrollBar"></a> AutoHideHScrollBar

When sets to true, the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> will hide the horizontal scroll bar
when the current viewport is horizontally sufficient for showing every element of the datasets.

```csharp
bool AutoHideHScrollBar { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_AutoHideVScrollBar"></a> AutoHideVScrollBar

When sets to true, the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> will hide the vertical scroll bar
when the current viewport is vertically sufficient for showing every element of the datasets.

```csharp
bool AutoHideVScrollBar { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_ElementFader"></a> ElementFader

Accessor for the currently active ElementFader,
assigning null to it will automatically fallbacks to <xref href="GodotViews.VirtualGrid.ElementFaders.None" data-throw-if-not-resolved="false"></xref>.

```csharp
IElementFader ElementFader { get; set; }
```

#### Property Value

 [IElementFader](GodotViews.VirtualGrid.IElementFader.md)

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_ElementPositioner"></a> ElementPositioner

Accessor for the currently active ElementPositioner,
assigning null to it will automatically fallbacks to <xref href="GodotViews.VirtualGrid.ElementPositioners.Side" data-throw-if-not-resolved="false"></xref>.

```csharp
IElementPositioner ElementPositioner { get; set; }
```

#### Property Value

 [IElementPositioner](GodotViews.VirtualGrid.IElementPositioner.md)

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_ElementTweener"></a> ElementTweener

Accessor for the currently active ElementTweener,
assigning null to it will automatically fallbacks to <xref href="GodotViews.VirtualGrid.ElementTweeners.None" data-throw-if-not-resolved="false"></xref>.

```csharp
IElementTweener ElementTweener { get; set; }
```

#### Property Value

 [IElementTweener](GodotViews.VirtualGrid.IElementTweener.md)

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_HScrollBarFader"></a> HScrollBarFader

Accessor for the currently active Horizontal ScrollBarFader,
assigning null to it will automatically fallbacks to <xref href="GodotViews.VirtualGrid.ElementFaders.None" data-throw-if-not-resolved="false"></xref>.

```csharp
IElementFader HScrollBarFader { get; set; }
```

#### Property Value

 [IElementFader](GodotViews.VirtualGrid.IElementFader.md)

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_HScrollBarTweener"></a> HScrollBarTweener

Accessor for the currently active Horizontal ScrollBarTweener,
assigning null to it will automatically fallbacks to <xref href="GodotViews.VirtualGrid.ScrollBarTweeners.None" data-throw-if-not-resolved="false"></xref>.

```csharp
IScrollBarTweener HScrollBarTweener { get; set; }
```

#### Property Value

 [IScrollBarTweener](GodotViews.VirtualGrid.IScrollBarTweener.md)

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_VScrollBarFader"></a> VScrollBarFader

Accessor for the currently active Vertical ScrollBarFader,
assigning null to it will automatically fallbacks to <xref href="GodotViews.VirtualGrid.ElementFaders.None" data-throw-if-not-resolved="false"></xref>.

```csharp
IElementFader VScrollBarFader { get; set; }
```

#### Property Value

 [IElementFader](GodotViews.VirtualGrid.IElementFader.md)

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_VScrollBarTweener"></a> VScrollBarTweener

Accessor for the currently active Vertical ScrollBarTweener,
assigning null to it will automatically fallbacks to <xref href="GodotViews.VirtualGrid.ScrollBarTweeners.None" data-throw-if-not-resolved="false"></xref>.

```csharp
IScrollBarTweener VScrollBarTweener { get; set; }
```

#### Property Value

 [IScrollBarTweener](GodotViews.VirtualGrid.IScrollBarTweener.md)

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_ViewColumns"></a> ViewColumns

The number of columns for the concurrently displayed virtualized grid items.

```csharp
int ViewColumns { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_ViewRows"></a> ViewRows

The number of rows for the concurrently displayed virtualized grid items.

```csharp
int ViewRows { get; }
```

#### Property Value

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Methods

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_GrabFocus"></a> GrabFocus\(\)

Trying to create a focus on the first available element.

```csharp
bool GrabFocus()
```

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if successfully grabs the focus; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Remarks

Controller will try to establish a focus based on certain methods,
and will automatically fallback to the alternative if the current method fails:<br /><ol><li>Try to focus to the previous selected data value.</li><li>Try to focus to the previous selected view position.</li><li>Try to focus to the top left element of the viewport.</li><li>Try to focus to the top left element of the datasets.</li><li>Return false.</li></ol>

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_GrabFocus_GodotViews_VirtualGrid_ViewFocusFinderPreset__"></a> GrabFocus\(in ViewFocusFinderPreset\)

Trying to create a focus on the element based on the specified <xref href="GodotViews.VirtualGrid.ViewFocusFinderPreset" data-throw-if-not-resolved="false"></xref>.<br />
You may access a set of presets from the <xref href="GodotViews.VirtualGrid.FocusPresets" data-throw-if-not-resolved="false"></xref> class.

```csharp
bool GrabFocus(in ViewFocusFinderPreset preset)
```

#### Parameters

`preset` [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

The preset for specifying the focus-finding logic.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if successfully grabs the focus; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_GrabFocus_GodotViews_VirtualGrid_DataFocusFinderPreset__"></a> GrabFocus\(in DataFocusFinderPreset\)

Trying to create a focus on the element based on the specified <xref href="GodotViews.VirtualGrid.DataFocusFinderPreset" data-throw-if-not-resolved="false"></xref>.
You may access a set of presets from the <xref href="GodotViews.VirtualGrid.FocusPresets" data-throw-if-not-resolved="false"></xref> class.

```csharp
bool GrabFocus(in DataFocusFinderPreset preset)
```

#### Parameters

`preset` [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset.md)

The preset for specifying the focus-finding logic.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if successfully grabs the focus; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_GrabFocus__1_GodotViews_VirtualGrid_ViewFocusFinderPreset___0____0_GodotViews_VirtualGrid_SearchDirection_"></a> GrabFocus<TArgument\>\(ViewFocusFinderPreset<TArgument\>, TArgument, SearchDirection\)

Trying to create a focus on the element based on the specified <xref href="GodotViews.VirtualGrid.ViewFocusFinderPreset%601" data-throw-if-not-resolved="false"></xref>.<br />
You may access a set of presets from the <xref href="GodotViews.VirtualGrid.FocusPresets" data-throw-if-not-resolved="false"></xref> class.

```csharp
bool GrabFocus<TArgument>(ViewFocusFinderPreset<TArgument> preset, TArgument argument, SearchDirection searchDirection)
```

#### Parameters

`preset` [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset\-1.md)<TArgument\>

The preset for specifying the focus-finding logic.

`argument` TArgument

The extra argument passes to the focus-finding logic.

`searchDirection` [SearchDirection](GodotViews.VirtualGrid.SearchDirection.md)

The search direction for the focus-finding logic.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if successfully grabs the focus; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Type Parameters

`TArgument` 

The type of the extra argument, specified by the <xref href="GodotViews.VirtualGrid.ViewFocusFinderPreset%601" data-throw-if-not-resolved="false"></xref>.

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_GrabFocus__1_GodotViews_VirtualGrid_DataFocusFinderPreset___0____0_GodotViews_VirtualGrid_SearchDirection_"></a> GrabFocus<TArgument\>\(DataFocusFinderPreset<TArgument\>, TArgument, SearchDirection\)

Trying to create a focus on the element based on the specified <xref href="GodotViews.VirtualGrid.DataFocusFinderPreset%601" data-throw-if-not-resolved="false"></xref>.<br />
You may access a set of presets from the <xref href="GodotViews.VirtualGrid.FocusPresets" data-throw-if-not-resolved="false"></xref> class.

```csharp
bool GrabFocus<TArgument>(DataFocusFinderPreset<TArgument> preset, TArgument argument, SearchDirection searchDirection)
```

#### Parameters

`preset` [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset\-1.md)<TArgument\>

The preset for specifying the focus-finding logic.

`argument` TArgument

The extra argument passes to the focus-finding logic.

`searchDirection` [SearchDirection](GodotViews.VirtualGrid.SearchDirection.md)

The search direction for the focus-finding logic.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if successfully grabs the focus; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Type Parameters

`TArgument` 

The type of the extra argument, specified by the <xref href="GodotViews.VirtualGrid.DataFocusFinderPreset%601" data-throw-if-not-resolved="false"></xref>.

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_GrabFocus__1_GodotViews_VirtualGrid_IViewFocusFinder___0__GodotViews_VirtualGrid_IViewStartHandler___0____0_GodotViews_VirtualGrid_SearchDirection_"></a> GrabFocus<TArgument\>\(IViewFocusFinder<TArgument\>, IViewStartHandler<TArgument\>, TArgument, SearchDirection\)

Trying to create a focus on the element based on the specified arguments.<br />
You may access a set of view focus finders from the <xref href="GodotViews.VirtualGrid.FocusFinders" data-throw-if-not-resolved="false"></xref> class,
and a set of view start handlers from the <xref href="GodotViews.VirtualGrid.StartHandlers" data-throw-if-not-resolved="false"></xref> class.

```csharp
bool GrabFocus<TArgument>(IViewFocusFinder<TArgument> focusFinder, IViewStartHandler<TArgument> startHandler, TArgument argument, SearchDirection searchDirection)
```

#### Parameters

`focusFinder` [IViewFocusFinder](GodotViews.VirtualGrid.IViewFocusFinder\-1.md)<TArgument\>

The <xref href="GodotViews.VirtualGrid.IViewFocusFinder%601" data-throw-if-not-resolved="false"></xref> that provide the focus-finding logic.

`startHandler` [IViewStartHandler](GodotViews.VirtualGrid.IViewStartHandler\-1.md)<TArgument\>

The <xref href="GodotViews.VirtualGrid.IViewStartHandler%601" data-throw-if-not-resolved="false"></xref> that provide the search-starting position.

`argument` TArgument

The extra argument passes to the focus-finding logic.

`searchDirection` [SearchDirection](GodotViews.VirtualGrid.SearchDirection.md)

The search direction for the focus-finding logic.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if successfully grabs the focus; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Type Parameters

`TArgument` 

The type of the extra argument, specified by the
    <xref href="GodotViews.VirtualGrid.IViewFocusFinder%601" data-throw-if-not-resolved="false"></xref> and <xref href="GodotViews.VirtualGrid.IViewStartHandler%601" data-throw-if-not-resolved="false"></xref>.

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_GrabFocus__1_GodotViews_VirtualGrid_IDataFocusFinder___0__GodotViews_VirtualGrid_IDataStartHandler___0____0_GodotViews_VirtualGrid_SearchDirection_"></a> GrabFocus<TArgument\>\(IDataFocusFinder<TArgument\>, IDataStartHandler<TArgument\>, TArgument, SearchDirection\)

Trying to create a focus on the element based on the specified arguments.<br />
You may access a set of data focus finders from the <xref href="GodotViews.VirtualGrid.FocusFinders" data-throw-if-not-resolved="false"></xref> class,
and a set of data start handlers from the <xref href="GodotViews.VirtualGrid.StartHandlers" data-throw-if-not-resolved="false"></xref> class.

```csharp
bool GrabFocus<TArgument>(IDataFocusFinder<TArgument> focusFinder, IDataStartHandler<TArgument> startHandler, TArgument argument, SearchDirection searchDirection)
```

#### Parameters

`focusFinder` [IDataFocusFinder](GodotViews.VirtualGrid.IDataFocusFinder\-1.md)<TArgument\>

The <xref href="GodotViews.VirtualGrid.IDataFocusFinder%601" data-throw-if-not-resolved="false"></xref> that provide the focus-finding logic.

`startHandler` [IDataStartHandler](GodotViews.VirtualGrid.IDataStartHandler\-1.md)<TArgument\>

The <xref href="GodotViews.VirtualGrid.IDataStartHandler%601" data-throw-if-not-resolved="false"></xref> that provide the search-starting position.

`argument` TArgument

The extra argument passes to the focus-finding logic.

`searchDirection` [SearchDirection](GodotViews.VirtualGrid.SearchDirection.md)

The search direction for the focus-finding logic.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if successfully grabs the focus; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Type Parameters

`TArgument` 

The type of the extra argument, specified by the
    <xref href="GodotViews.VirtualGrid.IDataFocusFinder%601" data-throw-if-not-resolved="false"></xref> and <xref href="GodotViews.VirtualGrid.IDataStartHandler%601" data-throw-if-not-resolved="false"></xref>.

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_GrabFocus_GodotViews_VirtualGrid_IEqualityDataFocusFinder__0__"></a> GrabFocus\(IEqualityDataFocusFinder, in TDataType\)

Trying to create a focus on the element based on the specified <xref href="GodotViews.VirtualGrid.IEqualityDataFocusFinder" data-throw-if-not-resolved="false"></xref>.<br />

```csharp
bool GrabFocus(IEqualityDataFocusFinder focusFinder, in TDataType matchingArgument)
```

#### Parameters

`focusFinder` [IEqualityDataFocusFinder](GodotViews.VirtualGrid.IEqualityDataFocusFinder.md)

The <xref href="GodotViews.VirtualGrid.IEqualityDataFocusFinder" data-throw-if-not-resolved="false"></xref> that provide the focus-finding logic.

`matchingArgument` TDataType

The argument passes to the focus-finding logic that uses for performing the matching.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if successfully grabs the focus; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_GrabFocus_GodotViews_VirtualGrid_IPredicateDataFocusFinder_System_Predicate__0__"></a> GrabFocus\(IPredicateDataFocusFinder, Predicate<TDataType\>\)

Trying to create a focus on the element based on the specified <xref href="GodotViews.VirtualGrid.IPredicateDataFocusFinder" data-throw-if-not-resolved="false"></xref>.<br />

```csharp
bool GrabFocus(IPredicateDataFocusFinder focusFinder, Predicate<TDataType> predicate)
```

#### Parameters

`focusFinder` [IPredicateDataFocusFinder](GodotViews.VirtualGrid.IPredicateDataFocusFinder.md)

The <xref href="GodotViews.VirtualGrid.IPredicateDataFocusFinder" data-throw-if-not-resolved="false"></xref> that provide the focus-finding logic.

`predicate` [Predicate](https://learn.microsoft.com/dotnet/api/system.predicate\-1)<TDataType\>

The predicate passes to the focus-finding logic that uses for performing the matching.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if successfully grabs the focus; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_GrabFocus__1_GodotViews_VirtualGrid_IPredicateDataFocusFinder_System_Func__0___0_System_Boolean____0_"></a> GrabFocus<TExtraArgument\>\(IPredicateDataFocusFinder, Func<TDataType, TExtraArgument, bool\>, TExtraArgument\)

Trying to create a focus on the element based on the specified <xref href="GodotViews.VirtualGrid.IPredicateDataFocusFinder" data-throw-if-not-resolved="false"></xref>.<br />

```csharp
bool GrabFocus<TExtraArgument>(IPredicateDataFocusFinder focusFinder, Func<TDataType, TExtraArgument, bool> predicate, TExtraArgument extraArgument)
```

#### Parameters

`focusFinder` [IPredicateDataFocusFinder](GodotViews.VirtualGrid.IPredicateDataFocusFinder.md)

The <xref href="GodotViews.VirtualGrid.IPredicateDataFocusFinder" data-throw-if-not-resolved="false"></xref> that provide the focus-finding logic.

`predicate` [Func](https://learn.microsoft.com/dotnet/api/system.func\-3)<TDataType, TExtraArgument, [bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>

The predicate passes to the focus-finding logic that uses for performing the matching.

`extraArgument` TExtraArgument

The predicate passes to the <code class="paramref">predicate</code> to avoid closure allocation.

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

<a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">true</a> if successfully grabs the focus; otherwise, <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/builtin-types/bool">false</a>.

#### Type Parameters

`TExtraArgument` 

### <a id="GodotViews_VirtualGrid_IVirtualGridView_1_Redraw"></a> Redraw\(\)

Triggers a redraw of the current viewport, reflecting the external changes to the datasets.

```csharp
void Redraw()
```

