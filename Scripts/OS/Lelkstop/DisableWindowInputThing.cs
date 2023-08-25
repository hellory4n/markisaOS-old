using Godot;
using System;

public class DisableWindowInputThing : Panel {
    public override void _GuiInput(InputEvent @event) {
        if (@event is InputEventMouseButton bruh) {
            // first check if it's already disabled
            if (WindowManager.windows[0].MouseFilter == MouseFilterEnum.Stop)
                WindowManager.DisableWindowInput();
        }
        base._GuiInput(@event);
    }
}
