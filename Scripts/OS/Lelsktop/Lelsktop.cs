using Godot;
using System;

public class Lelsktop : Node2D {
    public override void _Ready() {
        base._Ready();
        Vector2 bruh = ResolutionManager.GetScreenSize();
        GD.Print($"Screen resolution is {bruh.x}, {bruh.y}");

        Viewport pain = GetNode<Viewport>("/root/Lelsktop/Thing/Windows");
        pain.Size = bruh;

        // startup sound :)
        SoundManager sounds = GetNode<SoundManager>("/root/SoundManager");
        sounds.PlaySoundEffect(SoundManager.SoundEffects.Startup);

        // cool dock :)
        PackedScene m = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/LelsktopInterface.tscn");
        CanvasLayer lelsktopInterface = (CanvasLayer)m.Instance();
        GetTree().Root.CallDeferred("add_child", lelsktopInterface);
        lelsktopInterface.GetNode<Panel>("Dock").RectSize = new Vector2(bruh.x, 80);

        // play the animation for the dock and make sure the position on the animation is correct :)
        Animation animationomg = lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation("Startup");
        // this didn't work
        // int track = animationomg.FindTrack("Painful/Panel"); 
        int keyStart = animationomg.TrackFindKey(0, 0);
        int keyEnd = animationomg.TrackFindKey(0, 0.5f);
        animationomg.TrackSetKeyValue(0, keyStart, new Vector2(0, bruh.y));
        animationomg.TrackSetKeyValue(0, keyEnd, new Vector2(0, bruh.y-75));
        lelsktopInterface.GetNode<AnimationPlayer>("AnimationPlayer").Play("Startup");
    }

    public override void _Process(float delta) {
        base._Process(delta);
        Vector2 pain = ResolutionManager.GetScreenSize();
        Viewport bruh = GetNode<Viewport>("/root/Lelsktop/Thing/Windows");
        Panel appMenu = GetNode<Panel>("/root/LelsktopInterface/AppMenu");
        Panel quickSettings = GetNode<Panel>("/root/LelsktopInterface/QuickSettings");
        Color invisible = new Color(1, 1, 1, 0);

        if (GetGlobalMousePosition().y < 40 || GetGlobalMousePosition().y > pain.y-75 ||
        appMenu.Modulate != invisible || quickSettings.Modulate != invisible) {
            bruh.GuiDisableInput = true;
        } else {
            bruh.GuiDisableInput = false;
        }
    }
}
