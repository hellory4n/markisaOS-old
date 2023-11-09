using Godot;
using System;

public partial class DisplaySettingsActuallyApply : Button {
    public Vector2 Resolution;
    public float ScalingFactor;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        DisplaySettings m = new DisplaySettings {
            Resolution = Resolution,
            ScalingFactor = ScalingFactor,
            AlreadySetup = true
        };
        SavingManager.SaveSettings(m);

        PackedScene aPackedScene = ResourceLoader.Load<PackedScene>("res://OS/Core/Onboarding.tscn");
        Node aNode = aPackedScene.Instance();
        GetTree().Root.AddChild(aNode);
        GetNode("/root/Lelsktop").QueueFree();
        GetNode("/root/LelsktopInterface").QueueFree();
    }
}
