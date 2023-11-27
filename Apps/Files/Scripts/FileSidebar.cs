using Godot;
using System;
using Kickstart.Cabinetfs;

public partial class FileSidebar : Button {
    [Export]
    public string Path = "";

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        // pain
        //if (CabinetfsManager.PathExists(Path))
        //    GetNode<FileView>("../../../../Content/ContentThing/ItemList").Refresh(Path);
    }
}
