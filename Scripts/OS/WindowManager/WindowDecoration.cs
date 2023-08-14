using Godot;
using System;

public class WindowDecoration : Panel {
    Vector2 previousMousePosition = new Vector2();
    bool isDragging = false;

    public override void _Process(float delta) {
        base._Process(delta);
        if (isDragging) {
            Vector2 mouseDelta = previousMousePosition - GetLocalMousePosition();
            RectPosition -= mouseDelta;
        }
    }

    public override void _GuiInput(InputEvent @event) {
        base._GuiInput(@event);
        if (@event.IsActionPressed("ui_select")) {
            isDragging = true;
            previousMousePosition = GetLocalMousePosition();
        }
        if (@event.IsActionReleased("ui_select")) {
            isDragging = false;
        }
    }
}
