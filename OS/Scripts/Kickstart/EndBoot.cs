using Godot;
using Kickstart.Records;
using System;

namespace Kickstart.Bootloader;

public partial class EndBoot : Timer
{
    bool installing = false;

    public override void _Ready()
    {
        base._Ready();
        Connect("timeout", new Callable(this, nameof(Thing)));

        // if we're gonna install markisaOS then the bootscreen should take longer and stuff
        if (!RecordManager.Load<SystemInfo>().Installed)
        {
            WaitTime = 15;
            GetNode<Label>("../Control/Label").Text = "markisaOS Me is preparing the installation process.\nThis can take several seconds.";
            GetNode<Label>("../Control/Label").OffsetTop = -100;
            installing = true;
        }
    }

    public override void _Process(double delta)
    {
        // having to wait for the boot screen everytime i test it is very dogwater
        if (Input.IsActionJustReleased("skip_boot"))
            Thing();
        base._Process(delta);
    }

    public void Thing()
    {
        PackedScene aPackedScene;
        if (installing)
            aPackedScene = GD.Load<PackedScene>("res://OS/Kickstart/Installel.tscn");
        else
            aPackedScene = GD.Load<PackedScene>("res://OS/Kickstart/Onboarding.tscn");
        Node aNode = aPackedScene.Instantiate();
        GetTree().Root.AddChild(aNode);
        GetParent().QueueFree();
    }
}