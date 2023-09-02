using Godot;
using System;

public class BaseWindow : WindowDialog {
    Vector2 screenSize;
    Vector2 previousPosition = new Vector2(0, 0);
    AnimationPlayer animation;
    [Export]
    public Texture Icon;
    // used for the dock button
    public bool IsClosing = false;
    [Export]
    public bool CustomTheme = false;

    public override void _Ready() {
        base._Ready();
        screenSize = ResolutionManager.Resolution;

        // makes it use the theme from the viewport container, where all of the windows are located
        if (!CustomTheme)
            Theme = null;

        PackedScene maximize = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Maximize.tscn");
        Button yes = (Button)maximize.Instance();
        AddChild(yes);

        PackedScene minimize = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Minimize.tscn");
        Button perhaps = (Button)minimize.Instance();
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
            IsClosing = true;
        }
        
        // window snapping :)
        // first check if the window is moving
        if (previousPosition != RectPosition && Resizable) {
            if (GetGlobalMousePosition().y < 40) {
                Vector2 maximizedSize = new Vector2(screenSize.x, screenSize.y-160);
                RectPosition = new Vector2(0, 85);
                RectSize = maximizedSize;
            }

            if (GetGlobalMousePosition().x < 40) {
                Vector2 newSize = new Vector2(screenSize.x/2, screenSize.y-160);
                RectPosition = new Vector2(0, 85);
                RectSize = newSize;
            }

            if (GetGlobalMousePosition().x > screenSize.x-40) {
                Vector2 newSize = new Vector2(screenSize.x/2, screenSize.y-160);
                RectPosition = new Vector2(screenSize.x/2, 85);
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
