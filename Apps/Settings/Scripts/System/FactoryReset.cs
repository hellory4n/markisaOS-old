using Godot;
using System;

namespace Settings;

public partial class FactoryReset : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        // yes
        DeleteFolder("user://");

        DirAccess.RemoveAbsolute("user://Settings");
        DirAccess.RemoveAbsolute("user://Users");

        // now show the factory reset screen :)
        PackedScene m = GD.Load<PackedScene>("res://OS/Kickstart/FactoryReset.tscn");
        Node jjkn = m.Instantiate();
        GetTree().Root.AddChild(jjkn);
        
        GetNode("/root/Dashboard").QueueFree();
    }

    static void DeleteFolder(string path)
    {
        DirAccess dir = DirAccess.Open(path);
        if (dir != null)
        {
            dir.ListDirBegin();
            string filename = dir.GetNext();
            while (filename != "")
            {
                if (dir.CurrentIsDir()) {
                    DeleteFolder($"{path}/{filename}");
                    dir.Remove($"{path}/{filename}/");
                }
                else
                    dir.Remove($"{path}/{filename}");
                filename = dir.GetNext();
            }
            dir.ListDirEnd();
        } else {
            GD.PushWarning($"Error deleting {path}");
        }
    }
}
