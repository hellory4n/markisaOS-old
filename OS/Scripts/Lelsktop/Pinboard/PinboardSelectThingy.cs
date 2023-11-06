using Godot;
using System;

public class PinboardSelectThingy : Node2D {
    Control Yes;
    public static Rect2 IncreaseSize;
    public static Rect2 DecreaseSize;
    public static Rect2 RemoveSticker;

    public override void _Ready() {
        base._Ready();
        Yes = GetNode<Control>("Yes");
        Yes.RectSize = ResolutionManager.Resolution;
        IncreaseSize = Yes.GetNode<Panel>("IncreaseSize").GetRect();
        DecreaseSize = Yes.GetNode<Panel>("DecreaseSize").GetRect();
        RemoveSticker = Yes.GetNode<Panel>("Delete").GetRect();
    }

    public override void _Process(float delta) {
        base._Process(delta);
        // themes are certainly cool & stuff
        Yes.Theme = GetNode<Control>("../1/Windows/ThemeThing").Theme;

        // so true
        Visible = Sticker.SelectedSticker != null;
    }
}
