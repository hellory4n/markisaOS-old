using Godot;
using System;

// This node adapts the game's resolution and screen factor dynamically,
// so you get more ui on desktop and stuff
public class ResolutionManager : Camera2D {
    public override void _Ready() {
        base._Ready();
        Current = true;
        AnchorMode = AnchorModeEnum.FixedTopLeft;

        // this gives more zoom at mobile so it's more touch-friendly
        float zoomomgogmg;
        if (OS.GetName() == "Android")
            zoomomgogmg = 1280 / OS.GetScreenSize().x;
        else
            zoomomgogmg = OS.GetScreenSize().x / 1280;

        Zoom = new Vector2(zoomomgogmg, zoomomgogmg);
    }

    public static Vector2 GetScreenSize() {
        // get zoom level
        float zoomomgogmg;
        if (OS.GetName() == "Android")
            zoomomgogmg = 1280 / OS.GetScreenSize().x;
        else
            zoomomgogmg = OS.GetScreenSize().x / 1280;

        // get the resolution visible
        Vector2 result = new Vector2 {
            x = 1280 / (1 / zoomomgogmg),
            y = 720 / (1 / zoomomgogmg)
        };
        return result;
    }
}
