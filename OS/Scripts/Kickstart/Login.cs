using Godot;
using Kickstart.Records;
using System;

namespace Kickstart.Onboarding;

public partial class Login : Button
{
    public string Username;

    public override void _Pressed()
    {
        base._Pressed();
        RecordManager.CurrentUser = Username;
        RecordManager.CurrentUserDisplayName = Text;

        PackedScene packedScene = GD.Load<PackedScene>("res://OS/Dashboard/Dashboard.tscn");
        Node dashboard = packedScene.Instantiate();
        GetTree().Root.AddChild(dashboard);

        GetNode<Node2D>("/root/Onboarding").QueueFree();
    }
}
