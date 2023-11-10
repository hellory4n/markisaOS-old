using Godot;
using System;

public partial class InstallelRestart : Timer {
    public override void _Ready() {
        base._Ready();
        Connect("timeout", new Callable(this, nameof(Thing)));
    }

    public override void _Process(double delta) {
        // having to wait for the boot screen everytime i test it is very dogwater
        if (Input.IsActionJustReleased("skip_boot"))
            Thing();
        base._Process(delta);
    }

    public void Thing() {
        var aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/InstallelOobe.tscn");
        Node aNode = aPackedScene.Instantiate();
        GetTree().Root.AddChild(aNode);
        GetParent().QueueFree();
    }
}
