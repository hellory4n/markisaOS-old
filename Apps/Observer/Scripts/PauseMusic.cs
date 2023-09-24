using Godot;
using System;

public class PauseMusic : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        GetNode<AudioStreamPlayer>("../Audio").StreamPaused = Pressed;
    }
}
