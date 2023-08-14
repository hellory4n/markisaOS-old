using Godot;
using System;

public class Lelsktop : Control {
    public override void _Ready() {
        base._Ready();
        Vector2 bruh = ResolutionManager.GetScreenSize();
        GD.Print($"Screen resolution is {bruh.x}, {bruh.y}");
    }
}
