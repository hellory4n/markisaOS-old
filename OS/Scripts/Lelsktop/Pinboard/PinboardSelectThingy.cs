using Godot;
using System;

public class PinboardSelectThingy : Node2D {
    Control Yes;

    public override void _Ready() {
        base._Ready();
        Yes = GetNode<Control>("Yes");
    }

    public override void _Process(float delta) {
        base._Process(delta);
        Yes.RectSize = ResolutionManager.Resolution;
        
        // themes are certainly cool & stuff
        Yes.Theme = GetNode<Control>("../1/Windows/ThemeThing").Theme;

        // so true
        Visible = Sticker.SelectedSticker != null;
    }
}
