# <a id="GodotViews_VirtualGrid"></a> Namespace GodotViews.VirtualGrid

### Main Interface

#### [IVirtualGridView<TDataType\>](GodotViews.VirtualGrid.IVirtualGridView\-1.md)

Represents a controller that provides feature to navigate through and customise the virtualized grid view.

### VirtualGridView Builder

#### [VirtualGridView](GodotViews.VirtualGrid.VirtualGridView.md)

Use the <xref href="GodotViews.VirtualGrid.VirtualGridView.Create(System.Int32%2cSystem.Int32)" data-throw-if-not-resolved="false"></xref> method to initiate a build process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.

#### [IViewHandlerBuilder](GodotViews.VirtualGrid.IViewHandlerBuilder.md)

The builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.<br />Use the <xref href="GodotViews.VirtualGrid.IViewHandlerBuilder.WithHandlers(GodotViews.VirtualGrid.IElementPositioner%2cGodotViews.VirtualGrid.IElementTweener%2cGodotViews.VirtualGrid.IElementFader)" data-throw-if-not-resolved="false"></xref> method to set up the visual transition behavior for the grid elements.

#### [IDataLayoutBuilder](GodotViews.VirtualGrid.IDataLayoutBuilder.md)

The builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.<br />Use the <xref href="GodotViews.VirtualGrid.IDataLayoutBuilder.WithHorizontalDataLayout%60%601(System.Collections.Generic.IEqualityComparer%7b%60%600%7d%2cSystem.Boolean)" data-throw-if-not-resolved="false"></xref> or the <xref href="GodotViews.VirtualGrid.IDataLayoutBuilder.WithVerticalDataLayout%60%601(System.Collections.Generic.IEqualityComparer%7b%60%600%7d%2cSystem.Boolean)" data-throw-if-not-resolved="false"></xref> method to choose between the layout of the data sets.

##### [IFinishingBuilderAccess<TDataType\>](GodotViews.VirtualGrid.IFinishingBuilderAccess\-1.md)

The builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.<br />Use the <xref href="GodotViews.VirtualGrid.IFinishingBuilderAccess%601.WithArgument%60%601(Godot.PackedScene%2cGodot.Control%2cGodotViews.VirtualGrid.IInfiniteLayoutGrid)" data-throw-if-not-resolved="false"></xref> or the <xref href="GodotViews.VirtualGrid.IFinishingBuilderAccess%601.WithArgument%60%602(Godot.PackedScene%2cGodot.Control%2cGodotViews.VirtualGrid.IInfiniteLayoutGrid)" data-throw-if-not-resolved="false"></xref> method
to pass in the necessary arguments.

##### [IHorizontalDataLayoutBuilder<TDataType\>](GodotViews.VirtualGrid.IHorizontalDataLayoutBuilder\-1.md)

The builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.<br />Use the <xref href="GodotViews.VirtualGrid.IHorizontalDataLayoutBuilder%601.AppendRowDataSet(GodotViews.VirtualGrid.IDynamicGridViewer%7b%600%7d%2cSystem.Int32)" data-throw-if-not-resolved="false"></xref> method to build up the horizontal datasets,
and call the <xref href="GodotViews.VirtualGrid.IFinishingBuilderAccess%601.WithArgument%60%602(Godot.PackedScene%2cGodot.Control%2cGodotViews.VirtualGrid.IInfiniteLayoutGrid)" data-throw-if-not-resolved="false"></xref> method when finished building the dataset.

##### [IVerticalDataLayoutBuilder<TDataType\>](GodotViews.VirtualGrid.IVerticalDataLayoutBuilder\-1.md)

The builder that continues the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.<br />Use the <xref href="GodotViews.VirtualGrid.IVerticalDataLayoutBuilder%601.AppendColumnDataSet(GodotViews.VirtualGrid.IDynamicGridViewer%7b%600%7d%2cSystem.Int32)" data-throw-if-not-resolved="false"></xref> method to build up the vertical datasets,
and call the <xref href="GodotViews.VirtualGrid.IFinishingBuilderAccess%601.WithArgument%60%602(Godot.PackedScene%2cGodot.Control%2cGodotViews.VirtualGrid.IInfiniteLayoutGrid)" data-throw-if-not-resolved="false"></xref> method when finished building the dataset.

