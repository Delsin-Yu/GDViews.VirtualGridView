namespace GodotViews.VirtualGrid;

public interface IVerticalDataLayoutBuilder<TDataType> : IFinishingBuilderAccess<TDataType>
{
    IVerticalDataLayoutBuilder<TDataType> AddColumnDataSource(DataSetDefinition<TDataType> dataSetDefinition);
}