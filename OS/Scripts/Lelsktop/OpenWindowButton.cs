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
        HintTooltip = epicWindow.WindowTitle;

        // if we just check if it's queued for deletion it's gonna have a bit of a delay due to the closing animation
        if (epicWindow.IsClosing)
            QueueFree();
        else
            Icon = epicWindow.Icon;
    }

    public void Click() {
        Color invisible = new Color(1, 1, 1, 0);

        // minimize the window if it's active :)
        // we wouldn't minimize a window in another workspace tho
        if (epicWindow.IsActive() && epicWindow.Modulate != invisible &&
        WindowManager.CurrentWorkspace == epicWindow.GetViewport()) {
            animation.Play("Minimize");
        // already minimized
        // we wouldn't restore a window in another workspace tho
        } else if (epicWindow.Modulate == invisible &&
        WindowManager.CurrentWorkspace == epicWindow.GetViewport()) {
            animation.Play("Restore");
            epicWindow.Raise();
        } else {
            epicWindow.Raise();
        }

        // switch to a different workspace if necessary
        // pain
        if (!epicWindow.GetViewport().GetParent<ViewportContainer>().Visible) {
            switch (epicWindow.GetViewport().GetParent().Name) {
                case "1":
                    GetNode<ViewportContainer>("/root/Lelsktop/1").Visible = true;
                    GetNode<ViewportContainer>("/root/Lelsktop/2").Visible = false;
                    GetNode<ViewportContainer>("/root/Lelsktop/3").Visible = false;
                    GetNode<ViewportContainer>("/root/Lelsktop/4").Visible = false;
                    WindowManager.CurrentWorkspace = GetNode<Viewport>("/root/Lelsktop/1/Windows");
                    break;
                case "2":
                    GetNode<ViewportContainer>("/root/Lelsktop/1").Visible = false;
                    GetNode<ViewportContainer>("/root/Lelsktop/2").Visible = true;
                    GetNode<ViewportContainer>("/root/Lelsktop/3").Visible = false;
                    GetNode<ViewportContainer>("/root/Lelsktop/4").Visible = false;
                    WindowManager.CurrentWorkspace = GetNode<Viewport>("/root/Lelsktop/2/Windows");
                    break;
                case "3":
                    GetNode<ViewportContainer>("/root/Lelsktop/1").Visible = false;
                    GetNode<ViewportContainer>("/root/Lelsktop/2").Visible = false;
                    GetNode<ViewportContainer>("/root/Lelsktop/3").Visible = true;
                    GetNode<ViewportContainer>("/root/Lelsktop/4").Visible = false;
                    WindowManager.CurrentWorkspace = GetNode<Viewport>("/root/Lelsktop/3/Windows");
                    break;
                case "4":
                    GetNode<ViewportContainer>("/root/Lelsktop/1").Visible = false;
                    GetNode<ViewportContainer>("/root/Lelsktop/2").Visible = false;
                    GetNode<ViewportContainer>("/root/Lelsktop/3").Visible = false;
                    GetNode<ViewportContainer>("/root/Lelsktop/4").Visible = true;
                    WindowManager.CurrentWorkspace = GetNode<Viewport>("/root/Lelsktop/4/Windows");
                    break;
            }
        }
    }
}
