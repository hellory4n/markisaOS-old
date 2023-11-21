using Godot;
using Lelsktop.Wm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lelsktop.Interface;

public partial class ShowDesktop : Button
{
    List<Lelwindow> Windows = new();

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        if (toggledOn)
        {
            Windows = new List<Lelwindow>();

            // find every window ever
            foreach (Lelwindow window in GetNode<Lelsktop>("/root/Lelsktop").Windows.GetNode("ThemeThing")
            .GetChildren().Cast<Lelwindow>())
            {
                Windows.Add(window);
                window.Visible = false;
            }
        }
        else
        {
            foreach (var animator in Windows)
            {
                if (IsInstanceValid(animator))
                    animator.Visible = true;
            }
            Windows = new List<Lelwindow>();
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        // yes.
        if (!HasFocus())
            SetPressedNoSignal(false);
    }
}
