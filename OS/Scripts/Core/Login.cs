using Godot;
using System;

public class Login : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        SavingManager.CurrentUser = Text;
        LelfsManager.UpdatePaths();

        PackedScene packedScene = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Lelsktop.tscn");
        Node lelsktop = packedScene.Instance();
        GetTree().Root.AddChild(lelsktop);

        GetNode<Node2D>("/root/Onboarding").QueueFree();
    }
}
