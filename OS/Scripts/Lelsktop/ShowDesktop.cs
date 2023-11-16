using Godot;
using Lelsktop.Wm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lelsktop.Interface;

public partial class ShowDesktop : Button
{
    List<Lelwindow> WindowAnimators = new();

    public override void _Toggled(bool toggledOn)
    {
        base._Toggled(toggledOn);
        if (toggledOn)
        {
            WindowAnimators = new List<Lelwindow>();

            // find every window ever
            foreach (Lelwindow window in WindowManager.CurrentWorkspace.GetNode("ThemeThing")
            .GetChildren().Cast<Lelwindow>())
            {
                WindowAnimators.Add(window);
                window.Visible = false;
            }
        }
        else
        {
            foreach (var animator in WindowAnimators)
            {
                if (IsInstanceValid(animator))
                    animator.Visible = true;
            }
            WindowAnimators = new List<Lelwindow>();
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
