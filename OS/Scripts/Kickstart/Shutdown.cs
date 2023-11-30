using Godot;
using System;

namespace Kickstart.Bootloader;

public partial class Shutdown : Timer
{
    public void Thing() {
        GetTree().Quit();
    }
}
