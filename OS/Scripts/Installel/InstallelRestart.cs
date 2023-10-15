using Godot;
using System;

public class InstallelRestart : Timer {
    public override void _Ready() {
        base._Ready();
        Connect("timeout", this, nameof(Thing));
    }

    public void Thing() {
        var aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/InstallelOobe.tscn");
        Node aNode = aPackedScene.Instance();
        GetTree().Root.AddChild(aNode);
        GetParent().QueueFree();
    }
}
