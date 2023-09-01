using Godot;
using System;

public class Maximize : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public override void _Process(float delta) {
        base._Process(delta);
        WindowDialog window = (WindowDialog)GetParent();
        if (!window.Resizable) {
            GetParent().GetNode<Button>("Minimize").RectPosition = RectPosition;
            QueueFree();
        }
    }

    public void Click() {
        WindowDialog window = (WindowDialog)GetParent();
        Vector2 maximizedSize = ResolutionManager.GetScreenSize();
        maximizedSize = new Vector2(maximizedSize.x, maximizedSize.y-160);

        // check if the window is maximized
        if (window.RectPosition != new Vector2(0, 85) && window.RectSize != maximizedSize) {
            window.RectPosition = new Vector2(0, 85);
            window.RectSize = maximizedSize;
        } else {
            window.RectSize = window.RectMinSize;
            window.RectPosition = new Vector2(150, 150);
        }
    }
}
