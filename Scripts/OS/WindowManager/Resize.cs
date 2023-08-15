using Godot;
using System;

public class Resize : KinematicBody2D {
    bool CanDrag = false;
    Vector2 GrabbedOffset = new Vector2();
    readonly Texture normal = ResourceLoader.Load<Texture>("res://Assets/Themes/Leltheme/Window/Resize.png");
    readonly Texture hover = ResourceLoader.Load<Texture>("res://Assets/Themes/Leltheme/Window/ResizeHover.png");
    readonly Texture pressed = ResourceLoader.Load<Texture>("res://Assets/Themes/Leltheme/Window/ResizePressed.png");

    public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx) {
        if (@event is InputEventMouseButton) {
            GetNode<Sprite>("./Thing").Texture = hover;
            CanDrag = @event.IsPressed();
            Control coolWindow = GetNode<Control>("../Content");
            GrabbedOffset = coolWindow.RectSize - GetGlobalMousePosition();
        } else {
            GetNode<Sprite>("./Thing").Texture = normal;
        }
        base._InputEvent(viewport, @event, shapeIdx);
    }

    public override void _Process(float delta) {
        if (Input.IsMouseButtonPressed((int)ButtonList.Left) && CanDrag) {
            GetNode<Sprite>("./Thing").Texture = pressed;
            Vector2 newSize = GetGlobalMousePosition() + GrabbedOffset;

            // resize the window and stuff :)
            GetNode<Control>("../Content").RectMinSize = newSize;
            CollisionShape2D m = GetNode<CollisionShape2D>($"../EpicCollision");
            m.Shape = new RectangleShape2D {
                Extents = new Vector2(newSize.x-18, 45)
            };
            m.Position = new Vector2(newSize.x, 45);
            GetNode<Label>("../Name").RectSize = new Vector2(newSize.x-18, 45);
            GetNode<TextureButton>("../Close").RectPosition = new Vector2(newSize.x-36, 6);
            GetNode<Sprite>("../Pain").Scale = new Vector2(0.008f, 0.008f) * new Vector2(newSize.x-18, 45);
            Position = new Vector2(newSize.x+40, newSize.y+40);
        }
        base._Process(delta);
    }
}
