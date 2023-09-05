using Godot;
using System;

public class EndBoot : Timer {
    public override void _Ready() {
        base._Ready();
        Connect("timeout", this, nameof(Thing));
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
