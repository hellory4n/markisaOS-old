using Godot;
using System;

public class InstallelYes : Node2D {
    public override void _Ready() {
        base._Ready();
        Vector2 bruh = ResolutionManager.Resolution;
        GetNode<Viewport>("1/Windows").Size = bruh;

        var hfbjgfj = GetNode<BaseWindow>("1/Windows/ThemeThing/Installel");
        // j
        if (Name == "InstallelOobe")
            hfbjgfj.RectPosition = new Vector2(74892839, 89958439);
        hfbjgfj.Visible = true;
        hfbjgfj.Minimize.Visible = false;
        hfbjgfj.Maximize.Visible = false;
        hfbjgfj.GetCloseButton().Visible = false;
    }
}
