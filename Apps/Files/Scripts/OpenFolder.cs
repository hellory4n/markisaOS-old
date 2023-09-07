using Godot;
using System;

public class OpenFolder : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        FileView thing = GetParent<FileView>();
        thing.Refresh(HintTooltip);
    }
}
