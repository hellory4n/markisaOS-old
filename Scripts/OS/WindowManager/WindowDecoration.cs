using Godot;
using System;

public class WindowDecoration : Control {
    Vector2? dragPoint;

    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton) {
            InputEventMouseButton yes = (InputEventMouseButton)@event;
            if (yes.ButtonIndex == (int)ButtonList.Left) {
                if (yes.Pressed) {
                    dragPoint = GetGlobalMousePosition() - RectPosition;
                } else {
                    dragPoint = null;
                }
            }
        }

        if (@event is InputEventMouseButton && dragPoint != null)
            RectPosition = GetGlobalMousePosition() - (Vector2)dragPoint;
        base._GuiInput(@event);
    }
}
