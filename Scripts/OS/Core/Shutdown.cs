using Godot;
using System;

public class Shutdown : Timer {
    public override void _Ready() {
        base._Ready();
        Connect("timeout", this, nameof(Thing));
    }

    public void Thing() {
        GetTree().Quit();
    }
}
