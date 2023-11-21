using Godot;
using System;

namespace Lelsktop.Interface;

public partial class QuickSettingsShutdown : Button
{
    public override void _Pressed() {
        base._Pressed();
        PackedScene m = GD.Load<PackedScene>("res://OS/Core/Shutdown.tscn");
        Node jjkn = m.Instantiate();
        GetTree().Root.AddChild(jjkn);
        GetNode("/root/Lelsktop").QueueFree();
    }
}
