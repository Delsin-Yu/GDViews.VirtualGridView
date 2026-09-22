# Changelog

## v2.0.0

### Breaking Changes

- **`_OnGuiInput` now receives the cell context.** Override `_OnGuiInput(InputEvent inputEvent, TDataType data, Vector2I viewPosition, TExtraArgument extraArgument)` instead of `_OnGuiInput(InputEvent)`.
- **`IVirtualGridView` is split.** The non-generic `IVirtualGridView` owns `IDisposable`. `IVirtualGridView<TDataType>` extends it.
- **`ExtraArgument` is non-nullable.** Item callbacks take `TExtraArgument` rather than `TExtraArgument?`.

### New Features

- **Dataset-edge focus.** `FocusEdgeBehavior` is `None`, `Clamped`, or `Looped`, set per axis with `SetFocusEdgeBehavior` or the `HorizontalFocusEdgeBehavior` / `VerticalFocusEdgeBehavior` properties.
- **Interactive scroll bars.** `ConfigureInteractiveHorizontalScrollBar` and `ConfigureInteractiveVerticalScrollBar` let the bar drive the viewport. Value tweening stays off in that mode.
- **Extra scroll surfaces.** `ConfigureAdditionalScrollInput` forwards the same wheel and drag handling to other controls.
- **Cell lookup.** `TryGetControlAtViewPosition` and `TryFindAssociatedControl` resolve a live cell control.
- **`FocusFinders.LastData`.** Selects the last occupied cell in the dataset.
- **View passthrough.** `Modulate` and `FocusBehaviorRecursive` are available on the grid view.
