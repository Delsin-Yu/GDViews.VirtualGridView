using GDUtilities;
using Godot;

namespace GDViews.VirtualGrid.Example.TMI.Recipes;

public partial class TMIRecipes_Main
{
    private class TextPrinter : ObjectSpawner<TMIRecipes_TextView, string>
    {
        public TextPrinter(Control container, PackedScene prefab) : base(container, prefab) { }
        
        protected override void DrawElement(TMIRecipes_TextView instance, string value, int index) => 
            instance.Print(value);
    }
}