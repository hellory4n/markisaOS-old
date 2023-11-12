using Godot;
using System;
using System.Collections.Generic;

namespace Lelsktop.WindowManager;

/// <summary>
/// Responsible for managing all windows in the lelsktop.
/// </summary>
public partial class WindowManager : Node2D {
    /*PackedScene OpenWindow;
    public static SubViewport CurrentWorkspace;

    public override void _Ready() {
        base._Ready();
        OpenWindow = GD.Load<PackedScene>("res://OS/Lelsktop/OpenWindowButton.tscn");
    }

    /// <summary>
    /// Opens a window in the lelsktop.
    /// </summary>
    /// <param name="window">The window to open.</param>
    public void AddWindow(Lelwindow window) {
        Control lelsktop = CurrentWorkspace.GetNode<Control>("ThemeThing");
        lelsktop.AddChild(window);
        // using window.Popup_() makes it only work with 1 window, so this is a hack to bypass that
        window.Visible = true;

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
        if (OS.GetName() == "Android" && window.Resizable) {
            Vector2 maximizedSize = ResolutionManager.Resolution;
            maximizedSize = new Vector2(maximizedSize.x-75, maximizedSize.y-85);
            window.Position = new Vector2(0, 85);
            window.Size = maximizedSize;
        }
    }*/
}
