using Godot;
using System;

namespace Kickstart.Installel;

public partial class InstallelProgressbar : ProgressBar
{
    Random random = new();

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (!GetParent<Control>().Visible)
            return;

        // makes it look janky
        if (random.Next(0, 10) == 1)
            Value += random.Next(1, 5);
    }
}
