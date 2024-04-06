using Godot;
using GodotViews.Core.FocusFinder;

namespace GodotViews.VirtualGrid.Examples;

public partial class Main
{
    private readonly (string TweenName, TweenSetup TweenSetup)[] _listOfTweens =
    {
        ("线性", TweenSetups.Linear),
        ("缓入: Sine", TweenSetups.EaseInSine),
        ("缓出: Sine", TweenSetups.EaseOutSine),
        ("缓入缓出: Sine", TweenSetups.EaseInOutSine),
        ("缓入: Quad", TweenSetups.EaseInQuad),
        ("缓出: Quad", TweenSetups.EaseOutQuad),
        ("缓入缓出: Quad", TweenSetups.EaseInOutQuad),
        ("缓入: Cubic", TweenSetups.EaseInCubic),
        ("缓出: Cubic", TweenSetups.EaseOutCubic),
        ("缓入缓出: Cubic", TweenSetups.EaseInOutCubic),
        ("缓入: Quart", TweenSetups.EaseInQuart),
        ("缓出: Quart", TweenSetups.EaseOutQuart),
        ("缓入缓出: Quart", TweenSetups.EaseInOutQuart),
        ("缓入: Quint", TweenSetups.EaseInQuint),
        ("缓出: Quint", TweenSetups.EaseOutQuint),
        ("缓入缓出: Quint", TweenSetups.EaseInOutQuint),
        ("缓入: Expo", TweenSetups.EaseInExpo),
        ("缓出: Expo", TweenSetups.EaseOutExpo),
        ("缓入缓出: Expo", TweenSetups.EaseInOutExpo),
        ("缓入: Circ", TweenSetups.EaseInCirc),
        ("缓出: Circ", TweenSetups.EaseOutCirc),
        ("缓入缓出: Circ", TweenSetups.EaseInOutCirc),
        ("缓入: Back", TweenSetups.EaseInBack),
        ("缓出: Back", TweenSetups.EaseOutBack),
        ("缓入缓出: Back", TweenSetups.EaseInOutBack),
        ("缓入: Elastic", TweenSetups.EaseInElastic),
        ("缓出: Elastic", TweenSetups.EaseOutElastic),
        ("缓入缓出: Elastic", TweenSetups.EaseInOutElastic),
        ("缓入: Bounce", TweenSetups.EaseInBounce),
        ("缓出: Bounce", TweenSetups.EaseOutBounce),
        ("缓入缓出: Bounce", TweenSetups.EaseInOutBounce),
    };

    private readonly (string Name, IElementFader Data)[] _listOfFaderTypes =
    {
        ("无", ElementFaders.None),
        ("渐隐", ElementFaders.CreateFade(0f)),
        ("缩放", ElementFaders.CreateScale(0f)),
        ("缩放 & 旋转", ElementFaders.CreateScaleRotate(0f)),
    };

    private readonly (string Name, IElementTweener Data)[] _listOfTweenerTypes =
    {
        ("无", ElementTweeners.None),
        ("平移", ElementTweeners.CreatePan(0f)),
    };

    private readonly (string Name, IElementPositioner Data)[] _listOfPositionerTypes =
    {
        ("边缘对齐", ElementPositioners.Side),
        ("居中对齐", ElementPositioners.Centered),
    };

    private readonly (string Name, Vector2I Data)[] _listOfStartPositionsTypes =
    {
        ("左上", ViewCorner.TopLeft),
        ("右上", ViewCorner.TopRight),
        ("坐下", ViewCorner.BottomLeft),
        ("右下", ViewCorner.BottomRight),
        ("中央", Vector2I.Zero),
    };

    private readonly (string Name, SearchDirection Data)[] _listOfSearchDirectionsTypes =
    {
        ("右下", SearchDirections.RightDown),
        ("左下", SearchDirections.LeftDown),
        ("右上", SearchDirections.RightUp),
        ("左上", SearchDirections.LeftUp),
        ("下右", SearchDirections.DownRight),
        ("下左", SearchDirections.DownLeft),
        ("上右", SearchDirections.UpRight),
        ("上左", SearchDirections.UpLeft),
        ("全向顺时针", SearchDirections.FourWayClockwise),
        ("全向逆时针", SearchDirections.FourWayAnticlockwise),
        ("上下左右", SearchDirections.UpDownLeftRight),
        ("上", SearchDirections.Up),
        ("下", SearchDirections.Down),
        ("左", SearchDirections.Left),
        ("右", SearchDirections.Right),
    };

    private readonly (string Name, int Data)[] _listOfDataSets =
    {
        ("数据集1", 0),
        ("数据集2", 1),
        ("数据集3", 2),
        ("数据集4", 3),
        ("数据集5", 4),
    };
}