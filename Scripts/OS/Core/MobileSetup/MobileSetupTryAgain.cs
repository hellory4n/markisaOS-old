using Godot;
using System;

public class MobileSetupTryAgain : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        DisplaySettings display = SavingManager.LoadSettings<DisplaySettings>(
            SavingManager.Settings.DisplaySettings
        );
        display.ScalingFactor += 0.1f;
        SavingManager.SaveSettings(SavingManager.Settings.DisplaySettings, display);
        ResolutionManager m = GetNode<ResolutionManager>("/root/ResolutionManager");
        m.Update();

        // update stuff :)
        PackedScene aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/MobileSetup.tscn");
        Node aNode = aPackedScene.Instance();
        aNode.Name = "mfkmenkglmbnfkg";
        GetTree().Root.AddChild(aNode);

        GetParent().GetParent().QueueFree();
    }
}
