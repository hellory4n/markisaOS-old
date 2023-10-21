using Godot;
using System;

public class InstallelIntroThingy : VideoPlayer {
    public override void _Ready() {
        base._Ready();
        Connect("finished", this, nameof(Help));
    }

    public void Help() {
        GetNode<AudioStreamPlayer>("../../../../../H").StreamPaused = false;
        GetNode<AnimationPlayer>("../../Installel/AnimationPlayer").Play("Open");
        GetNode<BaseWindow>("../../Installel").RectPosition = new Vector2(50, 95);
        QueueFree();
    }
}
