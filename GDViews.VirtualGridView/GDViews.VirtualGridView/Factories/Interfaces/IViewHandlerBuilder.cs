namespace GodotViews.VirtualGrid;

public interface IViewHandlerBuilder
{
    IDataLayoutBuilder WithHandlers(IElementPositioner elementPositioner, IElementTweener elementTweener, IElementFader elementFader);
}