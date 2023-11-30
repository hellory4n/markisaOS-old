using Godot;
using Kickstart.Records;
using System;

namespace Kickstart.Onboarding;

public partial class UserList : VBoxContainer
{
    public override void _Ready()
    {
        base._Ready();
        PackedScene stupidity = GD.Load<PackedScene>("res://OS/Kickstart/UserButton.tscn");

        if (DirAccess.DirExistsAbsolute("user://Users/")) {
            DirAccess dir = DirAccess.Open("user://Users/");
            dir.ListDirBegin();
            string filename = dir.GetNext();
            while (filename != "") {
                // RecordManager.Load() can only load from the current user, so this is a workaround
                RecordManager.CurrentUser = filename;

                Login useromgomgomg = stupidity.Instantiate<Login>();
                useromgomgomg.Text = RecordManager.Load<MarkisaUser>().DisplayName;
                useromgomgomg.Username = filename;
                useromgomgomg.TooltipText = filename;
                
                // cool user photo
                string photo = RecordManager.Load<MarkisaUser>().Photo;
                useromgomgomg.Icon = GD.Load<Texture2D>(photo);

                AddChild(useromgomgomg);
                filename = dir.GetNext();
            }
        }
    }
}
