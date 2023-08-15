using Godot;
using System;

public class WindowDecoration : KinematicBody2D {
    bool CanDrag = false;
    Vector2 GrabbedOffset = new Vector2();

    public override void _InputEvent(Godot.Object viewport, InputEvent @event, int shapeIdx) {
        if (@event is InputEventMouseButton) {
            CanDrag = @event.IsPressed();
            GrabbedOffset = Position - GetGlobalMousePosition();
        }
        base._InputEvent(viewport, @event, shapeIdx);
    }

    public override void _Process(float delta) {
        if (Input.IsMouseButtonPressed((int)ButtonList.Left) && CanDrag) {
            Position = GetGlobalMousePosition() + GrabbedOffset;
            // WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
            Raise();
            // wm.Layer++;
        }
        base._Process(delta);
    }
}