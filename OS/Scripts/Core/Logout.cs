using Godot;
using System;

namespace Lelcore.Onboarding;

public partial class Logout : Button
{
    public override void _Pressed() {
        base._Pressed();
        PackedScene aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/Onboarding.tscn");
        Node aNode = aPackedScene.Instantiate();
        GetTree().Root.AddChild(aNode);
        GetNode("/root/Lelsktop").QueueFree();
        GetNode("/root/LelsktopInterface").QueueFree();
    }
}
