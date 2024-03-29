namespace GodotViews.VirtualGrid;

public interface IViewHandlerBuilder
{
    IDataLayoutBuilder WithViewHandler(IViewHandler viewHandler, IElementTweener elementTweener, IElementFader elementFader);
}