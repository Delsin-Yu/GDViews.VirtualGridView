namespace GodotViews.VirtualGrid;

public interface IGodotTween
{
    float Duration { get; set; }
    TweenSetup TweenSetup { get; set; }
}

public interface IGodotTweenScrollBarTweener : IGodotTween, IScrollBarTweener { }

public interface IGodotTweenTweener : IGodotTween, IElementTweener { }

public interface IGodotTweenFader : IGodotTween, IElementFader { }