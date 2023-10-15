using Godot;
using System;

public class InstallerNormalNext : Button {
    [Export]
    NodePath nextThing = "";
    [Export]
    NodePath previousThing = "";

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        GetNode<Control>(nextThing).Visible = true;
        GetNode<Control>(previousThing).Visible = false;
    }
}
