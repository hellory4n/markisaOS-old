using Godot;
using System;

public partial class OnboardingWindowStuff : Node2D {
    public override void _Ready() {
        base._Ready();

        Window window = GetNode<Window>("Control/Thing");
        window.Visible = true;
        window.GetCloseButton().Visible = false;

        Vector2 pain = ResolutionManager.Resolution;

        if (window.WindowTitle == "New User") {
            window.Size = new Vector2(pain.x*0.75f, pain.y*0.8f);
            window.GetNode<ItemList>("ScrollContainer/CenterContainer/VBoxContainer/Icons")
                .CustomMinimumSize = new Vector2(596, 0);
        } else {
            window.Size = new Vector2(pain.x*0.35f, pain.y*0.75f);
        }

        window.Position = pain/2-((window.Size/2)-new Vector2(0, 22.5f));

        window.GetNode<CenterContainer>("ScrollContainer/CenterContainer").CustomMinimumSize = 
            window.GetNode<ScrollContainer>("ScrollContainer").Size;
        
        if (window.WindowTitle == "New User") {
            window.GetNode<ItemList>("ScrollContainer/CenterContainer/VBoxContainer/Icons")
                .Position += new Vector2(17, 0);
        }
    }
}
