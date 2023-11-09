using Godot;
using System;

public partial class SoundSlider : HSlider {
    public override void _Ready() {
        base._Ready();
        Connect("value_changed", new Callable(this, nameof(TheThing)));
    }

    public void TheThing(float yes) {
        SoundManager soundManager = GetNode<SoundManager>("/root/SoundManager");
        soundManager.SoundVolume = Mathf.Log(yes);
    }
}
