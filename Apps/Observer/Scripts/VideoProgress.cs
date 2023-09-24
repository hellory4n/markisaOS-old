using Godot;
using System;

public class VideoProgress : ProgressBar {
    public override void _Process(float delta) {
        base._Process(delta);
        Value = GetNode<VideoPlayer>("../M/Video").StreamPosition;
    }
}
