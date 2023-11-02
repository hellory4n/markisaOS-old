using Godot;
using System;

public class CloseStore : Button {
    AnimationPlayer animation;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
        animation = GetNode<AnimationPlayer>("../../../../../AnotherAnimationPlayer");
    }

    public void Click() {
        animation.Play("StoreClose");
    }
}
