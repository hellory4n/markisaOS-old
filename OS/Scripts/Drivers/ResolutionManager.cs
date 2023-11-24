using Godot;
using System;
using System.Numerics;

namespace Kickstart.Drivers;

/// <summary>
/// Adapts the resolution and UI of the game, so you can get the screen resolution on desktop, and a bigger UI on mobile to make touching things easier.
/// </summary>
public partial class ResolutionManager : Node
{
    /// <summary>
    /// We could just load the settings everytime something needs the resolution but then the game is constantly reading a single file, quite inconvenient
    /// </summary>
    public static Vector2I Resolution = new(1280, 720);

    public override void _Ready()
    {
        base._Ready();
        Update();
    }

    /// <summary>
    /// Reads the display settings files again and updates the display accordingly.
    /// </summary>
    public void Update()
    {
        DisplaySettings displaySettings = SavingManager.LoadSettings<DisplaySettings>();
        Resolution = (Vector2I)(displaySettings.Resolution/displaySettings.ScalingFactor);

        GetTree().Root.Size = Resolution;
    }
}
