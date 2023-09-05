using Godot;
using System;

public class OpenQuickSettings : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        AnimationPlayer animation = GetNode<AnimationPlayer>("/root/LelsktopInterface/AnimationPlayer");
        if (!Pressed)
            animation.Play("CloseQuickSettings");
        else
            animation.Play("OpenQuickSettings");
    }
}
