using Godot;
using Kickstart.Drivers;
using System;

namespace Kickstart.Bootloader;

public partial class Boot : Control
{
    [Export]
    Sprite2D Boot1;

    public override void _Ready()
    {
        base._Ready();
        CustomMinimumSize = ResolutionManager.Resolution;
        Boot1.Position = ResolutionManager.Resolution/2;
    }
}
