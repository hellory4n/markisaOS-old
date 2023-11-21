using Godot;
using Lelcore.Drivers;
using Lelsktop.Interface;

namespace Lelsktop.Wm;

/// <summary>
/// Responsible for managing all windows in the lelsktop.
/// </summary>
public partial class WindowManager : Node
{
    PackedScene OpenWindow;
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
        Control lelsktop = GetNode<Lelsktop>("/root/Lelsktop").Windows.GetNode<Control>("ThemeThing");
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
        Node dock = GetNode<Lelsktop>("/root/Lelsktop").Interface.GetNode("Dock/DockStuff/Running");
        dock.AddChild(coolDockButton);
        window.DockButton = coolDockButton;
    }
}
