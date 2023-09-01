using Godot;
using System;

// This node adapts the game's resolution and screen factor dynamically,
// so you get more ui on desktop and stuff
public class ResolutionManager : Node {
    public override void _Ready() {
        base._Ready();
        Update();
    }

    public void Update() {
        DisplaySettings displaySettings = SavingManager.LoadSettings<DisplaySettings>();

        GetTree().SetScreenStretch(SceneTree.StretchMode.Mode2d, SceneTree.StretchAspect.Keep, 
            displaySettings.Resolution/displaySettings.ScalingFactor);
    }

    public static Vector2 GetScreenSize() {
        Vector2 resolution;
        DisplaySettings displaySettings = SavingManager.LoadSettings<DisplaySettings>();
        resolution = displaySettings.Resolution/displaySettings.ScalingFactor;
        return resolution;
    }
}
