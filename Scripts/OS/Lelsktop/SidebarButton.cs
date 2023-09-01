using Godot;
using System;

public class SidebarButton : Button {
    [Export]
    NodePath Category;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        // uh
        Control category = GetNode<Control>(Category);

        category.Visible = true;
        foreach (CanvasItem otherCategory in category.GetParent().GetChildren()) {
            if (otherCategory.Name.StartsWith("Category") && otherCategory != category) {
                otherCategory.Visible = false;
            }
        }
    }
}
