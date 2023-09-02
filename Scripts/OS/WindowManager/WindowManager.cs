using Godot;
using System;
using System.Collections.Generic;

public class WindowManager : Node2D {
    PackedScene OpenWindow;

    public override void _Ready() {
        base._Ready();
        OpenWindow = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/OpenWindowButton.tscn");
    }

    public void AddWindow(BaseWindow window) {
        Viewport lelsktop = GetNode<Viewport>("/root/Lelsktop/Thing/Windows");
        lelsktop.AddChild(window);
        // using window.Popup_() makes it only work with 1 window, so this is a hack to bypass that
        window.Visible = true;

        // put it on the center of the screen
        Vector2 yes = ResolutionManager.Resolution;
        window.RectPosition = yes/2 - (window.RectSize/2);

        // add it to the dock
        OpenWindowButton coolDockButton = (OpenWindowButton)OpenWindow.Instance();
        coolDockButton.Init(window);
        HBoxContainer dock = GetNode<HBoxContainer>("/root/LelsktopInterface/Dock/ScrollContainer/HBoxContainer");
        dock.AddChild(coolDockButton);

        // all windows are maximized by default on mobile
        if (OS.GetName() == "Android" && window.Resizable) {
            Vector2 maximizedSize = ResolutionManager.Resolution;
            maximizedSize = new Vector2(maximizedSize.x, maximizedSize.y-160);
            window.RectPosition = new Vector2(0, 85);
            window.RectSize = maximizedSize;
        }
    }
}
