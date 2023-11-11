using Godot;
using System;

namespace Lelcore.Onboarding;

public partial class Login : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        SavingManager.CurrentUser = Text;
        var yeah = SavingManager.Load<BasicUser>(SavingManager.CurrentUser);
        // versions before the creation of the filesystem
        if (!(yeah.MajorVersion == 0 && yeah.MinorVersion == 6))
            LelfsManager.UpdatePaths();

        PackedScene packedScene = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Lelsktop.tscn");
        Node lelsktop = packedScene.Instantiate();
        GetTree().Root.AddChild(lelsktop);

        GetNode<Node2D>("/root/Onboarding").QueueFree();
    }
}
