using Godot;
using System;

public class MobileSetup : Control {
    public int FailedAttempts = 0;

    public override void _Ready() {
        base._Ready();
        RectSize = ResolutionManager.GetScreenSize();
        GetNode<Button>("Button").Connect("pressed", this, nameof(Thing));
    }

    public override void _GuiInput(InputEvent @event) {
        if (Input.IsActionJustReleased("click_thingy")) {
            FailedAttempts++;
        }

        GD.Print($"user has failed {FailedAttempts} times");
        base._GuiInput(@event);
    }

    public void Thing() {
        if (FailedAttempts > 0) {
            GD.Print("lol user is noob xdxxddxddxdd");
        } else {
            GD.Print("show the bootscreen or something");
        }
    }
}
