using Godot;

/// <summary>
/// Stores the display settings, as well as whether or not the Mobile Setup screen should be shown.
/// </summary>
class DisplaySettings {
    public Vector2 Resolution = new(1280, 720);
    public float ScalingFactor = 1;
    public bool AlreadySetup = false;
}

/// <summary>
/// This is just used to check if the installer should be shown
/// </summary>
class InstallerInfo {
    public bool IsInstalled = false;
}