using Godot;
using System;

namespace Lelcore.Installel;

public partial class InstallelRestart : Timer
{
    /*public override void _Process(double delta)
    {
        // having to wait for the boot screen everytime i test it is very dogwater
        if (Input.IsActionJustReleased("skip_boot"))
            Thing();
        base._Process(delta);
    }

    public override
        var aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/InstallelOobe.tscn");
        Node aNode = aPackedScene.Instantiate();
        GetTree().Root.AddChild(aNode);
        GetParent().QueueFree();
    }*/
}