#### [IFinishingArgumentBuilder<TDataType, TButtonType, TExtraArgument\>](GodotViews.VirtualGrid.IFinishingArgumentBuilder\-3.md)

The builder that concludes the building process of the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> instance.

### VirtualGridView Builder Dependencies

#### [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

A struct that used as a <xref href="System.Void" data-throw-if-not-resolved="false"></xref> type in extra arguments.

#### [Corners](GodotViews.VirtualGrid.Corners.md)

Provides a set of predefined <xref href="Godot.Vector2I" data-throw-if-not-resolved="false"></xref> for indicating the corners defined by the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>

#### [DynamicGridViewers](GodotViews.VirtualGrid.DynamicGridViewers.md)

Provides a built-in <xref href="GodotViews.VirtualGrid.IDynamicGridViewer%601" data-throw-if-not-resolved="false"></xref>.

#### [TweenSetups.EaseIn](GodotViews.VirtualGrid.TweenSetups.EaseIn.md)

Contains a set of <xref href="Godot.Tween.TransitionType" data-throw-if-not-resolved="false"></xref> with an ease type of <xref href="Godot.Tween.EaseType.In" data-throw-if-not-resolved="false"></xref>.

#### [TweenSetups.EaseInOut](GodotViews.VirtualGrid.TweenSetups.EaseInOut.md)

Contains a set of <xref href="Godot.Tween.TransitionType" data-throw-if-not-resolved="false"></xref> with an ease type of <xref href="Godot.Tween.EaseType.InOut" data-throw-if-not-resolved="false"></xref>.

#### [TweenSetups.EaseOut](GodotViews.VirtualGrid.TweenSetups.EaseOut.md)

Contains a set of <xref href="Godot.Tween.TransitionType" data-throw-if-not-resolved="false"></xref> with an ease type of <xref href="Godot.Tween.EaseType.Out" data-throw-if-not-resolved="false"></xref>.

#### [ElementFaders](GodotViews.VirtualGrid.ElementFaders.md)

Provides a set of built-in <xref href="GodotViews.VirtualGrid.IElementFader" data-throw-if-not-resolved="false"></xref> that should cover common UI/UX development needs.

#### [ElementPositioners](GodotViews.VirtualGrid.ElementPositioners.md)

Provides a set of built-in <xref href="GodotViews.VirtualGrid.IElementPositioner" data-throw-if-not-resolved="false"></xref> that should cover common UI/UX development needs.

#### [ElementTweeners](GodotViews.VirtualGrid.ElementTweeners.md)

Provides a set of built-in <xref href="GodotViews.VirtualGrid.IElementTweener" data-throw-if-not-resolved="false"></xref> that should cover common UI/UX development needs.

#### [FocusFinders](GodotViews.VirtualGrid.FocusFinders.md)

Provides a set of predefined focus finders for handling the focus finding logic.

#### [FocusPresets](GodotViews.VirtualGrid.FocusPresets.md)

Provides a set of predefined focus presets that cover common grab focus requirements.

#### [GodotTweenCoreBasedElementFader<TCachedArgument\>](GodotViews.VirtualGrid.GodotTweenCoreBasedElementFader\-1.md)

This implementation of the <xref href="GodotViews.VirtualGrid.IElementFader" data-throw-if-not-resolved="false"></xref> leverages the <xref href="Godot.Tween" data-throw-if-not-resolved="false"></xref> system for handling transitions,
this type handles necessary initialization and interruptions, and leaves blank the actual tween logic abstract.
The developer may inherit this type for creating their customized element fader.

#### [GodotTweenCoreBasedElementTweener<TCachedArgument\>](GodotViews.VirtualGrid.GodotTweenCoreBasedElementTweener\-1.md)

