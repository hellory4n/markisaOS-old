using Godot;
using System;

public partial class VideoProgress : ProgressBar {
    public override void _Process(float delta) {
        base._Process(delta);
        Value = GetNode<VideoStreamPlayer>("../M/Video").StreamPosition;
    }
}
