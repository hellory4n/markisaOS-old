using Godot;
using System;

public partial class SidebarButton : Button {
    [Export]
    public NodePath Category;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        Control category = GetNode<Control>(Category);

        category.Visible = true;
        foreach (CanvasItem otherCategory in category.GetParent().GetChildren()) {
            if (otherCategory.Name.StartsWith("Category") && otherCategory != category) {
                otherCategory.Visible = false;
            }
        }
    }
}
