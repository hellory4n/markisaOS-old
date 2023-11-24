using Godot;
using System;

namespace Dashboard.Interface;

public partial class SoundSlider : HSlider
{
    /*public override void _ValueChanged(double newValue)
    {
        base._ValueChanged(newValue);
        SoundManager soundManager = GetNode<SoundManager>("/root/SoundManager");
        soundManager.SoundVolume = Mathf.Log(newValue);
    }*/
}
