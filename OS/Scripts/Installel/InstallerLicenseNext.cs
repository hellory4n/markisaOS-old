using Godot;
using System;

public partial class InstallerLicenseNext : Button {
    [Export]
    NodePath nextThing = "";
    [Export]
    NodePath previousThing = "";

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public override void _Process(double delta) {
        base._Process(delta);
        Disabled = !GetNode<CheckBox>("../CheckBox").Pressed;
    }

    public void Click() {
        GetNode<Control>(nextThing).Visible = true;
        GetNode<Control>(previousThing).Visible = false;
    }
}
