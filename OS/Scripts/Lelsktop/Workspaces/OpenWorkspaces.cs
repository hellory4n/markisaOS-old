using Godot;
using System;

public partial class OpenWorkspaces : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        AnimationPlayer animation = GetNode<AnimationPlayer>("/root/LelsktopInterface/AnimationPlayer");
        if (!Pressed)
            animation.Play("CloseWorkspaces");
        else
            animation.Play("OpenWorkspaces");
    }
}
