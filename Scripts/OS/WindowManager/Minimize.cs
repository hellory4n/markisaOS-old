using Godot;
using System;

public class Minimize : TextureButton {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        WindowDialog yourMother = (WindowDialog)GetParent();
        // making it invisible makes the window think it was closed cuz
        yourMother.RectPosition = new Vector2(-69420, -69420);
    }
}
