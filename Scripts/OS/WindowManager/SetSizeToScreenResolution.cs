using Godot;
using System;

public class SetSizeToScreenResolution : Control {
    public override void _Ready() {
        base._Ready();
        RectSize = ResolutionManager.GetScreenSize();
    }
}
