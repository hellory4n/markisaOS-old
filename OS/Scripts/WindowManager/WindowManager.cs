using Godot;
using Kickstart.Drivers;
using Dashboard.Interface;

namespace Dashboard.Wm;

/// <summary>
/// Responsible for managing all windows in the dashboard.
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
        OpenWindow = GD.Load<PackedScene>("res://OS/Dashboard/OpenWindowButton.tscn");
        WindowSpace = ResolutionManager.Resolution + new Vector2I(-85, 40);
    }

    /// <summary>
    /// Opens a window in the dashboard.
    /// </summary>
    /// <param name="window">The window to open.</param>
    public void AddWindow(MksWindow window)
    {
        Control dashboard = GetNode<Dashboard>("/root/Dashboard").Windows.GetNode<Control>("ThemeThing");
        dashboard.AddChild(window);

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
        Node dock = GetNode<Dashboard>("/root/Dashboard").Interface.GetNode("Dock/DockStuff/Running");
        dock.AddChild(coolDockButton);
        window.DockButton = coolDockButton;
    }
}
