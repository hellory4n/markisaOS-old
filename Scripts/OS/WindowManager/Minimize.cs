using Godot;
using System;

public class Minimize : TextureButton {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        WindowDialog yourMother = (WindowDialog)GetParent();
        yourMother.RectPosition = new Vector2(-69420, -69420);
    }
}
