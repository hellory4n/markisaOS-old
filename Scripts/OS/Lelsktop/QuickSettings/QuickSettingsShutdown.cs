using Godot;
using System;

public class QuickSettingsShutdown : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        PackedScene m = ResourceLoader.Load<PackedScene>("res://OS/Core/Shutdown.tscn");
        Node jjkn = m.Instance();
        GetTree().Root.AddChild(jjkn);
        GetNode("/root/Lelsktop").QueueFree();
        GetNode("/root/LelsktopInterface").QueueFree();
    }
}
