using Godot;
using System;

/// <summary>
/// A basic window. Adds window decorations, manages opening, closing, and minimizing animations, and also manages window snapping and making windows active.
/// </summary>
public class BaseWindow : WindowDialog {
    Vector2 screenSize;
    Vector2 previousPosition = new Vector2(0, 0);
    AnimationPlayer animation;
    /// <summary>
    /// The icon used for the button on the dock.
    /// </summary>
    [Export]
    public Texture Icon;
    /// <summary>
    /// Used by the button on the dock to check if it should delete itself, as if it checked if the window was queued for deletion, there would be a little delay before it actually deleted the button.
    /// </summary>
    public bool IsClosing = false;
    /// <summary>
    /// Prevents the lelsktop from changing the theme of the window based on the settings the user chose.
    /// </summary>
    [Export]
    public bool CustomTheme = false;

    public override void _Ready() {
        base._Ready();
        screenSize = ResolutionManager.Resolution;

        // makes it use the theme from the viewport where all of the windows are located
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
