using Godot;
using System;

// This node adapts the game's resolution and screen factor dynamically,
// so you get more ui on desktop and stuff
public class ResolutionManager : Node2D {
    public override void _Ready() {
        base._Ready();

        // this lowers the resolution at mobile so it's more touch-friendly
        if (OS.GetName() == "Android") {
            // we shouldn't put that much zoom on a tablet
            Vector2 funni = OS.GetScreenSize();
            if (funni.x/funni.y != 1.3) {
                GetTree().SetScreenStretch(SceneTree.StretchMode.Mode2d, SceneTree.StretchAspect.Keep, 
                    OS.GetScreenSize()/1.75f);
            } else {
                GetTree().SetScreenStretch(SceneTree.StretchMode.Mode2d, SceneTree.StretchAspect.Keep, 
                    OS.GetScreenSize()/1.25f);
            }
        } else {
            GetTree().SetScreenStretch(SceneTree.StretchMode.Mode2d, SceneTree.StretchAspect.Keep, 
                OS.GetScreenSize());
        }
    }

    public static Vector2 GetScreenSize() {
        Vector2 resolution;
        if (OS.GetName() == "Android") {
            Vector2 funni = OS.GetScreenSize();
            if (funni.x/funni.y != 1.3)
                resolution = OS.GetScreenSize()/1.75f;
            else
                resolution = OS.GetScreenSize()/1.25f;
        } else {
            resolution = OS.GetScreenSize();
        }

        return resolution;
    }
}
