using Godot;
using System;

namespace Kickstart.Onboarding;

public partial class Logout : Button
{
    public override void _Pressed() {
        base._Pressed();
        PackedScene aPackedScene = GD.Load<PackedScene>("res://OS/Core/Onboarding.tscn");
        Node aNode = aPackedScene.Instantiate();
        GetTree().Root.AddChild(aNode);
        GetNode("/root/Dashboard").QueueFree();
        GetNode("/root/DashboardInterface").QueueFree();
    }
}
