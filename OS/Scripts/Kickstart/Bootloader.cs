using Godot;
using Kickstart.Records;
using System;

namespace Kickstart.Bootloader;

public partial class Bootloader : Node
{
    bool installing = false;
    [Export(PropertyHint.File, "*.tscn")]
    public string GetMeStarted;
    [Export]
    public AnimationPlayer HahaYes;
    bool VariableToPreventTheGameFromLoadingTheOnboardingScreenOrInstaller58394834Times = false;

    public override void _Ready()
    {
        base._Ready();
        // if we're gonna install markisaOS then the bootscreen should take longer and stuff
        /*if (!new Record<SystemInfo>().Data.Installed)
        {
            GetNode<Label>("../Control/Label").Text = "markisaOS Me is preparing the installation process.\nThis can take several seconds.";
            GetNode<Label>("../Control/Label").OffsetTop = -100;
            installing = true;
        }*/

        Input.WarpMouse(Vector2.Zero);
        Input.MouseMode = Input.MouseModeEnum.ConfinedHidden;

        Record<SystemInfo> record = new();
        TranslationServer.SetLocale(record.Data.Language);
    }

    public override void _Process(double delta)
    {
        // having to wait for the boot screen everytime i test it is very dogwater
        if (Input.IsActionJustReleased("skip_boot"))
            Thing();
        base._Process(delta);
    }

    public void Thing()
    {
        if (VariableToPreventTheGameFromLoadingTheOnboardingScreenOrInstaller58394834Times)
            return;

        PackedScene aPackedScene;
        if (installing)
            aPackedScene = GD.Load<PackedScene>(GetMeStarted);
        else
            aPackedScene = GD.Load<PackedScene>(new Record<SystemInfo>().Data.Onboarding);
        
        Node aNode = aPackedScene.Instantiate();
        GetTree().Root.AddChild(aNode);
        GetParent<Control>().MoveToFront();
        HahaYes.Play("GJsdighjshijsf");
        VariableToPreventTheGameFromLoadingTheOnboardingScreenOrInstaller58394834Times = true;
    }

    public void Thing2(string animName)
    {
        GetParent().QueueFree();
        Input.MouseMode = Input.MouseModeEnum.Visible;
    }
}
