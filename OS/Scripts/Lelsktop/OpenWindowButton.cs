using Godot;
using System;
using Lelsktop.Wm;
using Lelcore.Drivers;

namespace Lelsktop.Interface;

public partial class OpenWindowButton : Button
{
    Lelwindow Window;

    // called when the window manager opens a window
    public void Init(Lelwindow window)
    {
        Window = window;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        TooltipText = Window.Title;
        Icon = Window.Icon;
    }

    public override void _Pressed()
    {
        base._Pressed();

        // minimize the window if it's active :)
        if (Window.HasFocus() && Window.Visible)
            Window.Visible = false;
        // already minimized
        else if (!Window.Visible /*&& WindowManager.CurrentWorkspace == Window.GetViewport()*/)
        {
            Window.Visible = true;
            Window.MoveToForeground();
        }
        else
            Window.MoveToForeground();
    }
}
