using Godot;
using System;

public class OpenAppMenu : Button {
    bool Open = false;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        AnimationPlayer animation = GetNode<AnimationPlayer>("/root/LelsktopInterface/AnimationPlayer");
        if (!Pressed)
            animation.Play("CloseAppMenu");
        else
            animation.Play("OpenAppMenu");
    }
}
