namespace GodotViews.VirtualGrid;

public interface IHorizontalDataLayoutBuilder<TDataType> : IDelegateBuilderAccess<TDataType>
{
    IHorizontalDataLayoutBuilder<TDataType> AddRowDataSource(DataSetDefinition<TDataType> dataSetDefinition);
}