This implementation of the <xref href="GodotViews.VirtualGrid.IElementTweener" data-throw-if-not-resolved="false"></xref> leverages the <xref href="Godot.Tween" data-throw-if-not-resolved="false"></xref> system for handling transitions,
this type handles necessary initialization and interruptions, and leaves blank the actual tween logic abstract.
The developer may inherit this type for creating their customized element tweener.

#### [GodotTweenCoreBasedScrollBarTweener<TCachedArgument\>](GodotViews.VirtualGrid.GodotTweenCoreBasedScrollBarTweener\-1.md)

This implementation of the <xref href="GodotViews.VirtualGrid.IScrollBarTweener" data-throw-if-not-resolved="false"></xref> leverages the <xref href="Godot.Tween" data-throw-if-not-resolved="false"></xref> system for handling transitions,
this type handles necessary initialization and interruptions, and leaves blank the actual tween logic abstract.
The developer may inherit this type for creating their customized scroll bar tweener.

#### [InfiniteLayoutGrids](GodotViews.VirtualGrid.InfiniteLayoutGrids.md)

Provides a built-in <xref href="GodotViews.VirtualGrid.IInfiniteLayoutGrid" data-throw-if-not-resolved="false"></xref>.

#### [VirtualGridViewItem<TDataType\>.MethodName](GodotViews.VirtualGrid.VirtualGridViewItem\-1.MethodName.md)

#### [VirtualGridViewItem<TDataType, TExtraArgument\>.MethodName](GodotViews.VirtualGrid.VirtualGridViewItem\-2.MethodName.md)

#### [VirtualGridViewItem<TDataType, TExtraArgument\>.PropertyName](GodotViews.VirtualGrid.VirtualGridViewItem\-2.PropertyName.md)

#### [VirtualGridViewItem<TDataType\>.PropertyName](GodotViews.VirtualGrid.VirtualGridViewItem\-1.PropertyName.md)

#### [ScrollBarTweeners](GodotViews.VirtualGrid.ScrollBarTweeners.md)

Provides a set of built-in <xref href="GodotViews.VirtualGrid.IScrollBarTweener" data-throw-if-not-resolved="false"></xref> that should cover common UI/UX development needs.

#### [SearchDirections](GodotViews.VirtualGrid.SearchDirections.md)

Provides a set of predefined <xref href="GodotViews.VirtualGrid.SearchDirection" data-throw-if-not-resolved="false"></xref> for indicating the search directions
to be used with <xref href="GodotViews.VirtualGrid.FocusPresets" data-throw-if-not-resolved="false"></xref> or <xref href="GodotViews.VirtualGrid.FocusFinders" data-throw-if-not-resolved="false"></xref>.

#### [VirtualGridViewItem<TDataType\>.SignalName](GodotViews.VirtualGrid.VirtualGridViewItem\-1.SignalName.md)

#### [VirtualGridViewItem<TDataType, TExtraArgument\>.SignalName](GodotViews.VirtualGrid.VirtualGridViewItem\-2.SignalName.md)

#### [StartHandlers](GodotViews.VirtualGrid.StartHandlers.md)

Provides a set of predefined start handlers for obtaining the start position of the focus finding logic.

#### [TweenSetups](GodotViews.VirtualGrid.TweenSetups.md)

Provides a set of <xref href="GodotViews.VirtualGrid.TweenSetup" data-throw-if-not-resolved="false"></xref> that should cover common UI/UX development needs,
you may check their visualization from the https://easings.net/ website.

#### [VirtualGridViewItem<TDataType\>](GodotViews.VirtualGrid.VirtualGridViewItem\-1.md)

Inherit this type to create a script that can be attached to a <xref href="Godot.PackedScene" data-throw-if-not-resolved="false"></xref>
which makes it a valid prefab for use with <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.

#### [VirtualGridViewItem<TDataType, TExtraArgument\>](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md)

Inherit this type to create a script that can be attached to a <xref href="Godot.PackedScene" data-throw-if-not-resolved="false"></xref>
which makes it a valid prefab for use with <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.

### Structs

