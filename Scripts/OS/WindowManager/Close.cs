using Godot;
using System;

public class Close : TextureButton {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        GetParent().QueueFree();
    }
}
