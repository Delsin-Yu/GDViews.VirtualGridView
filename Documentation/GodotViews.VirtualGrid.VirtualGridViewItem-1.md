# <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1"></a> Class VirtualGridViewItem<TDataType\>

Namespace: [GodotViews.VirtualGrid](GodotViews.VirtualGrid.md)  
Assembly: GDViews.VirtualGridView.dll  

Inherit this type to create a script that can be attached to a <xref href="Godot.PackedScene" data-throw-if-not-resolved="false"></xref>
which makes it a valid prefab for use with <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref>.

```csharp
public abstract class VirtualGridViewItem<TDataType> : VirtualGridViewItem<TDataType, NoExtraArgument>, IDisposable
```

#### Type Parameters

`TDataType` 

The type for the data the <xref href="GodotViews.VirtualGrid.IVirtualGridView%601" data-throw-if-not-resolved="false"></xref> focuses on.

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
GodotObject ← 
Node ← 
CanvasItem ← 
Control ← 
BaseButton ← 
Button ← 
[VirtualGridViewItem<TDataType, NoExtraArgument\>](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md) ← 
[VirtualGridViewItem<TDataType\>](GodotViews.VirtualGrid.VirtualGridViewItem\-1.md)

#### Implements

[IDisposable](https://learn.microsoft.com/dotnet/api/system.idisposable)

#### Inherited Members

[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_GuiInput\(InputEvent\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_GuiInput\_Godot\_InputEvent\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.TryGetInfo\(out VirtualGridViewItem<TDataType, NoExtraArgument\>.CellInfo\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_TryGetInfo\_GodotViews\_VirtualGrid\_VirtualGridViewItem\_\_0\_\_1\_\_CellInfo\_\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_Pressed\(\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_Pressed), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_Notification\(int\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_Notification\_System\_Int32\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnGuiInput\(InputEvent\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnGuiInput\_Godot\_InputEvent\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnNotification\(int\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnNotification\_System\_Int32\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnGridItemCreate\(\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnGridItemCreate), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnGridItemDraw\(TDataType, Vector2I, NoExtraArgument\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnGridItemDraw\_\_0\_Godot\_Vector2I\_\_1\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnGridItemMove\(TDataType, Vector2I, NoExtraArgument\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnGridItemMove\_\_0\_Godot\_Vector2I\_\_1\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnGridItemMoveIn\(TDataType, Vector2I, NoExtraArgument\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnGridItemMoveIn\_\_0\_Godot\_Vector2I\_\_1\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnGridItemMoveOut\(TDataType, Vector2I, NoExtraArgument\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnGridItemMoveOut\_\_0\_Godot\_Vector2I\_\_1\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnGridItemAppear\(TDataType, Vector2I, NoExtraArgument\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnGridItemAppear\_\_0\_Godot\_Vector2I\_\_1\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnGridItemDisapper\(NoExtraArgument\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnGridItemDisapper\_\_1\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnGridItemFocusEntered\(TDataType, Vector2I, NoExtraArgument\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnGridItemFocusEntered\_\_0\_Godot\_Vector2I\_\_1\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnGridItemFocusExited\(TDataType, Vector2I, NoExtraArgument\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnGridItemFocusExited\_\_0\_Godot\_Vector2I\_\_1\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.\_OnGridItemPressed\(TDataType, Vector2I, NoExtraArgument\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_\_OnGridItemPressed\_\_0\_Godot\_Vector2I\_\_1\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.SetGodotClassPropertyValue\(in godot\_string\_name, in godot\_variant\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_SetGodotClassPropertyValue\_Godot\_NativeInterop\_godot\_string\_name\_\_Godot\_NativeInterop\_godot\_variant\_\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.GetGodotClassPropertyValue\(in godot\_string\_name, out godot\_variant\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_GetGodotClassPropertyValue\_Godot\_NativeInterop\_godot\_string\_name\_\_Godot\_NativeInterop\_godot\_variant\_\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.InvokeGodotClassMethod\(in godot\_string\_name, NativeVariantPtrArgs, out godot\_variant\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_InvokeGodotClassMethod\_Godot\_NativeInterop\_godot\_string\_name\_\_Godot\_NativeInterop\_NativeVariantPtrArgs\_Godot\_NativeInterop\_godot\_variant\_\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.HasGodotClassMethod\(in godot\_string\_name\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_HasGodotClassMethod\_Godot\_NativeInterop\_godot\_string\_name\_\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.SaveGodotObjectData\(GodotSerializationInfo\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_SaveGodotObjectData\_Godot\_Bridge\_GodotSerializationInfo\_), 
[VirtualGridViewItem<TDataType, NoExtraArgument\>.RestoreGodotObjectData\(GodotSerializationInfo\)](GodotViews.VirtualGrid.VirtualGridViewItem\-2.md\#GodotViews\_VirtualGrid\_VirtualGridViewItem\_2\_RestoreGodotObjectData\_Godot\_Bridge\_GodotSerializationInfo\_), 
Button.InvokeGodotClassMethod\(in godot\_string\_name, NativeVariantPtrArgs, out godot\_variant\), 
Button.HasGodotClassMethod\(in godot\_string\_name\), 
Button.HasGodotClassSignal\(in godot\_string\_name\), 
Button.Text, 
Button.Icon, 
Button.Flat, 
Button.Alignment, 
Button.TextOverrunBehavior, 
Button.ClipText, 
Button.IconAlignment, 
Button.VerticalIconAlignment, 
Button.ExpandIcon, 
Button.TextDirection, 
Button.Language, 
BaseButton.\_Pressed\(\), 
BaseButton.\_Toggled\(bool\), 
BaseButton.SetPressedNoSignal\(bool\), 
BaseButton.IsHovered\(\), 
BaseButton.GetDrawMode\(\), 
BaseButton.InvokeGodotClassMethod\(in godot\_string\_name, NativeVariantPtrArgs, out godot\_variant\), 
BaseButton.HasGodotClassMethod\(in godot\_string\_name\), 
BaseButton.HasGodotClassSignal\(in godot\_string\_name\), 
BaseButton.Disabled, 
BaseButton.ToggleMode, 
BaseButton.ButtonPressed, 
BaseButton.ActionMode, 
BaseButton.ButtonMask, 
BaseButton.KeepPressedOutside, 
BaseButton.ButtonGroup, 
BaseButton.Shortcut, 
BaseButton.ShortcutFeedback, 
BaseButton.ShortcutInTooltip, 
BaseButton.Pressed, 
BaseButton.ButtonUp, 
BaseButton.ButtonDown, 
BaseButton.Toggled, 
Control.NotificationResized, 
Control.NotificationMouseEnter, 
Control.NotificationMouseExit, 
Control.NotificationFocusEnter, 
Control.NotificationFocusExit, 
Control.NotificationThemeChanged, 
Control.NotificationScrollBegin, 
Control.NotificationScrollEnd, 
Control.NotificationLayoutDirectionChanged, 
Control.\_CanDropData\(Vector2, Variant\), 
Control.\_DropData\(Vector2, Variant\), 
Control.\_GetDragData\(Vector2\), 
Control.\_GetMinimumSize\(\), 
Control.\_GetTooltip\(Vector2\), 
Control.\_GuiInput\(InputEvent\), 
Control.\_HasPoint\(Vector2\), 
Control.\_MakeCustomTooltip\(string\), 
Control.\_StructuredTextParser\(Array, string\), 
Control.AcceptEvent\(\), 
Control.GetMinimumSize\(\), 
Control.GetCombinedMinimumSize\(\), 
Control.SetAnchorsPreset\(Control.LayoutPreset, bool\), 
Control.SetOffsetsPreset\(Control.LayoutPreset, Control.LayoutPresetMode, int\), 
Control.SetAnchorsAndOffsetsPreset\(Control.LayoutPreset, Control.LayoutPresetMode, int\), 
Control.SetAnchor\(Side, float, bool, bool\), 
Control.SetAnchorAndOffset\(Side, float, float, bool\), 
Control.SetBegin\(Vector2\), 
Control.SetEnd\(Vector2\), 
Control.SetPosition\(Vector2, bool\), 
Control.SetSize\(Vector2, bool\), 
Control.ResetSize\(\), 
Control.SetGlobalPosition\(Vector2, bool\), 
Control.GetBegin\(\), 
Control.GetEnd\(\), 
Control.GetParentAreaSize\(\), 
Control.GetScreenPosition\(\), 
Control.GetRect\(\), 
Control.GetGlobalRect\(\), 
Control.HasFocus\(\), 
Control.GrabFocus\(\), 
Control.ReleaseFocus\(\), 
Control.FindPrevValidFocus\(\), 
Control.FindNextValidFocus\(\), 
Control.BeginBulkThemeOverride\(\), 
Control.EndBulkThemeOverride\(\), 
Control.AddThemeIconOverride\(StringName, Texture2D\), 
Control.AddThemeStyleboxOverride\(StringName, StyleBox\), 
Control.AddThemeFontOverride\(StringName, Font\), 
Control.AddThemeFontSizeOverride\(StringName, int\), 
Control.AddThemeColorOverride\(StringName, Color\), 
Control.AddThemeConstantOverride\(StringName, int\), 
Control.RemoveThemeIconOverride\(StringName\), 
Control.RemoveThemeStyleboxOverride\(StringName\), 
Control.RemoveThemeFontOverride\(StringName\), 
Control.RemoveThemeFontSizeOverride\(StringName\), 
Control.RemoveThemeColorOverride\(StringName\), 
Control.RemoveThemeConstantOverride\(StringName\), 
Control.GetThemeIcon\(StringName, StringName\), 
Control.GetThemeStylebox\(StringName, StringName\), 
Control.GetThemeFont\(StringName, StringName\), 
Control.GetThemeFontSize\(StringName, StringName\), 
Control.GetThemeColor\(StringName, StringName\), 
Control.GetThemeConstant\(StringName, StringName\), 
Control.HasThemeIconOverride\(StringName\), 
Control.HasThemeStyleboxOverride\(StringName\), 
Control.HasThemeFontOverride\(StringName\), 
Control.HasThemeFontSizeOverride\(StringName\), 
Control.HasThemeColorOverride\(StringName\), 
Control.HasThemeConstantOverride\(StringName\), 
Control.HasThemeIcon\(StringName, StringName\), 
Control.HasThemeStylebox\(StringName, StringName\), 
Control.HasThemeFont\(StringName, StringName\), 
Control.HasThemeFontSize\(StringName, StringName\), 
Control.HasThemeColor\(StringName, StringName\), 
Control.HasThemeConstant\(StringName, StringName\), 
Control.GetThemeDefaultBaseScale\(\), 
Control.GetThemeDefaultFont\(\), 
Control.GetThemeDefaultFontSize\(\), 
Control.GetParentControl\(\), 
Control.GetTooltip\(Vector2?\), 
Control.GetCursorShape\(Vector2?\), 
Control.ForceDrag\(Variant, Control\), 
Control.GrabClickFocus\(\), 
Control.SetDragForwarding\(Callable, Callable, Callable\), 
Control.SetDragPreview\(Control\), 
Control.IsDragSuccessful\(\), 
Control.WarpMouse\(Vector2\), 
Control.UpdateMinimumSize\(\), 
Control.IsLayoutRtl\(\), 
Control.InvokeGodotClassMethod\(in godot\_string\_name, NativeVariantPtrArgs, out godot\_variant\), 
Control.HasGodotClassMethod\(in godot\_string\_name\), 
Control.HasGodotClassSignal\(in godot\_string\_name\), 
Control.ClipContents, 
Control.CustomMinimumSize, 
Control.LayoutDirection, 
Control.LayoutMode, 
Control.AnchorsPreset, 
Control.AnchorLeft, 
Control.AnchorTop, 
Control.AnchorRight, 
Control.AnchorBottom, 
Control.OffsetLeft, 
Control.OffsetTop, 
Control.OffsetRight, 
Control.OffsetBottom, 
Control.GrowHorizontal, 
Control.GrowVertical, 
Control.Size, 
Control.Position, 
Control.GlobalPosition, 
Control.Rotation, 
Control.RotationDegrees, 
Control.Scale, 
Control.PivotOffset, 
Control.SizeFlagsHorizontal, 
Control.SizeFlagsVertical, 
Control.SizeFlagsStretchRatio, 
Control.AutoTranslate, 
Control.LocalizeNumeralSystem, 
Control.TooltipText, 
Control.FocusNeighborLeft, 
Control.FocusNeighborTop, 
Control.FocusNeighborRight, 
Control.FocusNeighborBottom, 
Control.FocusNext, 
Control.FocusPrevious, 
Control.FocusMode, 
Control.MouseFilter, 
Control.MouseForcePassScrollEvents, 
Control.MouseDefaultCursorShape, 
Control.ShortcutContext, 
Control.Theme, 
Control.ThemeTypeVariation, 
Control.Resized, 
Control.GuiInput, 
Control.MouseEntered, 
Control.MouseExited, 
Control.FocusEntered, 
Control.FocusExited, 
Control.SizeFlagsChanged, 
Control.MinimumSizeChanged, 
Control.ThemeChanged, 
CanvasItem.NotificationTransformChanged, 
CanvasItem.NotificationLocalTransformChanged, 
CanvasItem.NotificationDraw, 
CanvasItem.NotificationVisibilityChanged, 
CanvasItem.NotificationEnterCanvas, 
CanvasItem.NotificationExitCanvas, 
CanvasItem.NotificationWorld2DChanged, 
CanvasItem.\_Draw\(\), 
CanvasItem.GetCanvasItem\(\), 
CanvasItem.IsVisibleInTree\(\), 
CanvasItem.Show\(\), 
CanvasItem.Hide\(\), 
CanvasItem.QueueRedraw\(\), 
CanvasItem.MoveToFront\(\), 
CanvasItem.DrawLine\(Vector2, Vector2, Color, float, bool\), 
CanvasItem.DrawDashedLine\(Vector2, Vector2, Color, float, float, bool\), 
CanvasItem.DrawPolyline\(Vector2\[\], Color, float, bool\), 
CanvasItem.DrawPolylineColors\(Vector2\[\], Color\[\], float, bool\), 
CanvasItem.DrawArc\(Vector2, float, float, float, int, Color, float, bool\), 
CanvasItem.DrawMultiline\(Vector2\[\], Color, float\), 
CanvasItem.DrawMultilineColors\(Vector2\[\], Color\[\], float\), 
CanvasItem.DrawRect\(Rect2, Color, bool, float\), 
CanvasItem.DrawCircle\(Vector2, float, Color\), 
CanvasItem.DrawTexture\(Texture2D, Vector2, Color?\), 
CanvasItem.DrawTextureRect\(Texture2D, Rect2, bool, Color?, bool\), 
CanvasItem.DrawTextureRectRegion\(Texture2D, Rect2, Rect2, Color?, bool, bool\), 
CanvasItem.DrawMsdfTextureRectRegion\(Texture2D, Rect2, Rect2, Color?, double, double, double\), 
CanvasItem.DrawLcdTextureRectRegion\(Texture2D, Rect2, Rect2, Color?\), 
CanvasItem.DrawStyleBox\(StyleBox, Rect2\), 
CanvasItem.DrawPrimitive\(Vector2\[\], Color\[\], Vector2\[\], Texture2D\), 
CanvasItem.DrawPolygon\(Vector2\[\], Color\[\], Vector2\[\], Texture2D\), 
CanvasItem.DrawColoredPolygon\(Vector2\[\], Color, Vector2\[\], Texture2D\), 
CanvasItem.DrawString\(Font, Vector2, string, HorizontalAlignment, float, int, Color?, TextServer.JustificationFlag, TextServer.Direction, TextServer.Orientation\), 
CanvasItem.DrawMultilineString\(Font, Vector2, string, HorizontalAlignment, float, int, int, Color?, TextServer.LineBreakFlag, TextServer.JustificationFlag, TextServer.Direction, TextServer.Orientation\), 
CanvasItem.DrawStringOutline\(Font, Vector2, string, HorizontalAlignment, float, int, int, Color?, TextServer.JustificationFlag, TextServer.Direction, TextServer.Orientation\), 
CanvasItem.DrawMultilineStringOutline\(Font, Vector2, string, HorizontalAlignment, float, int, int, int, Color?, TextServer.LineBreakFlag, TextServer.JustificationFlag, TextServer.Direction, TextServer.Orientation\), 
CanvasItem.DrawChar\(Font, Vector2, string, int, Color?\), 
CanvasItem.DrawCharOutline\(Font, Vector2, string, int, int, Color?\), 
CanvasItem.DrawMesh\(Mesh, Texture2D, Transform2D?, Color?\), 
CanvasItem.DrawMultimesh\(MultiMesh, Texture2D\), 
CanvasItem.DrawSetTransform\(Vector2, float, Vector2?\), 
CanvasItem.DrawSetTransformMatrix\(Transform2D\), 
CanvasItem.DrawAnimationSlice\(double, double, double, double\), 
CanvasItem.DrawEndAnimation\(\), 
CanvasItem.GetTransform\(\), 
CanvasItem.GetGlobalTransform\(\), 
CanvasItem.GetGlobalTransformWithCanvas\(\), 
CanvasItem.GetViewportTransform\(\), 
CanvasItem.GetViewportRect\(\), 
CanvasItem.GetCanvasTransform\(\), 
CanvasItem.GetScreenTransform\(\), 
CanvasItem.GetLocalMousePosition\(\), 
CanvasItem.GetGlobalMousePosition\(\), 
CanvasItem.GetCanvas\(\), 
CanvasItem.GetWorld2D\(\), 
CanvasItem.SetNotifyLocalTransform\(bool\), 
CanvasItem.IsLocalTransformNotificationEnabled\(\), 
CanvasItem.SetNotifyTransform\(bool\), 
CanvasItem.IsTransformNotificationEnabled\(\), 
CanvasItem.ForceUpdateTransform\(\), 
CanvasItem.MakeCanvasPositionLocal\(Vector2\), 
CanvasItem.MakeInputLocal\(InputEvent\), 
CanvasItem.SetVisibilityLayerBit\(uint, bool\), 
CanvasItem.GetVisibilityLayerBit\(uint\), 
CanvasItem.InvokeGodotClassMethod\(in godot\_string\_name, NativeVariantPtrArgs, out godot\_variant\), 
CanvasItem.HasGodotClassMethod\(in godot\_string\_name\), 
CanvasItem.HasGodotClassSignal\(in godot\_string\_name\), 
CanvasItem.Visible, 
CanvasItem.Modulate, 
CanvasItem.SelfModulate, 
CanvasItem.ShowBehindParent, 
CanvasItem.TopLevel, 
CanvasItem.ClipChildren, 
CanvasItem.LightMask, 
CanvasItem.VisibilityLayer, 
CanvasItem.ZIndex, 
CanvasItem.ZAsRelative, 
CanvasItem.YSortEnabled, 
CanvasItem.TextureFilter, 
CanvasItem.TextureRepeat, 
CanvasItem.Material, 
CanvasItem.UseParentMaterial, 
CanvasItem.Draw, 
CanvasItem.VisibilityChanged, 
CanvasItem.Hidden, 
CanvasItem.ItemRectChanged, 
Node.NotificationEnterTree, 
Node.NotificationExitTree, 
Node.NotificationMovedInParent, 
Node.NotificationReady, 
Node.NotificationPaused, 
Node.NotificationUnpaused, 
Node.NotificationPhysicsProcess, 
Node.NotificationProcess, 
Node.NotificationParented, 
Node.NotificationUnparented, 
Node.NotificationSceneInstantiated, 
Node.NotificationDragBegin, 
Node.NotificationDragEnd, 
Node.NotificationPathRenamed, 
Node.NotificationChildOrderChanged, 
Node.NotificationInternalProcess, 
Node.NotificationInternalPhysicsProcess, 
Node.NotificationPostEnterTree, 
Node.NotificationDisabled, 
Node.NotificationEnabled, 
Node.NotificationNodeRecacheRequested, 
Node.NotificationEditorPreSave, 
Node.NotificationEditorPostSave, 
Node.NotificationWMMouseEnter, 
Node.NotificationWMMouseExit, 
Node.NotificationWMWindowFocusIn, 
Node.NotificationWMWindowFocusOut, 
Node.NotificationWMCloseRequest, 
Node.NotificationWMGoBackRequest, 
Node.NotificationWMSizeChanged, 
Node.NotificationWMDpiChange, 
Node.NotificationVpMouseEnter, 
Node.NotificationVpMouseExit, 
Node.NotificationOsMemoryWarning, 
Node.NotificationTranslationChanged, 
Node.NotificationWMAbout, 
Node.NotificationCrash, 
Node.NotificationOsImeUpdate, 
Node.NotificationApplicationResumed, 
Node.NotificationApplicationPaused, 
Node.NotificationApplicationFocusIn, 
Node.NotificationApplicationFocusOut, 
Node.NotificationTextServerChanged, 
Node.GetNode<T\>\(NodePath\), 
Node.GetNodeOrNull<T\>\(NodePath\), 
Node.GetChild<T\>\(int, bool\), 
Node.GetChildOrNull<T\>\(int, bool\), 
Node.GetOwner<T\>\(\), 
Node.GetOwnerOrNull<T\>\(\), 
Node.GetParent<T\>\(\), 
Node.GetParentOrNull<T\>\(\), 
Node.\_EnterTree\(\), 
Node.\_ExitTree\(\), 
Node.\_GetConfigurationWarnings\(\), 
Node.\_Input\(InputEvent\), 
Node.\_PhysicsProcess\(double\), 
Node.\_Process\(double\), 
Node.\_Ready\(\), 
Node.\_ShortcutInput\(InputEvent\), 
Node.\_UnhandledInput\(InputEvent\), 
Node.\_UnhandledKeyInput\(InputEvent\), 
Node.PrintOrphanNodes\(\), 
Node.AddSibling\(Node, bool\), 
Node.AddChild\(Node, bool, Node.InternalMode\), 
Node.RemoveChild\(Node\), 
Node.Reparent\(Node, bool\), 
Node.GetChildCount\(bool\), 
Node.GetChildren\(bool\), 
Node.GetChild\(int, bool\), 
Node.HasNode\(NodePath\), 
Node.GetNode\(NodePath\), 
Node.GetNodeOrNull\(NodePath\), 
Node.GetParent\(\), 
Node.FindChild\(string, bool, bool\), 
Node.FindChildren\(string, string, bool, bool\), 
Node.FindParent\(string\), 
Node.HasNodeAndResource\(NodePath\), 
Node.GetNodeAndResource\(NodePath\), 
Node.IsInsideTree\(\), 
Node.IsAncestorOf\(Node\), 
Node.IsGreaterThan\(Node\), 
Node.GetPath\(\), 
Node.GetPathTo\(Node, bool\), 
Node.AddToGroup\(StringName, bool\), 
Node.RemoveFromGroup\(StringName\), 
Node.IsInGroup\(StringName\), 
Node.MoveChild\(Node, int\), 
Node.GetGroups\(\), 
Node.GetIndex\(bool\), 
Node.PrintTree\(\), 
Node.PrintTreePretty\(\), 
Node.PropagateNotification\(int\), 
Node.PropagateCall\(StringName, Array, bool\), 
Node.SetPhysicsProcess\(bool\), 
Node.GetPhysicsProcessDeltaTime\(\), 
Node.IsPhysicsProcessing\(\), 
Node.GetProcessDeltaTime\(\), 
Node.SetProcess\(bool\), 
Node.IsProcessing\(\), 
Node.SetProcessInput\(bool\), 
Node.IsProcessingInput\(\), 
Node.SetProcessShortcutInput\(bool\), 
Node.IsProcessingShortcutInput\(\), 
Node.SetProcessUnhandledInput\(bool\), 
Node.IsProcessingUnhandledInput\(\), 
Node.SetProcessUnhandledKeyInput\(bool\), 
Node.IsProcessingUnhandledKeyInput\(\), 
Node.CanProcess\(\), 
Node.SetDisplayFolded\(bool\), 
Node.IsDisplayedFolded\(\), 
Node.SetProcessInternal\(bool\), 
Node.IsProcessingInternal\(\), 
Node.SetPhysicsProcessInternal\(bool\), 
Node.IsPhysicsProcessingInternal\(\), 
Node.GetWindow\(\), 
Node.GetLastExclusiveWindow\(\), 
Node.GetTree\(\), 
Node.CreateTween\(\), 
Node.Duplicate\(int\), 
Node.ReplaceBy\(Node, bool\), 
Node.SetSceneInstanceLoadPlaceholder\(bool\), 
Node.GetSceneInstanceLoadPlaceholder\(\), 
Node.SetEditableInstance\(Node, bool\), 
Node.IsEditableInstance\(Node\), 
Node.GetViewport\(\), 
Node.QueueFree\(\), 
Node.RequestReady\(\), 
Node.IsNodeReady\(\), 
Node.SetMultiplayerAuthority\(int, bool\), 
Node.GetMultiplayerAuthority\(\), 
Node.IsMultiplayerAuthority\(\), 
Node.RpcConfig\(StringName, Variant\), 
Node.Rpc\(StringName, params Variant\[\]\), 
Node.RpcId\(long, StringName, params Variant\[\]\), 
Node.UpdateConfigurationWarnings\(\), 
Node.CallDeferredThreadGroup\(StringName, params Variant\[\]\), 
Node.SetDeferredThreadGroup\(StringName, Variant\), 
Node.NotifyDeferredThreadGroup\(int\), 
Node.CallThreadSafe\(StringName, params Variant\[\]\), 
Node.SetThreadSafe\(StringName, Variant\), 
Node.NotifyThreadSafe\(int\), 
Node.InvokeGodotClassMethod\(in godot\_string\_name, NativeVariantPtrArgs, out godot\_variant\), 
Node.HasGodotClassMethod\(in godot\_string\_name\), 
Node.HasGodotClassSignal\(in godot\_string\_name\), 
Node.\_ImportPath, 
Node.Name, 
Node.UniqueNameInOwner, 
Node.SceneFilePath, 
Node.Owner, 
Node.Multiplayer, 
Node.ProcessMode, 
Node.ProcessPriority, 
Node.ProcessPhysicsPriority, 
Node.ProcessThreadGroup, 
Node.ProcessThreadGroupOrder, 
Node.ProcessThreadMessages, 
Node.EditorDescription, 
Node.Ready, 
Node.Renamed, 
Node.TreeEntered, 
Node.TreeExiting, 
Node.TreeExited, 
Node.ChildEnteredTree, 
Node.ChildExitingTree, 
Node.ChildOrderChanged, 
Node.ReplacingBy, 
GodotObject.NotificationPostinitialize, 
GodotObject.NotificationPredelete, 
GodotObject.InstanceFromId\(ulong\), 
GodotObject.IsInstanceIdValid\(ulong\), 
GodotObject.IsInstanceValid\(GodotObject\), 
GodotObject.WeakRef\(GodotObject\), 
GodotObject.Dispose\(\), 
GodotObject.Dispose\(bool\), 
GodotObject.ToString\(\), 
GodotObject.ToSignal\(GodotObject, StringName\), 
GodotObject.SetGodotClassPropertyValue\(in godot\_string\_name, in godot\_variant\), 
GodotObject.GetGodotClassPropertyValue\(in godot\_string\_name, out godot\_variant\), 
GodotObject.RaiseGodotClassSignalCallbacks\(in godot\_string\_name, NativeVariantPtrArgs\), 
GodotObject.SaveGodotObjectData\(GodotSerializationInfo\), 
GodotObject.RestoreGodotObjectData\(GodotSerializationInfo\), 
GodotObject.\_Get\(StringName\), 
GodotObject.\_GetPropertyList\(\), 
GodotObject.\_Notification\(int\), 
GodotObject.\_PropertyCanRevert\(StringName\), 
GodotObject.\_PropertyGetRevert\(StringName\), 
GodotObject.\_Set\(StringName, Variant\), 
GodotObject.Free\(\), 
GodotObject.GetClass\(\), 
GodotObject.IsClass\(string\), 
GodotObject.Set\(StringName, Variant\), 
GodotObject.Get\(StringName\), 
GodotObject.SetIndexed\(NodePath, Variant\), 
GodotObject.GetIndexed\(NodePath\), 
GodotObject.GetPropertyList\(\), 
GodotObject.GetMethodList\(\), 
GodotObject.PropertyCanRevert\(StringName\), 
GodotObject.PropertyGetRevert\(StringName\), 
GodotObject.Notification\(int, bool\), 
GodotObject.GetInstanceId\(\), 
GodotObject.SetScript\(Variant\), 
GodotObject.GetScript\(\), 
GodotObject.SetMeta\(StringName, Variant\), 
GodotObject.RemoveMeta\(StringName\), 
GodotObject.GetMeta\(StringName, Variant\), 
GodotObject.HasMeta\(StringName\), 
GodotObject.GetMetaList\(\), 
GodotObject.AddUserSignal\(string, Array\), 
GodotObject.HasUserSignal\(StringName\), 
GodotObject.EmitSignal\(StringName, params Variant\[\]\), 
GodotObject.Call\(StringName, params Variant\[\]\), 
GodotObject.CallDeferred\(StringName, params Variant\[\]\), 
GodotObject.SetDeferred\(StringName, Variant\), 
GodotObject.Callv\(StringName, Array\), 
GodotObject.HasMethod\(StringName\), 
GodotObject.HasSignal\(StringName\), 
GodotObject.GetSignalList\(\), 
GodotObject.GetSignalConnectionList\(StringName\), 
GodotObject.GetIncomingConnections\(\), 
GodotObject.Connect\(StringName, Callable, uint\), 
GodotObject.Disconnect\(StringName, Callable\), 
GodotObject.IsConnected\(StringName, Callable\), 
GodotObject.SetBlockSignals\(bool\), 
GodotObject.IsBlockingSignals\(\), 
GodotObject.NotifyPropertyListChanged\(\), 
GodotObject.SetMessageTranslation\(bool\), 
GodotObject.CanTranslateMessages\(\), 
GodotObject.Tr\(StringName, StringName\), 
GodotObject.TrN\(StringName, StringName, int, StringName\), 
GodotObject.IsQueuedForDeletion\(\), 
GodotObject.CancelFree\(\), 
GodotObject.InvokeGodotClassMethod\(in godot\_string\_name, NativeVariantPtrArgs, out godot\_variant\), 
GodotObject.HasGodotClassMethod\(in godot\_string\_name\), 
GodotObject.HasGodotClassSignal\(in godot\_string\_name\), 
GodotObject.NativeInstance, 
GodotObject.ScriptChanged, 
GodotObject.PropertyListChanged, 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1_HasGodotClassMethod_Godot_NativeInterop_godot_string_name__"></a> HasGodotClassMethod\(in godot\_string\_name\)

```csharp
protected override bool HasGodotClassMethod(in godot_string_name method)
```

#### Parameters

`method` godot\_string\_name

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1_InvokeGodotClassMethod_Godot_NativeInterop_godot_string_name__Godot_NativeInterop_NativeVariantPtrArgs_Godot_NativeInterop_godot_variant__"></a> InvokeGodotClassMethod\(in godot\_string\_name, NativeVariantPtrArgs, out godot\_variant\)

```csharp
protected override bool InvokeGodotClassMethod(in godot_string_name method, NativeVariantPtrArgs args, out godot_variant ret)
```

#### Parameters

`method` godot\_string\_name

`args` NativeVariantPtrArgs

`ret` godot\_variant

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1_RestoreGodotObjectData_Godot_Bridge_GodotSerializationInfo_"></a> RestoreGodotObjectData\(GodotSerializationInfo\)

```csharp
protected override void RestoreGodotObjectData(GodotSerializationInfo info)
```

#### Parameters

`info` GodotSerializationInfo

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1_SaveGodotObjectData_Godot_Bridge_GodotSerializationInfo_"></a> SaveGodotObjectData\(GodotSerializationInfo\)

```csharp
protected override void SaveGodotObjectData(GodotSerializationInfo info)
```

#### Parameters

`info` GodotSerializationInfo

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemAppear__0_Godot_Vector2I_GodotViews_VirtualGrid_NoExtraArgument_"></a> \_OnGridItemAppear\(TDataType, Vector2I, NoExtraArgument\)

Invoked when the view controller is showing this virtualized grid element instance.

```csharp
protected override sealed void _OnGridItemAppear(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

`extraArgument` [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

The extra argument passed to this virtualized grid element instance.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemAppear__0_Godot_Vector2I_"></a> \_OnGridItemAppear\(TDataType, Vector2I\)

Invoked when the view controller is showing this virtualized grid element instance.

```csharp
protected virtual void _OnGridItemAppear(TDataType data, Vector2I viewPosition)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemDisappear"></a> \_OnGridItemDisappear\(\)

Invoked when the view controller is hiding this virtualized grid element instance.

```csharp
protected virtual void _OnGridItemDisappear()
```

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemDisapper_GodotViews_VirtualGrid_NoExtraArgument_"></a> \_OnGridItemDisapper\(NoExtraArgument\)

Invoked when the view controller is hiding this virtualized grid element instance.

```csharp
protected override sealed void _OnGridItemDisapper(NoExtraArgument extraArgument)
```

#### Parameters

`extraArgument` [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

The extra argument passed to this virtualized grid element instance.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemDraw__0_Godot_Vector2I_GodotViews_VirtualGrid_NoExtraArgument_"></a> \_OnGridItemDraw\(TDataType, Vector2I, NoExtraArgument\)

Invoked when the internal data of the current virtualized grid element instance
has changed (or initialized) and requires developer-implemented draw logic.

```csharp
protected override sealed void _OnGridItemDraw(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

`extraArgument` [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

The extra argument passed to this virtualized grid element instance.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemDraw__0_Godot_Vector2I_"></a> \_OnGridItemDraw\(TDataType, Vector2I\)

Invoked when the internal data of the current virtualized grid element instance
has changed (or initialized) and requires developer-implemented draw logic.

```csharp
protected virtual void _OnGridItemDraw(TDataType data, Vector2I viewPosition)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemFocusEntered__0_Godot_Vector2I_GodotViews_VirtualGrid_NoExtraArgument_"></a> \_OnGridItemFocusEntered\(TDataType, Vector2I, NoExtraArgument\)

Invoked when this virtualized grid element instance grabs focus.

```csharp
protected override sealed void _OnGridItemFocusEntered(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

`extraArgument` [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

The extra argument passed to this virtualized grid element instance.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemFocusEntered__0_Godot_Vector2I_"></a> \_OnGridItemFocusEntered\(TDataType, Vector2I\)

Invoked when this virtualized grid element instance grabs focus.

```csharp
protected virtual void _OnGridItemFocusEntered(TDataType data, Vector2I viewPosition)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemFocusExited__0_Godot_Vector2I_GodotViews_VirtualGrid_NoExtraArgument_"></a> \_OnGridItemFocusExited\(TDataType, Vector2I, NoExtraArgument\)

Invoked when this virtualized grid element instance loses focus.

```csharp
protected override sealed void _OnGridItemFocusExited(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

`extraArgument` [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

The extra argument passed to this virtualized grid element instance.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemFocusExited__0_Godot_Vector2I_"></a> \_OnGridItemFocusExited\(TDataType, Vector2I\)

Invoked when this virtualized grid element instance loses focus.

```csharp
protected virtual void _OnGridItemFocusExited(TDataType data, Vector2I viewPosition)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemMove__0_Godot_Vector2I_GodotViews_VirtualGrid_NoExtraArgument_"></a> \_OnGridItemMove\(TDataType, Vector2I, NoExtraArgument\)

Invoked when the view controller is moving this virtualized grid element inside the viewport.

```csharp
protected override sealed void _OnGridItemMove(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

`extraArgument` [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

The extra argument passed to this virtualized grid element instance.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemMove__0_Godot_Vector2I_"></a> \_OnGridItemMove\(TDataType, Vector2I\)

Invoked when the view controller is moving this virtualized grid element inside the viewport.

```csharp
protected virtual void _OnGridItemMove(TDataType data, Vector2I viewPosition)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemMoveIn__0_Godot_Vector2I_GodotViews_VirtualGrid_NoExtraArgument_"></a> \_OnGridItemMoveIn\(TDataType, Vector2I, NoExtraArgument\)

Invoked when the view controller is moving this newly spawned or
reused virtualized grid element instance into the viewport.

```csharp
protected override sealed void _OnGridItemMoveIn(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

`extraArgument` [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

The extra argument passed to this virtualized grid element instance.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemMoveIn__0_Godot_Vector2I_"></a> \_OnGridItemMoveIn\(TDataType, Vector2I\)

Invoked when the view controller is moving this newly spawned or
reused virtualized grid element instance into the viewport.

```csharp
protected virtual void _OnGridItemMoveIn(TDataType data, Vector2I viewPosition)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemMoveOut__0_Godot_Vector2I_GodotViews_VirtualGrid_NoExtraArgument_"></a> \_OnGridItemMoveOut\(TDataType, Vector2I, NoExtraArgument\)

Invoked when the view controller is moving this
virtualized grid element instance out from the viewport.

```csharp
protected override sealed void _OnGridItemMoveOut(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

`extraArgument` [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

The extra argument passed to this virtualized grid element instance.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemMoveOut__0_Godot_Vector2I_"></a> \_OnGridItemMoveOut\(TDataType, Vector2I\)

Invoked when the view controller is moving this
virtualized grid element instance out from the viewport.

```csharp
protected virtual void _OnGridItemMoveOut(TDataType data, Vector2I viewPosition)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemPressed__0_Godot_Vector2I_GodotViews_VirtualGrid_NoExtraArgument_"></a> \_OnGridItemPressed\(TDataType, Vector2I, NoExtraArgument\)

Invoked when this virtualized grid element instance is pressed.

```csharp
protected override sealed void _OnGridItemPressed(TDataType data, Vector2I viewPosition, NoExtraArgument extraArgument)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

`extraArgument` [NoExtraArgument](GodotViews.VirtualGrid.NoExtraArgument.md)

The extra argument passed to this virtualized grid element instance.

### <a id="GodotViews_VirtualGrid_VirtualGridViewItem_1__OnGridItemPressed__0_Godot_Vector2I_"></a> \_OnGridItemPressed\(TDataType, Vector2I\)

Invoked when this virtualized grid element instance is pressed.

```csharp
protected virtual void _OnGridItemPressed(TDataType data, Vector2I viewPosition)
```

#### Parameters

`data` TDataType

The data of the current virtualized grid element instance.

`viewPosition` Vector2I

The position of this virtualized grid element instance in the viewport.

