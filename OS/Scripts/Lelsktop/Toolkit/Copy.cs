using Godot;
using System;

public partial class Copy : Button {
    [Export]
    public string TextToCopy = "";

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        OS.Clipboard = TextToCopy;
    }
}
