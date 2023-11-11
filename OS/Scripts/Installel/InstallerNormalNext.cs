using Godot;
using System;

namespace Lelcore.Installel;

public partial class InstallerNormalNext : Button
{
    [Export]
    Control NextThing;
    [Export]
    Control PreviousThing;

    public override void _Pressed()
    {
        base._Pressed();
        NextThing.Visible = true;
        PreviousThing.Visible = false;
    }
}
