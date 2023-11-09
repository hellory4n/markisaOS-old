using Godot;
using System;

public partial class MusicProgress : ProgressBar {
    public override void _Ready() {
        base._Ready();
        MaxValue = GetNode<MusicPlayer>("../O/Audio").Music.GetLength();
    }

    public override void _Process(float delta) {
        base._Process(delta);
        Value = GetNode<MusicPlayer>("../O/Audio").GetPlaybackPosition();
    }
}
