using Godot;
using System;

namespace Lelcore.Bootloader;

public partial class Shutdown : Timer
{
    /*public override void _Ready() {
        base._Ready();
        Connect("timeout", new Callable(this, nameof(Thing)));
        SoundManager sounds = GetNode<SoundManager>("/root/SoundManager");
        sounds.PlaySoundEffect(SoundManager.SoundEffects.Shutdown);
    }

    public void Thing() {
        GetTree().Quit();
    }*/
}
