using Godot;
using System;

public class DisplaySettingsApply : Button {
    public override void _Ready() {
        base._Ready();

        // set the value of the spin boxes to the current settings
        DisplaySettings display = SavingManager.LoadSettings<DisplaySettings>();
    }
}
