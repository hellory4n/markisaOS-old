using Godot;
using System;

/// <summary>
/// Adapts the resolution and UI of the game, so you can get the screen resolution on desktop, and a bigger UI on mobile to make touching things easier.
/// </summary>
public partial class ResolutionManager : Node {
    /// <summary>
    /// We could just load the settings everytime something needs the resolution but then the game is constantly reading a single file, quite inconvenient
    /// </summary>
    public static Vector2 Resolution = new Vector2(1280, 1080);

    public override void _Ready() {
        base._Ready();
        Update();
    }

    /// <summary>
    /// Reads the display settings files again and updates the display accordingly.
    /// </summary>
    public void Update() {
        DisplaySettings displaySettings = SavingManager.LoadSettings<DisplaySettings>();
        Resolution = displaySettings.Resolution/displaySettings.ScalingFactor;

        GetTree().SetScreenStretch(SceneTree.StretchMode.Mode2d, SceneTree.StretchAspect.Keep, Resolution);
    }
}
