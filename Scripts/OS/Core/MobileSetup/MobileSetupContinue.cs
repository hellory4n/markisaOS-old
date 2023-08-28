using Godot;
using System;

public class MobileSetupContinue : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        PackedScene m = ResourceLoader.Load<PackedScene>("res://OS/Core/Bootscreen.tscn");
        Node jjkn = m.Instance();
        GetTree().Root.AddChild(jjkn);
        GetParent().GetParent().QueueFree();
    }
}
