using Godot;
using System;

public class MusicProgress : ProgressBar {
    public override void _Ready() {
        base._Ready();
        MaxValue = GetNode<AudioStreamPlayer>("../O/Audio").Stream.GetLength();
    }

    public override void _Process(float delta) {
        base._Process(delta);
        Value = GetNode<AudioStreamPlayer>("../O/Audio").GetPlaybackPosition();
    }
}
