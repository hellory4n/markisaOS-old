using Godot;
using System;

public class OpenStore : Button {
    AnimationPlayer animation;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
        animation = GetNode<AnimationPlayer>("../AnotherAnimationPlayer");
    }

    public void Click() {
        animation.Play("StoreOpen");
    }
}
