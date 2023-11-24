using Godot;
using System;
using Kickstart.Drivers;

namespace Dashboard.Toolkit;

[GlobalClass]
public partial class SetSizeToScreenResolution : Control
{
    [Export]
    Vector2 Offset = new(0, 0);

    public override void _Ready()
    {
        base._Ready();
        Size = ResolutionManager.Resolution - Offset;
    }
}
