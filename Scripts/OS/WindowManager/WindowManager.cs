using Godot;
using System;
using System.Collections.Generic;

public class WindowManager : Node {
    PackedScene openWindow;

    public override void _Ready() {
        base._Ready();
        openWindow = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/OpenWindowButton.tscn");
    }

    public void AddWindow(BaseWindow window) {
        Node2D lelsktop = GetNode<Node2D>("/root/Lelsktop");
        lelsktop.AddChild(window);
        // using window.Popup_() makes it only work with 1 window, so this is a hack to bypass that
        window.Visible = true;

        // put it on the center of the screen
        Vector2 yes = ResolutionManager.GetScreenSize();
        window.RectPosition = yes/2 - (window.RectSize/2);

        // add it to the dock
        OpenWindowButton coolDockButton = (OpenWindowButton)openWindow.Instance();
        coolDockButton.Init(window);
        HBoxContainer dock = GetNode<HBoxContainer>("/root/LelsktopInterface/Dock/ScrollContainer/HBoxContainer");
        dock.AddChild(coolDockButton);

        // all windows are maximized by default on mobile
        if (OS.GetName() == "Android") {
            Vector2 maximizedSize = ResolutionManager.GetScreenSize();
            maximizedSize = new Vector2(maximizedSize.x, maximizedSize.y-160);
            window.RectPosition = new Vector2(0, 85);
            window.RectSize = maximizedSize;
        }
    }
}
