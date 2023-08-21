using Godot;
using System;

public class ApplicationMenu : Panel {
    public override void _Ready() {
        base._Ready();
        Vector2 screenSize = ResolutionManager.GetScreenSize();
        RectMinSize = screenSize - new Vector2(0, 140);
        RectPosition = new Vector2(0, 40);
    }
}
