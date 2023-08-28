using Godot;
using System;

public class EndBoot : Timer {
    public override void _Ready() {
        base._Ready();
        Connect("timeout", this, nameof(Thing));

        // open the mobile setup screen :)
        if (OS.GetName() != "Android") {
            PackedScene oneOfThePackedScenes = ResourceLoader.Load<PackedScene>("res://OS/Core/MobileSetup.tscn");
            Node oneOfTheNodes = oneOfThePackedScenes.Instance();
            GetTree().Root.CallDeferred("add_child", oneOfTheNodes);
            GetParent().QueueFree();
        }
    }

    public override void _Process(float delta) {
        // having to wait for the boot screen everytime i test it is very dogwater
        if (Input.IsActionJustReleased("skip_boot"))
            Thing();
        base._Process(delta);
    }

    public void Thing() {
        PackedScene aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/Onboarding.tscn");
        Node aNode = aPackedScene.Instance();
        GetTree().Root.AddChild(aNode);
        GetParent().QueueFree();
    }
}
