using Godot;

namespace GodotViews.VirtualGrid;

internal interface ITweenUser<in TTweenType>
{
    bool IsTweenSupported(TTweenType tweenType);
    void InitializeTween(TTweenType tweenType, in Vector2? targetPosition, Control control, Tween tween);
}