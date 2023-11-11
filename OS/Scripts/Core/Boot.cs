using Godot;
using Lelcore.Drivers;
using System;

namespace Lelcore.Bootloader;

public partial class Boot : Control
{
    [Export]
    Sprite2D Boot1;
    [Export]
    Sprite2D Boot2;

    public override void _Ready()
    {
        base._Ready();
        CustomMinimumSize = ResolutionManager.Resolution;
        Boot1.Position = ResolutionManager.Resolution/2;
        Boot2.Position = ResolutionManager.Resolution/2;
    }
}
