using Godot;
using Lelcore.Drivers;
using Lelsktop.Interface;
using System;
using System.Collections.Generic;

namespace Lelsktop.Wm;

/// <summary>
/// Responsible for managing all windows in the lelsktop.
/// </summary>
public partial class WindowManager : Node2D
{
    PackedScene OpenWindow;
    public static SubViewport CurrentWorkspace;

    public override void _Ready()
    {
        base._Ready();
        OpenWindow = GD.Load<PackedScene>("res://OS/Lelsktop/OpenWindowButton.tscn");
    }

    /// <summary>
    /// Opens a window in the lelsktop.
    /// </summary>
    /// <param name="window">The window to open.</param>
    public void AddWindow(Lelwindow window)
    {
        Control lelsktop = CurrentWorkspace.GetNode<Control>("ThemeThing");
        lelsktop.AddChild(window);

        // put it on the center of the screen
        Vector2I yes = ResolutionManager.Resolution;
        yes -= new Vector2I(85, 0);
        yes += new Vector2I(0, 40);
        window.Position = yes/2 - (window.Size/2);

        // add it to the dock
        OpenWindowButton coolDockButton = (OpenWindowButton)OpenWindow.Instantiate();
        coolDockButton.Init(window);
        VBoxContainer dock = GetNode<VBoxContainer>("/root/LelsktopInterface/Dock/DockStuff/Running");
        dock.AddChild(coolDockButton);

        // all windows are maximized by default on mobile
        if (OS.GetName() == "Android" && !window.Unresizable)
        {
            Vector2I maximizedSize = ResolutionManager.Resolution;
            maximizedSize = new Vector2I(maximizedSize.X-75, maximizedSize.Y-85);
            window.Position = new Vector2I(0, 85);
            window.Size = maximizedSize;
        }
    }
}
