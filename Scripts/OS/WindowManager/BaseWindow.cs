using Godot;
using System;

public class BaseWindow : WindowDialog {
    Vector2 screenSize;
    Vector2 previousPosition = new Vector2(0, 0);
    AnimationPlayer animation;

    public override void _Ready() {
        base._Ready();
        screenSize = ResolutionManager.GetScreenSize();

        PackedScene maximize = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Maximize.tscn");
        TextureButton yes = (TextureButton)maximize.Instance();
        AddChild(yes);

        PackedScene minimize = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Minimize.tscn");
        TextureButton perhaps = (TextureButton)minimize.Instance();
        AddChild(perhaps);

        PackedScene title = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/TitleTexture.tscn");
        Panel probably = (Panel)title.Instance();
        AddChild(probably);
        MoveChild(probably, 0);
        probably.GetNode<Label>("Label").Text = WindowTitle;

        // epic animation for opening the window, very important indeed
        animation = GetNode<AnimationPlayer>("AnimationPlayer");
        animation.Play("Open");
    }

    public override void _Process(float delta) {
        base._Process(delta);

        // windowdialog's close button just makes it invisible, this plays the close animation that also deletes
        // the window
        if (!Visible) {
            animation.Play("Close");
            Visible = true;
        }
        
        // window snapping :)
        // first check if the window is moving
        if (previousPosition != RectPosition) {
            if (GetGlobalMousePosition().y < 45) {
                Vector2 maximizedSize = new Vector2(screenSize.x, screenSize.y-45);
                RectPosition = new Vector2(0, 45);
                RectSize = maximizedSize;
            }

            if (GetGlobalMousePosition().x < 25) {
                Vector2 newSize = new Vector2(screenSize.x/2, screenSize.y);
                RectPosition = new Vector2(0, 45);
                RectSize = newSize;
            }

            if (GetGlobalMousePosition().x > screenSize.x-25) {
                Vector2 newSize = new Vector2(screenSize.x/2, screenSize.y);
                RectPosition = new Vector2(screenSize.x/2, 45);
                RectSize = newSize;
            }
        }
        previousPosition = RectPosition;
    }

    // make the window active :)
    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton bruh) {
            if (bruh.Pressed) {
                Raise();
            }
        }
        base._GuiInput(@event);
    }
}
