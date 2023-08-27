using Godot;
using System;

public class Shutdown : Timer {
    public override void _Ready() {
        base._Ready();
        Connect("timeout", this, nameof(Thing));
        SoundManager sounds = GetNode<SoundManager>("/root/SoundManager");
        sounds.PlaySoundEffect(SoundManager.SoundEffects.Shutdown);
    }

    public void Thing() {
        GetTree().Quit();
    }
}
