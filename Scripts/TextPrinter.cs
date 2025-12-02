using GDUtilities;
using Godot;

namespace GDViews.VirtualGrid.Example.TMI;

public class TextPrinter : ObjectSpawner<TextView, string>
{
    public TextPrinter(Control container, PackedScene prefab) : base(container, prefab) { }
        
    protected override void DrawElement(TextView instance, string value, int index) => 
        instance.Print(value);
}