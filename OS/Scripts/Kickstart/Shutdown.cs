using Godot;
using System;

namespace Kickstart.Bootloader;

public partial class Shutdown : Timer
{
    public override void _Ready()
    {
        base._Ready();
        Input.WarpMouse(Vector2.Zero);
        Input.MouseMode = Input.MouseModeEnum.ConfinedHidden;
    }

    public void Thing()
    {
        GetTree().Quit();
    }
}
