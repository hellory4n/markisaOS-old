using Godot;
using System;

public class Copy : Button {
    [Export]
    public string TextToCopy = "";

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        OS.Clipboard = TextToCopy;
    }
}
