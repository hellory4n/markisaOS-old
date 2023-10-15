using Godot;
using System;

public class InstallerLicenseNext : Button {
    [Export]
    NodePath nextThing = "";
    [Export]
    NodePath previousThing = "";

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public override void _Process(float delta) {
        base._Process(delta);
        Disabled = !GetNode<CheckBox>("../CheckBox").Pressed;
    }

    public void Click() {
        GetNode<Control>(nextThing).Visible = true;
        GetNode<Control>(previousThing).Visible = false;
    }
}
