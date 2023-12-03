using Godot;
using System;
using Dashboard.Wm;
using Kickstart.Drivers;

namespace Dashboard.Interface;

public partial class OpenWindowButton : Button
{
    MksWindow Window;

    // called when the window manager opens a window
    public void Init(MksWindow window)
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
