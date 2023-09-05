using Godot;
using System;

public class SetSizeToScreenResolution : Control {
    [Export]
    Vector2 Offset = new Vector2(0, 0);

    public override void _Ready() {
        base._Ready();
        RectSize = ResolutionManager.Resolution - Offset;
    }
}
