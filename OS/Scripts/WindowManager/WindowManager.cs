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
    /// <summary>
    /// The size of a maximized window.
    /// </summary>
    public static Vector2I WindowSpace {get; private set;}

    public override void _Ready()
    {
        base._Ready();
        OpenWindow = GD.Load<PackedScene>("res://OS/Lelsktop/OpenWindowButton.tscn");
        WindowSpace = ResolutionManager.Resolution + new Vector2I(-85, 40);
    }

    /// <summary>
    /// Opens a window in the lelsktop.
    /// </summary>
    /// <param name="window">The window to open.</param>
    public void AddWindow(Lelwindow window)
    {
        Control lelsktop = CurrentWorkspace.GetNode<Control>("ThemeThing");
        lelsktop.AddChild(window);

        // add it to the dock
        OpenWindowButton coolDockButton = (OpenWindowButton)OpenWindow.Instantiate();
        coolDockButton.Init(window);
        VBoxContainer dock = GetNode<VBoxContainer>("/root/LelsktopInterface/Dock/DockStuff/Running");
        dock.AddChild(coolDockButton);
    }
}
