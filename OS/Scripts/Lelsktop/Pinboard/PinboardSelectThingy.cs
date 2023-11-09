using Godot;
using System;

public partial class PinboardSelectThingy : Node2D {
    Control Yes;
    public static Rect2 IncreaseSize;
    public static Rect2 DecreaseSize;
    public static Rect2 RemoveSticker;

    public override void _Ready() {
        base._Ready();
        Yes = GetNode<Control>("Yes");
        Yes.Size = ResolutionManager.Resolution;
        IncreaseSize = Yes.GetNode<Panel>("IncreaseSize").GetRect().GrowIndividual(-80, -20, -80, -20);
        DecreaseSize = Yes.GetNode<Panel>("DecreaseSize").GetRect().GrowIndividual(-80, -20, -80, -20);
        RemoveSticker = Yes.GetNode<Panel>("Delete").GetRect().GrowIndividual(-80, -20, -80, -20);
    }

    public override void _Process(float delta) {
        base._Process(delta);
        // themes are certainly cool & stuff
        Yes.Theme = GetNode<Control>("../1/Windows/ThemeThing").Theme;

        // so true
        Visible = Sticker.SelectedSticker != null;
    }
}
