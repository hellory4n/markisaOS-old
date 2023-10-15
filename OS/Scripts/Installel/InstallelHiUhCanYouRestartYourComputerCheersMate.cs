using Godot;
using System;

public class InstallelHiUhCanYouRestartYourComputerCheersMate : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        // make the screen go black for a short period of time :)
        Timer timer = new Timer {
            WaitTime = 1,
            OneShot = true,
            Autostart = true
        };
        GetTree().Root.AddChild(timer);
        timer.Connect("timeout", this, nameof(Bruh));
        GetNode<Node2D>("/root/Installel").Position = new Vector2(69420, 69420);
    }

    public void Bruh() {
        PackedScene m = ResourceLoader.Load<PackedScene>("res://OS/Core/InstallelRestart.tscn");
        Node jjkn = m.Instance();
        GetTree().Root.AddChild(jjkn);
        GetNode("/root/Installel").QueueFree();
    }
}
