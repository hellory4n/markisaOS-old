using Godot;
using System;

namespace Lelcore.Installel;

public partial class InstallerLicenseNext : Button
{
    [Export]
    Control NextThing;
    [Export]
    Control PreviousThing;

    /*public override void _Process(double delta) {
        base._Process(delta);
        Disabled = !GetNode<CheckBox>("../CheckBox").ToggleMode;
    }*/

    public override void _Pressed()
    {
        base._Pressed();
        NextThing.Visible = true;
        PreviousThing.Visible = false;
    }
}
