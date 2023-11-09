using Godot;
using System;

public partial class InstallerNormalNext : Button {
    [Export]
    NodePath nextThing = "";
    [Export]
    NodePath previousThing = "";

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        GetNode<Control>(nextThing).Visible = true;
        GetNode<Control>(previousThing).Visible = false;
    }
}
