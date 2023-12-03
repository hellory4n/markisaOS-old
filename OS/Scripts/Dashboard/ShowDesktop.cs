using Godot;
using Dashboard.Wm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dashboard.Interface;

public partial class ShowDesktop : Button
{
    List<MksWindow> Windows = new();

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        if (toggledOn)
        {
            Windows = new List<MksWindow>();

            // find every window ever
            foreach (MksWindow window in GetNode<Dashboard>("/root/Dashboard").Windows.GetNode("ThemeThing")
            .GetChildren().Cast<MksWindow>())
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
            Windows = new List<MksWindow>();
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
