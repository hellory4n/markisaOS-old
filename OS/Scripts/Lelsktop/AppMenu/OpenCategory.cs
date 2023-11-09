using Godot;
using System;

public partial class OpenCategory : Button {
    [Export]
    string Category = "";

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        ScrollContainer category = GetParent().GetParent().GetParent().GetNode<ScrollContainer>(Category);
        category.Visible = true;
        foreach (CanvasItem otherCategory in category.GetParent().GetChildren()) {
            if (otherCategory.Name.StartsWith("Category") && otherCategory.Name != Category) {
                otherCategory.Visible = false;
            }
        }
    }
}
