namespace GodotViews.VirtualGrid;

public interface IViewHandlerBuilder
{
    IDataLayoutBuilder WithHandlers(IViewPositioner viewPositioner, IElementTweener elementTweener, IElementFader elementFader);
}