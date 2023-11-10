using Godot;
using System;

public partial class ExternalStorageOpen : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Files/FileImporter.tscn");
        FileImporter jjkn = m.Instantiate<FileImporter>();

        // pain
        FileView mewhenthe = GetNode<FileView>("../../../../Content/ContentThing/ItemList");
        LelfsFile dfggfdf = LelfsManager.Load<LelfsFile>(mewhenthe.Path3D);
        jjkn.Parent = dfggfdf.Id;
        jjkn.ThingThatINeedToRefresh = mewhenthe;

        wm.AddWindow(jjkn);
    }
}
