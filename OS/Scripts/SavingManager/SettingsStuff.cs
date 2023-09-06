using Godot;

/// <summary>
/// Stores the display settings, as well as whether or not the Mobile Setup screen should be shown.
/// </summary>
class DisplaySettings {
    public Vector2 Resolution = new Vector2(1280, 720);
    public float ScalingFactor = 1;
    public bool AlreadySetup = false;
}