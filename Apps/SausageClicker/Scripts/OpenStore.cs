using Godot;
using System;

public partial class OpenStore : Button {
    AnimationPlayer animation;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
        animation = GetNode<AnimationPlayer>("../AnotherAnimationPlayer");
    }

    public void Click() {
        animation.Play("StoreOpen");
    }
}
