using Godot;
using System;

// This node adapts the game's resolution and screen factor dynamically,
// so you get more ui on desktop and stuff
public class ResolutionManager : Node {
    // we could just load the settings but then the game is constantly reading a single file, quite inconvenient
    public static Vector2 Resolution = new Vector2(1280, 1080);

    public override void _Ready() {
        base._Ready();
        Update();
    }

    public void Update() {
        DisplaySettings displaySettings = SavingManager.LoadSettings<DisplaySettings>();
        Resolution = displaySettings.Resolution/displaySettings.ScalingFactor;

        GetTree().SetScreenStretch(SceneTree.StretchMode.Mode2d, SceneTree.StretchAspect.Keep, Resolution);
    }
}
