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
    public Button StupidThingForInactiveWindows = new Button {
        AnchorRight = 1,
        AnchorBottom = 1,
    };
    /// <summary>
    /// A panel used to display the texture of the window.
    /// </summary>
    public Panel TitleTexture;
    /// <summary>
    /// The maximize button of the window.
    /// </summary>
    public Button Maximize;
    /// <summary>
    /// The minimize button of the window.
    /// </summary>
    public Button Minimize;

    public override void _Ready() {
        base._Ready();
        screenSize = ResolutionManager.Resolution;

        // makes it use the theme from the viewport where all of the windows are located
        if (!CustomTheme)
            Theme = null;

        PackedScene maximize = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Maximize.tscn");
        Maximize = (Button)maximize.Instance();
        AddChild(Maximize);

        PackedScene minimize = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Minimize.tscn");
        Minimize = (Button)minimize.Instance();
        AddChild(Minimize);

        PackedScene title = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/TitleTexture.tscn");
        TitleTexture = (Panel)title.Instance();
        AddChild(TitleTexture);
        MoveChild(TitleTexture, 0);
        TitleTexture.GetNode<Label>("Label").Text = WindowTitle;

        // epic animation for opening the window, very important indeed
        animation = GetNode<AnimationPlayer>("AnimationPlayer");
        animation.Play("Open");

        // pain
        AddChild(StupidThingForInactiveWindows);
        StupidThingForInactiveWindows.AddStyleboxOverride("normal", new StyleBoxEmpty());
        StupidThingForInactiveWindows.AddStyleboxOverride("pressed", new StyleBoxEmpty());
        StupidThingForInactiveWindows.AddStyleboxOverride("hover", new StyleBoxEmpty());
        StupidThingForInactiveWindows.AddStyleboxOverride("focus", new StyleBoxEmpty());
        StupidThingForInactiveWindows.AddStyleboxOverride("disabled", new StyleBoxEmpty());
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
            Raise();

            if (GetGlobalMousePosition().y < 60) {
                Vector2 maximizedSize = new Vector2(screenSize.x-75, screenSize.y-85);
                RectPosition = new Vector2(0, 85);
                RectSize = maximizedSize;
            }

            if (GetGlobalMousePosition().x < 40) {
                Vector2 newSize = new Vector2((screenSize.x-75)/2, screenSize.y-85);
                RectPosition = new Vector2(0, 85);
                RectSize = newSize;
            }

            // we check the viewport thing so the window doesn't get snapped just because the mouse was on the dock
            if (GetGlobalMousePosition().x > screenSize.x-115 && !GetViewport().GuiDisableInput) {
                Vector2 newSize = new Vector2((screenSize.x-75)/2, screenSize.y-85);
                RectPosition = new Vector2((screenSize.x-75)/2, 85);
                RectSize = newSize;
            }
        }

        previousPosition = RectPosition;

        // so true
        Control jkbmjdg = GetFocusOwner();
        if (jkbmjdg != null) {
            if (jkbmjdg == StupidThingForInactiveWindows) {
                Raise();
            }
        }

        // checks if the window is active
        if (GetIndex() != GetParent().GetChildCount()-1) {
            StupidThingForInactiveWindows.Visible = true;
        } else {
            StupidThingForInactiveWindows.Visible = false;
        }
        StupidThingForInactiveWindows.Raise();
    }

    // make the window active :)
    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton bruh) {
            if (bruh.Pressed) {
                if (GetFocusOwner() != null) {
                    Raise();
                }
            }
        }
        base._GuiInput(@event);
    }

    /// <summary>
    /// Closes the window.
    /// </summary>
    public void Close() {
        // in the _Process function it will see that this variable has changed and actually run the close script
        Visible = false;
    }
}
