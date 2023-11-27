using Godot;
using System;
using System.Numerics;

namespace Kickstart.Drivers;

/// <summary>
/// Uhhhhhhhh
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
        // if we're on desktop we can use the real resolution, but on mobile the game stretches vertically
        // and expands horizontally
        if (OS.GetName() != "Android")
        {
            GetTree().Root.ContentScaleMode = Window.ContentScaleModeEnum.Disabled;
            GetTree().Root.ContentScaleAspect = Window.ContentScaleAspectEnum.Expand;
        }
    }
}
