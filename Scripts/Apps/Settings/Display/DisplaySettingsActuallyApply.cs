using Godot;
using System;

public class DisplaySettingsActuallyApply : Button {
    public Vector2 Resolution;
    public float ScalingFactor;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        DisplaySettings m = new DisplaySettings {
            Resolution = Resolution,
            ScalingFactor = ScalingFactor,
            AlreadySetup = true
        };
        SavingManager.SaveSettings(m);

        GetParent<BaseWindow>().Visible = false;
    }
}
