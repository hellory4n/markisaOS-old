using Godot;
using System;

public class Minimize : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        GetParent().GetNode<AnimationPlayer>("AnimationPlayer").Play("Minimize");
    }
}
