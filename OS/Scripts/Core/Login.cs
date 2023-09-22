using Godot;
using System;

public class Login : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        SavingManager.CurrentUser = Text;
        var yeah = SavingManager.Load<BasicUser>(SavingManager.CurrentUser);
        // versions before the creation of the filesystem
        if (!(yeah.MajorVersion == 0 && yeah.MinorVersion == 6))
            LelfsManager.UpdatePaths();

        PackedScene packedScene = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Lelsktop.tscn");
        Node lelsktop = packedScene.Instance();
        GetTree().Root.AddChild(lelsktop);

        GetNode<Node2D>("/root/Onboarding").QueueFree();
    }
}
