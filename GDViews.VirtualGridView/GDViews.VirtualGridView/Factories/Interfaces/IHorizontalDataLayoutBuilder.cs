namespace GodotViews.VirtualGrid;

public interface IHorizontalDataLayoutBuilder<TDataType> : IFinishingBuilderAccess<TDataType>
{
    IHorizontalDataLayoutBuilder<TDataType> AddRowDataSource(DataSetDefinition<TDataType> dataSetDefinition);
}