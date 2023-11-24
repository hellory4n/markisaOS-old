using Godot;
using System;
using Dashboard.Wm;

public partial class ExternalStorageOpen : Button {
    /*public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = GD.Load<PackedScene>("res://Apps/Files/FileImporter.tscn");
        FileImporter jjkn = m.Instantiate<FileImporter>();

        // pain
        FileView mewhenthe = GetNode<FileView>("../../../../Content/ContentThing/ItemList");
        CabinetfsFile dfggfdf = CabinetfsManager.Load<CabinetfsFile>(mewhenthe.Path);
        jjkn.Parent = dfggfdf.Id;
        jjkn.ThingThatINeedToRefresh = mewhenthe;

        wm.AddWindow(jjkn);
    }*/
}
