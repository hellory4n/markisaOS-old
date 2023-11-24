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
        DisplaySettings m = new()
        {
            Resolution = Resolution,
            ScalingFactor = ScalingFactor,
            AlreadySetup = true
        };
        SavingManager.SaveSettings(m);

        PackedScene aPackedScene = GD.Load<PackedScene>("res://OS/Core/Onboarding.tscn");
        Node aNode = aPackedScene.Instantiate();
        GetTree().Root.AddChild(aNode);
        GetNode("/root/Dashboard").QueueFree();
        GetNode("/root/DashboardInterface").QueueFree();
    }
}
