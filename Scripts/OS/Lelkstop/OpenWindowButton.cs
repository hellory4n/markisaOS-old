using Godot;
using System;

public class OpenWindowButton : Button {
    BaseWindow epicWindow;
    AnimationPlayer animation;

    // called when the window manager opens a window
    public void Init(BaseWindow window) {
        epicWindow = window;
    }

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
        animation = epicWindow.GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _Process(float delta) {
        base._Process(delta);
        // if we just check if it's queued for deletion it's gonna have a bit of a delay due to the closing animation
        if (epicWindow.IsClosing)
            QueueFree();
        else
            Icon = epicWindow.Icon;
    }

    public void Click() {
        Color invisible = new Color(1, 1, 1, 0);

        // minimize the window if it's active :)
        if (epicWindow.GetIndex() == epicWindow.GetParent().GetChildCount()-1 && epicWindow.Modulate != invisible)
           animation.Play("Minimize");
        // already minimized
        else if (epicWindow.Modulate == invisible) {
            animation.Play("Restore");
            epicWindow.Raise();
        } else
            epicWindow.Raise();
    }
}
