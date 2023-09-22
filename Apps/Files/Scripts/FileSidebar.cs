using Godot;
using System;

public class FileSidebar : Button {
    [Export]
    public string Path = "";

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        // pain
        if (LelfsManager.FileExists(Path))
            GetNode<FileView>("../../../Content/ContentThing/ItemList").Refresh(Path);
    }
}