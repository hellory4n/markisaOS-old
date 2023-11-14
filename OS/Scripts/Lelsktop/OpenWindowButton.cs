using Godot;
using System;
using Lelsktop.Wm;

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
        if (Window.HasFocus() && Window.Visible /*&& WindowManager.CurrentWorkspace ==
        Window.GetViewport()*/)
            Window.Visible = false;
        // already minimized
        // we wouldn't restore a window in another workspace tho
        else if (!Window.Visible /*&& WindowManager.CurrentWorkspace == Window.GetViewport()*/)
        {
            Window.Visible = true;
            Window.MoveToForeground();
        }
        else
            Window.MoveToForeground();

        // switch to a different workspace if necessary
        // pain
        /*if (!Window.GetViewport().GetParent<SubViewportContainer>().Visible) {
            switch (Window.GetViewport().GetParent().Name) {
                case "1":
                    GetNode<SubViewportContainer>("/root/Lelsktop/1").Visible = true;
                    GetNode<SubViewportContainer>("/root/Lelsktop/2").Visible = false;
                    GetNode<SubViewportContainer>("/root/Lelsktop/3").Visible = false;
                    GetNode<SubViewportContainer>("/root/Lelsktop/4").Visible = false;
                    WindowManager.CurrentWorkspace = GetNode<SubViewport>("/root/Lelsktop/1/Windows");
                    break;
                case "2":
                    GetNode<SubViewportContainer>("/root/Lelsktop/1").Visible = false;
                    GetNode<SubViewportContainer>("/root/Lelsktop/2").Visible = true;
                    GetNode<SubViewportContainer>("/root/Lelsktop/3").Visible = false;
                    GetNode<SubViewportContainer>("/root/Lelsktop/4").Visible = false;
                    WindowManager.CurrentWorkspace = GetNode<SubViewport>("/root/Lelsktop/2/Windows");
                    break;
                case "3":
                    GetNode<SubViewportContainer>("/root/Lelsktop/1").Visible = false;
                    GetNode<SubViewportContainer>("/root/Lelsktop/2").Visible = false;
                    GetNode<SubViewportContainer>("/root/Lelsktop/3").Visible = true;
                    GetNode<SubViewportContainer>("/root/Lelsktop/4").Visible = false;
                    WindowManager.CurrentWorkspace = GetNode<SubViewport>("/root/Lelsktop/3/Windows");
                    break;
                case "4":
                    GetNode<SubViewportContainer>("/root/Lelsktop/1").Visible = false;
                    GetNode<SubViewportContainer>("/root/Lelsktop/2").Visible = false;
                    GetNode<SubViewportContainer>("/root/Lelsktop/3").Visible = false;
                    GetNode<SubViewportContainer>("/root/Lelsktop/4").Visible = true;
                    WindowManager.CurrentWorkspace = GetNode<SubViewport>("/root/Lelsktop/4/Windows");
                    break;
            }
        }*/
    }
}
