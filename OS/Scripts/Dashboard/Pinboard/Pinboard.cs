using Dashboard.Interface;
using Godot;
using System;

namespace Dashboard.Pinboard;

public partial class Pinboard : Button
{
    public static bool EditingPinboard = false;

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        // quite the mouthful
        GetNode<Dashboard>("/root/Dashboard").Interface.GetNode<ShowDesktop>("Panel/ShowDesktop")._Toggled(toggledOn);
        EditingPinboard = toggledOn;
    }
}
