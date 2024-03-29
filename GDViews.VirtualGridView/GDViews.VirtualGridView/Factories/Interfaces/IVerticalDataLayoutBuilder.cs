namespace GodotViews.VirtualGrid;

public interface IVerticalDataLayoutBuilder<TDataType> : IDelegateBuilderAccess<TDataType>
{
    IVerticalDataLayoutBuilder<TDataType> AddColumnDataSource(DataSetDefinition<TDataType> dataSetDefinition);
}