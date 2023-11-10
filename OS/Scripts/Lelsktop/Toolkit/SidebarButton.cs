using Godot;
using System;
using System.Linq;

namespace Lelsktop.Toolkit;

[GlobalClass]
public partial class SidebarButton : Button
{
    [Export]
    public Control Category;

    public override void _Pressed()
    {
        base._Pressed();
        Category.Visible = true;
        foreach (CanvasItem otherCategory in Category.GetParent().GetChildren().Cast<CanvasItem>())
        {
            if (otherCategory.Name.ToString().StartsWith("Category") && otherCategory != Category)
                otherCategory.Visible = false;
        }
    }
}
