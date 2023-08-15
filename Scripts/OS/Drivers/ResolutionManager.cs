using Godot;
using System;

// This node adapts the game's resolution and screen factor dynamically,
// so you get more ui on desktop and stuff
public class ResolutionManager : Node2D {
    public override void _Ready() {
        base._Ready();

        // this lowers the resolution at mobile so it's more touch-friendly
        if (OS.GetName() == "Android") {
            GetTree().SetScreenStretch(SceneTree.StretchMode.Mode2d, SceneTree.StretchAspect.Keep, 
                OS.GetScreenSize()/1.5f);
        } else {
            GetTree().SetScreenStretch(SceneTree.StretchMode.Mode2d, SceneTree.StretchAspect.Keep, 
                OS.GetScreenSize());
        }
    }

    public static Vector2 GetScreenSize() {
        Vector2 resolution;
        if (OS.GetName() == "Android")
            resolution = OS.GetScreenSize()/1.5f;
        else
            resolution = OS.GetScreenSize();

        return resolution;
    }
}
