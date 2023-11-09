using Godot;
using System;

public partial class InstallelIntroThingy : VideoStreamPlayer {
    public override void _Ready() {
        base._Ready();
        Connect("finished", new Callable(this, nameof(Help)));
    }

    public void Help() {
        GetNode<AudioStreamPlayer>("../../../../../H").StreamPaused = false;
        GetNode<AnimationPlayer>("../../Installel/AnimationPlayer").Play("Open");
        GetNode<BaseWindow>("../../Installel").Position = new Vector2(50, 95);
        QueueFree();
    }
}
