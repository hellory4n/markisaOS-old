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
        // we wouldn't minimize a window in another workspace tho
        if (Window.HasFocus() && !Window.IsMinimized /*&& WindowManager.CurrentWorkspace ==
        Window.GetViewport()*/)
            Window.IsMinimized = true;
        // already minimized
        // we wouldn't restore a window in another workspace tho
        else if (Window.IsMinimized /*&& WindowManager.CurrentWorkspace == Window.GetViewport()*/)
        {
            Window.IsMinimized = false;
            Window.MoveToForeground();
        }
        else
            Window.MoveToForeground();

        // switch to a different workspace if necessary
        // pain
        if (!Window.GetParent().GetParent().GetParent<SubViewportContainer>().Visible)
        {
            // this is dumb
            GetNode<WindowManager>("/root/WindowManager").SwitchWorkspace(
                int.Parse(Window.GetParent().GetParent().GetParent().Name)
            );
        }
    }
}
