using Godot;
using System;

public class Maximize : TextureButton {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        WindowDialog window = (WindowDialog)GetParent();
        Vector2 maximizedSize = ResolutionManager.GetScreenSize();
        maximizedSize = new Vector2(maximizedSize.x, maximizedSize.y-45);

        // check if the window is maximized
        if (window.RectPosition != new Vector2(0, 45) && window.RectSize != maximizedSize) {
            window.RectPosition = new Vector2(0, 45);
            window.RectSize = maximizedSize;
        } else {
            window.RectSize = window.RectMinSize;
            window.RectPosition = new Vector2(150, 150);
        }
    }
}
