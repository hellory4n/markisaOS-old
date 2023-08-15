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
        maximizedSize = new Vector2(maximizedSize.x, maximizedSize.y-55);

        // check if the window is maximized
        if (window.RectPosition != new Vector2(0, 55) && window.RectSize != maximizedSize) {
            window.RectPosition = new Vector2(0, 55);
            window.RectSize = maximizedSize;
        } else {
            window.RectSize = window.RectMinSize;
            window.RectPosition = new Vector2(150, 150);
        }
    }
}
