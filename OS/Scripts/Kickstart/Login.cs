using Godot;
using System;

namespace Kickstart.Onboarding;

public partial class Login : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        SavingManager.CurrentUser = Text;
        var yeah = SavingManager.Load<BasicUser>(SavingManager.CurrentUser);
        // versions before the creation of the filesystem
        if (!(yeah.MajorVersion == 0 && yeah.MinorVersion == 6))
            CabinetfsManager.UpdatePaths();

        PackedScene packedScene = GD.Load<PackedScene>("res://OS/Dashboard/Dashboard.tscn");
        Node dashboard = packedScene.Instantiate();
        GetTree().Root.AddChild(dashboard);

        GetNode<Node2D>("/root/Onboarding").QueueFree();
    }
}
