using Godot;

namespace GodotViews.VirtualGrid.Examples;

public partial class Main
{
    private readonly (string TweenName, TweenSetup TweenSetup)[] _listOfTweens =
    {
        ("线性", TweenSetups.Linear),
        ("缓入: Sine", TweenSetups.EaseIn.Sine),
        ("缓出: Sine", TweenSetups.EaseOut.Sine),
        ("缓入缓出: Sine", TweenSetups.EaseInOut.Sine),
        ("缓入: Quad", TweenSetups.EaseIn.Quad),
        ("缓出: Quad", TweenSetups.EaseOut.Quad),
        ("缓入缓出: Quad", TweenSetups.EaseInOut.Quad),
        ("缓入: Cubic", TweenSetups.EaseIn.Cubic),
        ("缓出: Cubic", TweenSetups.EaseOut.Cubic),
        ("缓入缓出: Cubic", TweenSetups.EaseInOut.Cubic),
        ("缓入: Quart", TweenSetups.EaseIn.Quart),
        ("缓出: Quart", TweenSetups.EaseOut.Quart),
        ("缓入缓出: Quart", TweenSetups.EaseInOut.Quart),
        ("缓入: Quint", TweenSetups.EaseIn.Quint),
        ("缓出: Quint", TweenSetups.EaseOut.Quint),
        ("缓入缓出: Quint", TweenSetups.EaseInOut.Quint),
        ("缓入: Expo", TweenSetups.EaseIn.Expo),
        ("缓出: Expo", TweenSetups.EaseOut.Expo),
        ("缓入缓出: Expo", TweenSetups.EaseInOut.Expo),
        ("缓入: Circ", TweenSetups.EaseIn.Circ),
        ("缓出: Circ", TweenSetups.EaseOut.Circ),
        ("缓入缓出: Circ", TweenSetups.EaseInOut.Circ),
        ("缓入: Back", TweenSetups.EaseIn.Back),
        ("缓出: Back", TweenSetups.EaseOut.Back),
        ("缓入缓出: Back", TweenSetups.EaseInOut.Back),
        ("缓入: Elastic", TweenSetups.EaseIn.Elastic),
        ("缓出: Elastic", TweenSetups.EaseOut.Elastic),
        ("缓入缓出: Elastic", TweenSetups.EaseInOut.Elastic),
        ("缓入: Bounce", TweenSetups.EaseIn.Bounce),
        ("缓出: Bounce", TweenSetups.EaseOut.Bounce),
        ("缓入缓出: Bounce", TweenSetups.EaseInOut.Bounce),
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
    
    private readonly (string Name, IScrollBarTweener Data)[] _listOfScrollBarTweenerTypes =
    {
        ("无", ScrollBarTweeners.None),
        ("过渡", ScrollBarTweeners.CreateLerp(0f)),
    };

    private readonly (string Name, IElementPositioner Data)[] _listOfPositionerTypes =
    {
        ("边缘对齐", ElementPositioners.Side),
        ("居中对齐", ElementPositioners.Centered),
    };

    private readonly (string Name, Vector2I Data)[] _listOfStartPositionsTypes =
    {
        ("左上", ViewCorners.TopLeft),
        ("右上", ViewCorners.TopRight),
        ("左下", ViewCorners.BottomLeft),
        ("右下", ViewCorners.BottomRight),
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