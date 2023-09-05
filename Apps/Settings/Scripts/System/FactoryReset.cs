using Godot;
using System;

public class FactoryReset : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        // yes
        DeleteFolder("user://");

        Directory mfghjh = new Directory();
        mfghjh.Remove("user://Settings");
        mfghjh.Remove("user://Users");

        // now show the factory reset screen :)
        PackedScene m = ResourceLoader.Load<PackedScene>("res://OS/Core/FactoryReset.tscn");
        Node jjkn = m.Instance();
        GetTree().Root.AddChild(jjkn);
        
        GetNode<Node2D>("/root/Lelsktop").QueueFree();
        GetNode<CanvasLayer>("/root/LelsktopInterface").QueueFree();
    }

    public void DeleteFolder(string path) {
        Directory dir = new Directory();
        if (dir.Open(path) == Error.Ok) {
            dir.ListDirBegin(true);
            string filename = dir.GetNext();
            while (filename != "") {
                if (dir.CurrentIsDir()) {
                    DeleteFolder($"{path}/{filename}");
                    dir.Remove($"{path}/{filename}/");
                }
                else {
                    dir.Remove($"{path}/{filename}");
                }
                filename = dir.GetNext();
            }
            dir.ListDirEnd();
        } else {
            GD.PushWarning($"Error deleting {path}");
        }
    }
}
