using Godot;
using System;

public class MobileSetupButton : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        MobileSetup yes = GetParent().GetParent<MobileSetup>();

        // this seems right
        if (yes.Taps == 0) {
            PackedScene aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/Bootscreen.tscn");
            Node aNode = aPackedScene.Instance();
            aNode.Name = "fjdkfjeplkhjgidlmfgr";
            GetTree().Root.AddChild(aNode);
        // this is not right
        } else {
            DisplaySettings display = SavingManager.LoadSettings<DisplaySettings>(
                SavingManager.Settings.DisplaySettings
            );
            display.ScalingFactor += 0.25f;
            SavingManager.SaveSettings(SavingManager.Settings.DisplaySettings, display);
            ResolutionManager m = GetNode<ResolutionManager>("/root/ResolutionManager");
            m.Update();

            // update stuff :)
            PackedScene aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/MobileSetup.tscn");
            Node aNode = aPackedScene.Instance();
            GetTree().Root.AddChild(aNode);

        }

        GetParent().GetParent().QueueFree();
    }
}
