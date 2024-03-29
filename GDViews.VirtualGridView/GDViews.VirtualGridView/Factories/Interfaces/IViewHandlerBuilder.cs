namespace GodotViews.VirtualGrid;

public interface IViewHandlerBuilder
{
    IDataLayoutBuilder WithHandlers(IViewHandler viewHandler, IElementTweener elementTweener, IElementFader elementFader);
}