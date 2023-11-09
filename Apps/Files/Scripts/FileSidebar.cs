using Godot;
using System;

public partial class FileSidebar : Button {
    [Export]
    public string Path3D = "";

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        // pain
        if (LelfsManager.FileExists(Path3D))
            GetNode<FileView>("../../../../Content/ContentThing/ItemList").Refresh(Path3D);
    }
}
