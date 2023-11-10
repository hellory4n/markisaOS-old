using Godot;
using System;
using Lelsktop.WindowManager;

public partial class NewFileOpen : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        /*WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Files/NewFile.tscn");
        NewFile jjkn = m.Instantiate<NewFile>();

        // pain
        FileView mewhenthe = GetNode<FileView>("../../ItemList");
        LelfsFile dfggfdf = LelfsManager.Load<LelfsFile>(mewhenthe.Path);
        jjkn.Parent = dfggfdf.Id;
        jjkn.ThingThatINeedToRefresh = mewhenthe;

        wm.AddWindow(jjkn);*/
    }
}
