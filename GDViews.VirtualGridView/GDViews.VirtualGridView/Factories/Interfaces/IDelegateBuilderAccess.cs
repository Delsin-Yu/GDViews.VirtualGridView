using Godot;

namespace GodotViews.VirtualGrid;

public interface IDelegateBuilderAccess<TDataType>
{
    IDelegateBuilder<TDataType, TButtonType> WithDelegate<TButtonType>() where TButtonType : Button;
}