using Godot;
using System;

public class BaseWindow : WindowDialog {
    Vector2 screenSize;
    Vector2 previousPosition = new Vector2(0, 0);
    ColorRect blur;

    public override void _Ready() {
        base._Ready();
        screenSize = ResolutionManager.GetScreenSize();

        PackedScene maximize = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Maximize.tscn");
        TextureButton yes = (TextureButton)maximize.Instance();
        AddChild(yes);

        PackedScene minimize = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Minimize.tscn");
        TextureButton perhaps = (TextureButton)minimize.Instance();
        AddChild(perhaps);

        // if we add the blur node as a child it's gonna blur the window's background and window title, not cool!
        Random random = new Random();
        PackedScene jbodlkmgodkg = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/Blur.tscn");
        blur = (ColorRect)jbodlkmgodkg.Instance();
        blur.Name = $"BlurWindow-{Name}-{random.Next(0, int.MaxValue)}";
        GetParent().AddChild(blur);
    }

    public override void _Process(float delta) {
        base._Process(delta);

        // windowdialog's close button just makes it invisible
        if (!Visible)
            QueueFree();
        
        // window snapping :)
        // first check if the window is moving
        if (previousPosition != RectPosition) {
            if (GetGlobalMousePosition().y < 55) {
                Vector2 maximizedSize = new Vector2(screenSize.x, screenSize.y-55);
                RectPosition = new Vector2(0, 55);
                RectSize = maximizedSize;
            }

            if (GetGlobalMousePosition().x < 25) {
                Vector2 newSize = new Vector2(screenSize.x/2, screenSize.y);
                RectPosition = new Vector2(0, 55);
                RectSize = newSize;
            }

            if (GetGlobalMousePosition().x > screenSize.x-25) {
                Vector2 newSize = new Vector2(screenSize.x/2, screenSize.y);
                RectPosition = new Vector2(screenSize.x/2, 55);
                RectSize = newSize;
            }
        }
        previousPosition = RectPosition;

        // cool blur effect :)
        blur.RectPosition = RectPosition - new Vector2(5, 55);
        blur.RectSize = RectSize + new Vector2(10, 60);
    }

    // make the window active :)
    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton bruh) {
            if (bruh.Pressed) {
                blur.Raise();
                Raise();
            }
        }
        base._GuiInput(@event);
    }
}
