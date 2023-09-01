using Godot;
using System;

public class DisplaySettingsRevert : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        ResolutionManager resolutionManager = GetNode<ResolutionManager>("/root/ResolutionManager");

        // since the settings aren't actually saved we can just do this
        resolutionManager.Update();

        GetParent<BaseWindow>().Visible = false;
        GetParent().GetNode<Timer>("Timer").QueueFree();
    }
}
