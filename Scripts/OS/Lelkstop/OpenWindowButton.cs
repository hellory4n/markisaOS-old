using Godot;
using System;

public class OpenWindowButton : Button {
    BaseWindow epicWindow;

    // called when the window manager opens a window
    public void Init(BaseWindow window) {
        epicWindow = window;
    }

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public override void _Process(float delta) {
        base._Process(delta);
        if (epicWindow.IsQueuedForDeletion())
            QueueFree();
        else {
            Text = epicWindow.WindowTitle;
            Icon = epicWindow.Icon;
        }
    }

    public void Click() {
        epicWindow.Raise();
    }
}