#### [VirtualGridViewItem<TDataType, TExtraArgument\>.CellInfo](GodotViews.VirtualGrid.VirtualGridViewItem\-2.CellInfo.md)

Stores the associated info assigned to the current virtualized grid element.

#### [DataFocusFinderPreset<TArgument\>](GodotViews.VirtualGrid.DataFocusFinderPreset\-1.md)

A focus finder preset contains a set of predefined combinations 
of arguments to simplify the arguments required to pass into
the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for the developers.

#### [DataFocusFinderPreset](GodotViews.VirtualGrid.DataFocusFinderPreset.md)

A focus finder preset contains a set of predefined combinations 
of arguments to simplify the arguments required to pass into
the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for the developers.

#### [ReadOnlyDataArray<TDataType\>](GodotViews.VirtualGrid.ReadOnlyDataArray\-1.md)

Allow the developer to indirectly access the content of the datasets.

#### [ReadOnlyViewArray](GodotViews.VirtualGrid.ReadOnlyViewArray.md)

Allow the developer to indirectly access the populate state of the current viewport.

#### [SearchDirection](GodotViews.VirtualGrid.SearchDirection.md)

Represents the search direction passed to the <xref href="GodotViews.VirtualGrid.FocusFinders" data-throw-if-not-resolved="false"></xref>.

#### [TweenSetup](GodotViews.VirtualGrid.TweenSetup.md)

The combination of <xref href="GodotViews.VirtualGrid.TweenSetup.EaseType" data-throw-if-not-resolved="false"></xref> and <xref href="GodotViews.VirtualGrid.TweenSetup.TransitionType" data-throw-if-not-resolved="false"></xref>.

#### [ViewFocusFinderPreset](GodotViews.VirtualGrid.ViewFocusFinderPreset.md)

A focus finder preset contains a set of predefined combinations 
of arguments to simplify the arguments required to pass into
the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for the developers.

#### [ViewFocusFinderPreset<TArgument\>](GodotViews.VirtualGrid.ViewFocusFinderPreset\-1.md)

A focus finder preset contains a set of predefined combinations 
of arguments to simplify the arguments required to pass into
the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>
for the developers.

### Interfaces

#### [IDataFocusFinder<TArgument\>](GodotViews.VirtualGrid.IDataFocusFinder\-1.md)

Data Focus Finder is the abstraction of the focus finding algorithm that
calculates the target focus coordinate based on the elements in the datasets.


#### [IDataStartHandler<TArgument\>](GodotViews.VirtualGrid.IDataStartHandler\-1.md)

Data Start Handler is responsible for resolving the start position from the datasets when finding focus.

#### [IDynamicGridViewer<T\>](GodotViews.VirtualGrid.IDynamicGridViewer\-1.md)

Dynamic Grid Viewer is the abstraction of a 2D list with one fixed dimension,
it can also be used for emulating a 2D view from a regular list.<br />
You may create an instance of the dynamic grid viewer from the <xref href="GodotViews.VirtualGrid.DynamicGridViewers" data-throw-if-not-resolved="false"></xref> class.

#### [IElementFader](GodotViews.VirtualGrid.IElementFader.md)

Element Fader is responsible for managing the hiding and showing of the virtualized elements.<br />
You may access a set of built-in element faders from the <xref href="GodotViews.VirtualGrid.ElementFaders" data-throw-if-not-resolved="false"></xref> class.

#### [IElementPositioner](GodotViews.VirtualGrid.IElementPositioner.md)

Element Positioner is responsible for calculating the positioning of the virtual viewport when focusing on a grid element.<br />
You may access a set of built-in element positioners from the <xref href="GodotViews.VirtualGrid.ElementPositioners" data-throw-if-not-resolved="false"></xref> class.

#### [IElementTweener](GodotViews.VirtualGrid.IElementTweener.md)

Element Tweener is responsible for managing the visual positional interpolation of the elements when user moves the virtualized viewport.<br />
You may access a set of built-in element tweeners from the <xref href="GodotViews.VirtualGrid.ElementTweeners" data-throw-if-not-resolved="false"></xref> class.

