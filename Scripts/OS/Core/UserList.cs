using Godot;
using System;
using System.Collections.Generic;

public class UserList : VBoxContainer {
    public override void _Ready() {
        base._Ready();

        PackedScene stupidity = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/UserButton.tscn");
        Texture lelcube = ResourceLoader.Load<Texture>("res://icon.png");

        Directory dir = new Directory();
        dir.Open("user://Users/");
        dir.ListDirBegin(true);
        string filename = dir.GetNext();
        while (filename != "") {
            Button useromgomgomg = (Button)stupidity.Instance();
            useromgomgomg.Text = filename;
            
            // cool user photo
            string photo = SavingManager.GetUserPhoto(filename);
            switch (photo) {
                case "Lelcube":
                    useromgomgomg.Icon = lelcube;
                    break;
            }

            AddChild(useromgomgomg);
            filename = dir.GetNext();
        }
        dir.ListDirEnd();
    }
}
