using Godot;
using System;

namespace Kickstart.Onboarding;

public partial class NotTheRealShutdown : Button
{
	[Export]
	public Node TheOnboardingThingy;
	[Export(PropertyHint.File, "*.tscn")]
	public string Shutdown = "";

    public override void _Pressed()
    {
        base._Pressed();
		PackedScene fsck = GD.Load<PackedScene>(Shutdown);
		Node fucker = fsck.Instantiate();
		GetTree().Root.AddChild(fucker);
		TheOnboardingThingy.QueueFree();
    }
}
