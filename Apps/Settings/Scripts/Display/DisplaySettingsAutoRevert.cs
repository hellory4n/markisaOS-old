using Godot;
using System;

public class DisplaySettingsAutoRevert : Timer {
    public override void _Ready() {
        base._Ready();
        Connect("timeout", this, nameof(Yes));
    }

    public void Yes() {
        ResolutionManager resolutionManager = GetNode<ResolutionManager>("/root/ResolutionManager");

        // since the settings aren't actually saved we can just do this
        resolutionManager.Update();

        GetParent<BaseWindow>().Close();
    }
}
