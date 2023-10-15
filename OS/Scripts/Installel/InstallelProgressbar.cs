using Godot;
using System;

public class InstallelProgressbar : ProgressBar {
    Random random = new Random();

    public override void _Process(float delta) {
        base._Process(delta);
        if (!GetParent<Control>().Visible)
            return;

        // makes it look janky
        if (random.Next(0, 10) == 1)
            Value += random.Next(1, 5);
    }
}
