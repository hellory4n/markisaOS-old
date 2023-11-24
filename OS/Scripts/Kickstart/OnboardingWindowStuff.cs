using Godot;
using System;
using Kickstart.Drivers;

namespace Kickstart.Onboarding;

public partial class OnboardingWindowStuff : Node2D
{
    [Export]
    Window Window;

    public override void _Ready()
    {
        base._Ready();

        Vector2I pain = ResolutionManager.Resolution;

        if (Window.Title == "New User")
        {
            Window.Size = new Vector2I((int)(pain.X*0.75), (int)(pain.Y*0.8));
            Window.GetNode<ItemList>("ScrollContainer/CenterContainer/VBoxContainer/Icons")
                .CustomMinimumSize = new Vector2(596, 0);
        }
        else
        {
            Window.Size = new Vector2I((int)(pain.X*0.35), (int)(pain.Y*0.75));
        }

        Window.Position = pain/2 - ((Window.Size/2) - new Vector2I(0, 23));

        Window.GetNode<CenterContainer>("ScrollContainer/CenterContainer").CustomMinimumSize = 
            Window.GetNode<ScrollContainer>("ScrollContainer").Size;
        
        if (Window.Title == "New User")
        {
            Window.GetNode<ItemList>("ScrollContainer/CenterContainer/VBoxContainer/Icons")
                .Position += new Vector2(17, 0);
        }
    }
}