#### [IEqualityDataFocusFinder](GodotViews.VirtualGrid.IEqualityDataFocusFinder.md)

Equality Data Focus Finder is the abstraction of the focus finding algorithm that
calculates the target focus coordinate based on matching the elements in the datasets.


#### [IGodotTween](GodotViews.VirtualGrid.IGodotTween.md)

Representing the transition controller that's implemented
by the Godot's built-in <xref href="Godot.Tween" data-throw-if-not-resolved="false"></xref> system.

#### [IGodotTweenFader](GodotViews.VirtualGrid.IGodotTweenFader.md)

Representing the <xref href="GodotViews.VirtualGrid.IElementFader" data-throw-if-not-resolved="false"></xref> that's implemented
by the Godot's built-in <xref href="Godot.Tween" data-throw-if-not-resolved="false"></xref> system.

#### [IGodotTweenScrollBarTweener](GodotViews.VirtualGrid.IGodotTweenScrollBarTweener.md)

Representing the <xref href="GodotViews.VirtualGrid.IScrollBarTweener" data-throw-if-not-resolved="false"></xref> that's implemented
by the Godot's built-in <xref href="Godot.Tween" data-throw-if-not-resolved="false"></xref> system.

#### [IGodotTweenTweener](GodotViews.VirtualGrid.IGodotTweenTweener.md)

Representing the <xref href="GodotViews.VirtualGrid.IElementTweener" data-throw-if-not-resolved="false"></xref> that's implemented
by the Godot's built-in <xref href="Godot.Tween" data-throw-if-not-resolved="false"></xref> system.

#### [IInfiniteLayoutGrid](GodotViews.VirtualGrid.IInfiniteLayoutGrid.md)

Infinite Layout Grid is the abstraction of a layout grid that has infinite amount of cells.<br />
You may create an instance of the dynamic grid viewer from the <xref href="GodotViews.VirtualGrid.InfiniteLayoutGrids" data-throw-if-not-resolved="false"></xref> class.

#### [IPredicateDataFocusFinder](GodotViews.VirtualGrid.IPredicateDataFocusFinder.md)

Predicate Data Focus Finder is the abstraction of the focus finding algorithm that
calculates the target focus coordinate based on the matching the elements
by the custom predicate in the datasets.

#### [IScrollBarTweener](GodotViews.VirtualGrid.IScrollBarTweener.md)

ScrollBar Tweener is responsible for managing the <xref href="Godot.Range.Value" data-throw-if-not-resolved="false"></xref> and <xref href="Godot.Range.Page" data-throw-if-not-resolved="false"></xref>
value interpolation of the <xref href="Godot.ScrollBar" data-throw-if-not-resolved="false"></xref> when user moves the virtualized viewport.<br />
You may access a set of built-in element tweeners from the <xref href="GodotViews.VirtualGrid.ScrollBarTweeners" data-throw-if-not-resolved="false"></xref> class.


#### [IViewFocusFinder<TArgument\>](GodotViews.VirtualGrid.IViewFocusFinder\-1.md)

View Focus Finder is the abstraction of the focus finding algorithm that
calculates the target focus coordinate based on the elements in the current viewport.

#### [IViewStartHandler<TArgument\>](GodotViews.VirtualGrid.IViewStartHandler\-1.md)

View Start Handler is responsible for resolving the start position from the viewport when finding focus.

### Enums

#### [EdgeType](GodotViews.VirtualGrid.EdgeType.md)

Defines the edge type of the current virtualized grid element.

#### [GodotTweenCoreBasedElementFader<TCachedArgument\>.FadeType](GodotViews.VirtualGrid.GodotTweenCoreBasedElementFader\-1.FadeType.md)

The interpolation type that's going to execute.

#### [MoveDirection](GodotViews.VirtualGrid.MoveDirection.md)

Represent the move direction.

#### [GodotTweenCoreBasedElementTweener<TCachedArgument\>.TweenType](GodotViews.VirtualGrid.GodotTweenCoreBasedElementTweener\-1.TweenType.md)

The interpolation type that's going to execute.

