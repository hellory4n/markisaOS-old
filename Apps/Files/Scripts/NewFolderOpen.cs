using Godot;
using System;

public class NewFolderOpen : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Files/NewFolder.tscn");
        NewFolder jjkn = m.Instance<NewFolder>();

        // pain
        FileView mewhenthe = GetNode<FileView>("../../ItemList");
        if (mewhenthe.Path != "/") {
            BaseLelfs dfggfdf = LelfsManager.Load<BaseLelfs>(mewhenthe.Path);
            jjkn.Parent = dfggfdf.Id;
        } else {
            jjkn.Parent = "/";
        }
        jjkn.ThingThatINeedToRefresh = mewhenthe;

        wm.AddWindow(jjkn);
    }
}
