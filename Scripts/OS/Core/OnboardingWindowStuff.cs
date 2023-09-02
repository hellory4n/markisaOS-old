using Godot;
using System;

public class OnboardingWindowStuff : Node2D {
    public override void _Ready() {
        base._Ready();

        WindowDialog window = GetNode<WindowDialog>("Control/Thing");
        window.Visible = true;
        window.GetCloseButton().Visible = false;

        Vector2 pain = ResolutionManager.Resolution;

        if (window.WindowTitle == "New User") {
            window.RectSize = new Vector2(pain.x*0.75f, pain.y*0.8f);
            window.GetNode<ItemList>("ScrollContainer/CenterContainer/VBoxContainer/Icons")
                .RectMinSize = new Vector2(596, 0);
        } else {
            window.RectSize = new Vector2(pain.x*0.35f, pain.y*0.75f);
        }

        window.RectPosition = pain/2-((window.RectSize/2)-new Vector2(0, 22.5f));

        window.GetNode<CenterContainer>("ScrollContainer/CenterContainer").RectMinSize = 
            window.GetNode<ScrollContainer>("ScrollContainer").RectSize;
        
        if (window.WindowTitle == "New User") {
            window.GetNode<ItemList>("ScrollContainer/CenterContainer/VBoxContainer/Icons")
                .RectPosition += new Vector2(17, 0);
        }
    }
}
