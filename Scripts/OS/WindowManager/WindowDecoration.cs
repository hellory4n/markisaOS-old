using Godot;
using System;

public class WindowDecoration : KinematicBody2D {
    bool CanDrag = false;
    Vector2 GrabbedOffset = new Vector2();

    public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx) {
        GD.Print("1");
        if (@event is InputEventMouseButton) {
            GD.Print("2");
            CanDrag = @event.IsPressed();
            GrabbedOffset = Position - GetGlobalMousePosition();
        }
        GD.Print("3");
        base._InputEvent(viewport, @event, shapeIdx);
    }

    public override void _Process(float delta) {
        if (Input.IsMouseButtonPressed((int)ButtonList.Left) && CanDrag) {
            Position = GetGlobalMousePosition() + GrabbedOffset;
            GD.Print("4");
        }

        base._Process(delta);
    }
}