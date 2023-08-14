using Godot;
using System;

public class Maximize : TextureButton {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        Vector2 newSize = ResolutionManager.GetScreenSize();
        // newSize = new Vector2(newSize.x, newSize.y-55);
        Control window = (Control)GetParent();
        window.RectPosition = new Vector2(0, 55);
        window.RectSize = newSize;
    }
}
