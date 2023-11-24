using Godot;
using System;
using Kickstart.Drivers;

namespace Dashboard.Pinboard;

public partial class PinboardSelectThingy : Node2D
{
    public static Rect2 IncreaseSize;
    public static Rect2 DecreaseSize;
    public static Rect2 RemoveSticker;
    [Export]
    Panel IncreaseSizePanel;
    [Export]
    Panel DecreaseSizePanel;
    [Export]
    Panel DeletePanel;
    [Export]
    Control Yes;
    [Export]
    Control WindowThemeStuff;

    public override void _Ready()
    {
        base._Ready();
        Yes.Size = ResolutionManager.Resolution;
        IncreaseSize = IncreaseSizePanel.GetRect().GrowIndividual(-80, -20, -80, -20);
        DecreaseSize = DecreaseSizePanel.GetRect().GrowIndividual(-80, -20, -80, -20);
        RemoveSticker = DeletePanel.GetRect().GrowIndividual(-80, -20, -80, -20);
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        // themes are certainly cool & stuff
        Yes.Theme = WindowThemeStuff.Theme;

        // so true
        Visible = Sticker.SelectedSticker != null;
    }
}
