using Godot;
using System;

public partial class OpenAppMenu : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        AnimationPlayer animation = GetNode<AnimationPlayer>("/root/LelsktopInterface/AnimationPlayer");
        if (!Pressed)
            animation.Play("CloseAppMenu");
        else
            animation.Play("OpenAppMenu");
    }
}
