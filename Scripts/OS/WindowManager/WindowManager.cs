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
        window.Visible = true;
        window.RectPosition = new Vector2(150, 150);

        // add it to the dock
        OpenWindowButton coolDockButton = (OpenWindowButton)openWindow.Instance();
        coolDockButton.Init(window);
        HBoxContainer dock = lelsktop.GetNode<HBoxContainer>("LelsktopInterface/Dock/ScrollContainer/HBoxContainer");
        dock.AddChild(coolDockButton);
    }
}
