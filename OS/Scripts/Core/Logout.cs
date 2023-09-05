using Godot;
using System;

public class Logout : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        PackedScene aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/Onboarding.tscn");
        Node aNode = aPackedScene.Instance();
        GetTree().Root.AddChild(aNode);
        GetNode("/root/Lelsktop").QueueFree();
        GetNode("/root/LelsktopInterface").QueueFree();
    }
}
