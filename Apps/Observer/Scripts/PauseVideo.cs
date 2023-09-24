using Godot;
using System;

public class PauseVideo : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        GetNode<VideoPlayer>("../M/Video").Paused = Pressed;
    }
}
