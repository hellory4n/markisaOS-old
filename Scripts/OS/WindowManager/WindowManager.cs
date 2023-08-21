using Godot;
using System;
using System.Collections.Generic;

public class WindowManager : Node {
    PackedScene openWindow;
    List<BaseWindow> Windows = new List<BaseWindow>();

    public override void _Ready() {
        base._Ready();
        openWindow = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/OpenWindowButton.tscn");
    }

    public void AddWindow(BaseWindow window) {
        Node2D lelsktop = GetNode<Node2D>("/root/Lelsktop");
        lelsktop.AddChild(window);
        window.Visible = true;
        window.RectPosition = new Vector2(150, 150);
        Windows.Add(window);

        // add it to the dock
        OpenWindowButton coolDockButton = (OpenWindowButton)openWindow.Instance();
        coolDockButton.Init(window);
        HBoxContainer dock = lelsktop.GetNode<HBoxContainer>("LelsktopInterface/Dock/ScrollContainer/HBoxContainer");
        dock.AddChild(coolDockButton);
    }

    public void ShowAllWindows() {
        foreach (var window in Windows) {
            if (!window.IsQueuedForDeletion())
                window.Modulate = new Color(1, 1, 1, 0);
        }
    }

    public void HideAllWindows() {
        foreach (var window in Windows) {
            if (!window.IsQueuedForDeletion())
                window.Modulate = new Color(1, 1, 1, 0);
        }
    }

    public void CloseWindow(BaseWindow window) {
        Windows.Remove(window);
    }
}
