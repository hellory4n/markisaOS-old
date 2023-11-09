using Godot;
using System;

public partial class Minimize : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        GetParent().GetNode<AnimationPlayer>("AnimationPlayer").Play("Minimize");
    }
}
