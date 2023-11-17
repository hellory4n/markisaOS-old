using Godot;
using Lelcore.Drivers;
using Lelsktop.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

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

        // put it on the center of the screen
        window.Position = WindowSpace/2 - (window.Size/2);
        
        // windows are maximized by default on mobile
        if (OS.GetName() == "Android" && !window.Unresizable)
        {
            window.Position = new Vector2I(0, 85);
            window.Size = WindowSpace;
        }

        // add it to the dock
        OpenWindowButton coolDockButton = (OpenWindowButton)OpenWindow.Instantiate();
        coolDockButton.Init(window);
        VBoxContainer dock = GetNode<VBoxContainer>("/root/LelsktopInterface/Dock/DockStuff/Running");
        dock.AddChild(coolDockButton);
        window.DockButton = coolDockButton;
    }

    /// <summary>
    /// Switches between workspaces.
    /// </summary>
    /// <param name="workspace">The workspace to switch to (1-4)</param>
    public void SwitchWorkspace(int workspace)
    {
        if (workspace < 1 || workspace > 4)
            return;
        
        // neither making viewports invisible or changing their size worked, so i decided to multiply/divide
        // by 10 the position of every window lol
        switch (workspace)
        {
            // i know
            case 1:    
                foreach (Lelwindow window in GetNode("/root/Lelsktop/1/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = true;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/2/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/3/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/4/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                break;

            case 2:
                foreach (Lelwindow window in GetNode("/root/Lelsktop/1/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/2/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = true;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/3/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/4/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                break;
            
            case 3:
                foreach (Lelwindow window in GetNode("/root/Lelsktop/1/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/2/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/3/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = true;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/4/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                break;
            
            case 4:
                foreach (Lelwindow window in GetNode("/root/Lelsktop/1/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/2/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/3/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = false;
                }
                foreach (Lelwindow window in GetNode("/root/Lelsktop/4/Windows/ThemeThing").GetChildren()
                .Cast<Lelwindow>())
                {
                    if (!window.IsMinimized)
                        window.Visible = true;
                }
                break;
        }
    }
}
