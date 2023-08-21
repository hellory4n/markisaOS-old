using Godot;
using System;

public class Lelsktop : Node2D {
    public override void _Ready() {
        base._Ready();
        Vector2 bruh = ResolutionManager.GetScreenSize();
        GD.Print($"Screen resolution is {bruh.x}, {bruh.y}");

        // cool dock :)
        GetNode<Panel>("Painful/Panel").RectSize = new Vector2(bruh.x, 80);

        // play the animation for the dock and make sure the position on the animation is correct :)
        Animation animationomg = GetNode<AnimationPlayer>("AnimationPlayer").GetAnimation("Startup");
        // this didn't work
        // int track = animationomg.FindTrack("Painful/Panel"); 
        int keyStart = animationomg.TrackFindKey(0, 0);
        int keyEnd = animationomg.TrackFindKey(0, 0.5f);
        animationomg.TrackSetKeyValue(0, keyStart, new Vector2(0, bruh.y));
        animationomg.TrackSetKeyValue(0, keyEnd, new Vector2(0, bruh.y-100));
        GetNode<AnimationPlayer>("AnimationPlayer").Play("Startup");
    }
}
