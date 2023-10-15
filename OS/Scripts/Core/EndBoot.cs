using Godot;
using System;

public class EndBoot : Timer {
    bool installing = false;

    public override void _Ready() {
        base._Ready();
        Connect("timeout", this, nameof(Thing));

        // if we're gonna install lelcubeOS then the bootscreen should take longer and stuff
        if (!SavingManager.LoadSettings<InstallerInfo>().IsInstalled) {
            WaitTime = 20;
            GetNode<Label>("../Control/Label").Text = "lelcubeOS Me is preparing the installation process.\nThis can take several seconds.";
            GetNode<Label>("../Control/Label").MarginTop = -100;
            installing = true;
        }
    }

    public override void _Process(float delta) {
        // having to wait for the boot screen everytime i test it is very dogwater
        if (Input.IsActionJustReleased("skip_boot"))
            Thing();
        base._Process(delta);
    }

    public void Thing() {
        PackedScene aPackedScene;
        if (installing)
            aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/Installel.tscn");
        else
            aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/Onboarding.tscn");
        Node aNode = aPackedScene.Instance();
        GetTree().Root.AddChild(aNode);
        GetParent().QueueFree();
    }
}
