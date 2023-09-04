using Godot;
using System;

public class SoundSlider : HSlider {
    public override void _Ready() {
        base._Ready();
        Connect("value_changed", this, nameof(TheThing));
    }

    public void TheThing(float yes) {
        SoundManager soundManager = GetNode<SoundManager>("/root/SoundManager");
        soundManager.SoundVolume = Mathf.Log(yes);
    }
}
