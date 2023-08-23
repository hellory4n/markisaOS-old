using Godot;
using System;

public class OpenCategory : Button {
    [Export]
    string Category = "";

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
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
