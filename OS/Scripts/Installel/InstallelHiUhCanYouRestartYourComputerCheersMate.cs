using Godot;
using System;

namespace Lelcore.Installel;

public partial class InstallelHiUhCanYouRestartYourComputerCheersMate : Button 
{
    public override void _Pressed()
    {
        base._Pressed();
        // make the screen go black for a short period of time :)
        Timer timer = new()
        {
            WaitTime = 1,
            OneShot = true,
            Autostart = true
        };
        GetTree().Root.AddChild(timer);
        timer.Connect("timeout", new Callable(this, nameof(Bruh)));
        GetNode<Node2D>("/root/Installel").Position = new Vector2(69420, 69420);
    }

    public void Bruh()
    {
        PackedScene m = ResourceLoader.Load<PackedScene>("res://OS/Core/InstallelRestart.tscn");
        Node jjkn = m.Instantiate();
        GetTree().Root.AddChild(jjkn);
        GetNode("/root/Installel").QueueFree();
    }
}
