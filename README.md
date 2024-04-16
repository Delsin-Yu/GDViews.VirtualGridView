# GDViews.VirtualGridView [Work in Progress]

[![GitHub Release](https://img.shields.io/github/v/release/Delsin-Yu/GDViews.VirtualGridView)](https://github.com/Delsin-Yu/GDViews.VirtualGridView/releases/latest) [![NuGet Version](https://img.shields.io/nuget/v/GDViews.VirtualGridView)](https://www.nuget.org/packages/GDViews.VirtualGridView) ![NuGet Downloads](https://img.shields.io/nuget/dt/GDViews.VirtualGridView) [![Stars](https://img.shields.io/github/stars/Delsin-Yu/GDViews.VirtualGridView?color=brightgreen)](https://github.com/Delsin-Yu/GDViews.VirtualGridView/stargazers) [![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/Delsin-Yu/GDViews.VirtualGridView/blob/main/LICENSE)

## Introduction

Supports in `Godot 4.1+` with .Net module.  
***GDViews.VirtualGridView*** is a `Godot 4` UI Component that provides classes that's useful for creating highly customizable, fully virtualized list/grid views.

## Installation

For .Net CLI

```txt
dotnet add package GDViews.VirtualGridView
```

For Package Manager Console

```txt
NuGet\Install-Package GDViews.VirtualGridView
```

For `csproj` PackageReference

```xml
<PackageReference Include="GDViews.VirtualGridView" Version="*" />
```

---

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
<!-- ## Table of Contents

- [Glossarys](#glossarys)
  - [`VirtualGridView / GridView`](#VirtualGridView--GridView)
  - [`VirtualGridViewItem / GridViewItem`](#VirtualGridViewitem--GridViewitem)
- [API Usage](#api-usage)
  - [Creating a `ViewItem`](#creating-a-viewitem)
    - [A Simple Example](#a-simple-example)
    - [A Complex Example](#a-complex-example)
  - [Creating a `GridView`](#creating-a-GridView)
    - [Create from existing `ViewItem` Instances](#create-from-existing-viewitem-instances)
    - [Create from `PackedScenes`](#create-from-packedscenes)
- [Component Documentation](#component-documentation)
  - [The `VirtualGridView`](#the-VirtualGridView)
    - [Static Factory Methods](#static-factory-methods)
      - [`VirtualGridView.CreateFromPrefab`](#VirtualGridViewcreatefromprefab)
      - [`VirtualGridView.CreateFromInstance`](#VirtualGridViewcreatefrominstance)
    - [Instance Methods](#instance-methods)
      - [`Show(int index)` / `Show(int index, object? optionalArg)`](#showint-index--showint-index-object-optionalarg)
      - [`ShowNext` / `ShowPrevious`](#shownext--showprevious)
    - [`ArgumentResolver`](#argumentresolver)
      - [Default Resolver](#default-resolver)
      - [Resolver for `ShowNext` / `ShowPrevious`](#resolver-for-shownext--showprevious)
  - [The `VirtualGridViewItem` / `VirtualGridViewItemT`](#the-VirtualGridViewitem--VirtualGridViewitemt)
    - [Event Methods Diagram](#event-methods-diagram)
  - [ViewItemTweeners](#viewitemtweeners)
    - [Built-in Tweeners](#built-in-tweeners)
    - [Customize Tweeners](#customize-tweeners) -->

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

---

<!-- ## Glossarys

### `VirtualGridView / GridView`

The C# type that controls a group of associated `GridViewItem` and handles the all the necessary item virtualization, event method invoking, input processing; it provides APIs that allow the developer to control the internal focused items, movement of virtual viewports and triggering redraws.

### `VirtualGridViewItem / GridViewItem`

The script(s) inheriting the or `VirtualGridViewItemT`, attaching the script to a control to make it a `GridViewItem`, this type contains all the necessary event method for customizing the visual behaviour of a `GridViewItem`, the developer should pass their implementation of `GridViewItem` PackedScene to the corresponding builder(mentioned below) when construct.

## API Usage

### Creating a `GridViewItem`

Attach the following script to a `Control` to make it a `GridViewItem`.  

#### A Simple Example

This view item displays the current system time.

```csharp
using Godot;
using GodotViews.FreeTab;

/// <summary>
/// Attach this script to a <see cref="Control"/> to make it a ViewItem.
/// </summary>
public partial class MyViewItem : VirtualGridViewItem
{
    [Export] private Label _text;

    public override void _Process(double delta)
    {
        base._Process(delta);
        _text.Text = Time.GetTimeStringFromSystem();
    }
}
```

#### A Complex Example

This view item displays `Hello World!` when shown, and shows `Click: Number` when clicking the `_pressButton`.

```csharp
using Godot;

/// <summary>
/// Attach this script to a <see cref="Control"/> to make it a ViewItem.
/// </summary>
public partial class MyViewItem2 : VirtualGridViewItem
{
    [Export] private Label _text;
    [Export] private Button _pressButton;

    private int _clickCount;
    
    /// <summary>
    /// Called when the <see cref="VirtualGridView"/> is initializing the view item.
    /// </summary>
    protected override void _OnViewItemInitialize()
    {
        _pressButton.Pressed += () => _text.Text = $"Clicked: {_clickCount++}";
    }

    /// <summary>
    /// Called when the <see cref="VirtualGridView"/> is showing the view item.
    /// </summary>
    protected override void _OnViewShow()
    {
        _text.Text = "Hello World!";
        _pressButton.GrabFocus();
    }
}
```

### Creating a `GridView`

The `VirtualGridView` is pure C# implementation, so instead of attaching a script to a node in the scene tree, developers need to create and use it in scripts, there are two ways for constructing a `VirtualGridView` instance.

#### Create from existing `ViewItem` Instances

For use cases where the developer wishes to instantiate their instance of `ViewItem`, or simply leave them in the scene tree, `VirtualGridView.CreateFromInstance` can be used to construct the `VirtualGridView`.

```csharp
using Godot;

/// <summary>
/// Attached to a node in scene tree.
/// </summary>
public partial class Main : Node
{
    // Assigned in Godot Editor, through inspector.
    [Export] private MyViewItem _viewItem1;
    [Export] private MyViewItem2 _viewItem2;

    [Export] private CheckButton _tab1;
    [Export] private CheckButton _tab2;

    private VirtualGridView _GridView;

    public override void _Ready()
    {
        // Construct a tab view on ready.
        _GridView = VirtualGridView.CreateFromInstance(
            [
                // Associate a tab to its corresponding view item instance.
                new TabInstanceSetup(_tab1, _viewItem1), 
                new TabInstanceSetup(_tab2, _viewItem2), 
            ]
        );
        
        // Make the tab view displays the first view item.
        _GridView.Show(0);
    }

    public override void _Process(double delta)
    {
        // Developer may use their own preferred way to handle switching between tabs.
        if (Input.IsActionJustPressed("ui_left")) _GridView.ShowPrevious();
        if (Input.IsActionJustPressed("ui_right")) _GridView.ShowNext();
    }
}
```

#### Create from `PackedScenes`

For use cases where the developer wishes to store the `ViewItem`s as `PackedScenes`, `VirtualGridView.CreateFromPrefab` can be used to construct the `VirtualGridView`.

```csharp
using Godot;

/// <summary>
/// Attached to a node in scene tree.
/// </summary>
public partial class Main : Node
{
    // Assigned in Godot Editor, through inspector.
    [Export] private PackedScene _viewItem1;
    [Export] private PackedScene _viewItem2;

    [Export] private CheckButton _tab1;
    [Export] private CheckButton _tab2;

    // Required for storing the instances.
    [Export] private Control _container;

    private VirtualGridView _GridView;

    public override void _Ready()
    {
        // Construct a tab view on ready.
        _GridView = VirtualGridView.CreateFromPrefab(
            [
                // Associate a tab to a instance for the provided packed scene.
                new TabPrefabSetup(_tab1, _viewItem1), 
                new TabPrefabSetup(_tab2, _viewItem2), 
            ],
            _container
        );
        
        // Make the tab view displays the first view item.
        _GridView.Show(0);
    }

    public override void _Process(double delta)
    {
        // Developer may use their own preferred way to handle switching between tabs.
        if (Input.IsActionJustPressed("ui_left")) _GridView.ShowPrevious();
        if (Input.IsActionJustPressed("ui_right")) _GridView.ShowNext();
    }
}
```

## Component Documentation

### The `VirtualGridView`

#### Static Factory Methods

Use factory functions to instantiate `VirtualGridViews`.

##### `VirtualGridView.CreateFromPrefab`

Create an instance of the `VirtualGridView` from the given `TabPrefabSetups`, this overload instantiates the given `PackedScenes` under the `viewsContainer`.

```csharp

[Export] private PackedScene _viewItem1;
[Export] private PackedScene _viewItem2;

[Export] private CheckButton _tab1;
[Export] private CheckButton _tab2;

_GridView = VirtualGridView.CreateFromPrefab(
    [
        new(_tab1, _viewItem1), 
        new(_tab2, _viewItem2), 
    ],
    _container
);
```

##### `VirtualGridView.CreateFromInstance`

Create an instance of the `VirtualGridView` from the given `TabPrefabSetups`, this overload references the given `IVirtualGridViewItems`.

```csharp

[Export] private PackedScene _viewItem1;
[Export] private PackedScene _viewItem2;

[Export] private CheckButton _tab1;
[Export] private CheckButton _tab2;

_GridView = VirtualGridView.CreateFromPrefab(
    [
        new(_tab1, _viewItem1), 
        new(_tab2, _viewItem2), 
    ],
    _container
);
```

#### Instance Methods

A `VirtualGridView` exposes four functions to the developer to switch between the `ViewItems`.

##### `Show(int index)` / `Show(int index, object? optionalArg)`

Shows a view item at the given index, the latter overload supports passing an optional argument to the target view item.

```csharp
// Shows the first view item.
_GridView.Show(0);

// Shows the second view item, 
// and pass the "Hello World" to its `_OnViewItemShow` method.
_GridView.Show(1, "Hello World");
```

##### `ShowNext` / `ShowPrevious`

Shows the next/previous view item. If no view item is shown at the moment, the first view item will be shown.  
The first argument determines if the `GridView` should warp to the first/last view item if the current shown view item is the last/first.

```csharp
// Shows the first view item.
_GridView.Show(0);

// Shows the previous view item.
_GridView.ShowPrevious();
_GridView.ShowNext();
```

#### `ArgumentResolver`

When using the `ShowNext`/`ShowPrevious` API or `CheckButtons` to switch between view items, it is hard to pass the argument to the displaying view items, in this case, the developer may pass an `argument resolver delegate` when constructing the `VirtualGridView` or to the `ShowNext`/`ShowPrevious` API.

##### Default Resolver

Passing a delegate with the following signature `Func<IVirtualGridViewItem, object?>` to the factory method as the default `ArgumentResolver`, this resolver gets called when calling `Show(0)`, `Show(0, null)`, `ShowPrevious()`, and `ShowNext()` API, the developer may write their logic to return the desired argument based on the given `IVirtualGridViewItem` instance.

```csharp
using Godot;

/// <summary>
/// Attached to a node in scene tree.
/// </summary>
public partial class Main : Node
{
    // Assigned in Godot Editor, through inspector.
    [Export] private MyViewItem _viewItem1;
    [Export] private MyViewItem2 _viewItem2;

    [Export] private CheckButton _tab1;
    [Export] private CheckButton _tab2;

    private VirtualGridView _GridView;

    public override void _Ready()
    {
        // Construct a tab view on ready.
        _GridView = VirtualGridView.CreateFromInstance(
            [
                // Associate a tab to its corresponding view item instance.
                new TabInstanceSetup(_tab1, _viewItem1), 
                new TabInstanceSetup(_tab2, _viewItem2), 
            ],
            ArgumentResolver
        );
        
        return;
        
        object? ArgumentResolver(IVirtualGridViewItem arg)
        {
            if (arg == _viewItem1) return "Hello World!";
            if (arg == _viewItem2) return 10;
            return null;
        }
    }
}
```

##### Resolver for `ShowNext` / `ShowPrevious`

Passing a delegate with the following signature `Func<IVirtualGridViewItem, object?>` to the `ShowPrevious()`, and `ShowNext()` API as the `ArgumentResolver`, the developer may write their logic to return the desired argument based on the given `IVirtualGridViewItem` instance. Passing null to these two APIs will fall back to the `default ArgumentResolver`.

```csharp
object? ArgumentResolver(IVirtualGridViewItem arg)
{
    if (arg == _viewItem1) return "Hello World!";
    if (arg == _viewItem2) return 10;
    return null;
}

_GridView.ShowPrevious(argumentResolver: ArgumentResolver);
_GridView.ShowNext(argumentResolver: ArgumentResolver);
```

### The `VirtualGridViewItem` / `VirtualGridViewItemT`

Inheriting the `VirtualGridViewItem` or `VirtualGridViewItemT` type, and attach the script to a `Control` node for it to work.

#### Event Methods Diagram

While working with `ViewItems`, certain methods get called at a certain lifetime of a view item, a brief diagram can be summarised as follows.

```mermaid
---
title: The Summary of Event Methods throughout the lifetime of a ViewItem
---
flowchart TD

id1["_OnViewItemInitialize()"]
id2["_OnViewItemShow()"]
id4["_OnViewItemHide()"]
id5["_OnViewItemPredelete()"]
id6["_OnViewItemNotification()"]

id0[["Component Calls"]] -.-> id1
id1 -..->|Component Calls|id2

subgraph Called Multiple Times before the View Item gets Freed
id4 -..->|Component Calls|id2
id2 -..->|Component Calls|id4
end
id6 -.->|Component Calls|id5
id7[["Godot Calls"]] -.-> id6
```

1. When calling one of the factory methods (`VirtualGridView.CreateFromInstance`/`VirtualGridView.CreateFromPrefab`), after the component has done basic initializing, the `_OnViewItemInitialize` method of each associated view item instance gets invoked.
2. When calling any of the `Show` APIs on a `GridView`, the tab view will call `_OnViewItemHide` on the currently shown view item and call `_OnViewItemShown` on the target view item.
3. A `GridViewItem` delegates the `_Notification` engine call to `_OnViewItemNotification`, and calls `_OnViewItemPredelete` when necessary.

### ViewItemTweeners

Developers may customize a view item's `visual transition behavior when showing/hiding` by accessing its `ViewItemTweener` property.

#### Built-in Tweeners

There are two preconfigured Tweenrs provided with the component.

1. NoneViewItemTweener: This tweener simply hides and shows the view items, it is also the default value of a `ViewItemTweener`, you may access the global instance of this tweener from `NoneViewItemTweener.Instance`.
2. FadeViewItemTweener: This tweener performs fade transition for the view items' showing and hiding, after instantiating the tweener, you may configure the transition time by accessing its `FadeTime` property.

#### Customize Tweeners

By inheriting the `IViewItemTweener` interface, the developer may customize their transition effects.

```csharp
/// <summary>
/// Defines the behavior for view transitions.
/// </summary>
public interface IViewItemTweener
{
    /// <summary>
    /// This sets the default visual appearance for a view item.
    /// </summary>
    /// <param name="viewItem">The target view item.</param>
    /// <param name="additionalData">Optional additional data required by this tweener.</param>
    void Init(Control viewItem, ref object? additionalData);
    
    /// <summary>
    /// This async method manages the behavior when the view item is showing up.
    /// </summary>
    /// <param name="viewItem">The target view item.</param>
    /// <param name="additionalData">Optional additional data required by this tweener.</param>
    void Show(Control viewItem, object? additionalData);
    
    /// <summary>
    /// This async method manages the behavior when the view item is hiding out.
    /// </summary>
    /// <param name="viewItem">The target view item.</param>
    /// <param name="additionalData">Optional additional data required by this tweener.</param>
    void Hide(Control viewItem, object? additionalData);
}
``` -->
