using Godot;
using GodotViews.VirtualGrid;

namespace GodotViews.Core.FocusFinder;


public delegate Vector2I DataStartPositionHandler<TDataType, in TArgument>(ref readonly ReadOnlyDataArray<TDataType> currentView, TArgument argument);

public delegate Vector2I ViewStartPositionHandler(ref readonly ReadOnlyViewArray currentView);

public delegate Vector2I ViewStartPositionHandler<in TArgument>(ref readonly ReadOnlyViewArray currentView, TArgument argument);
