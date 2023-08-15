using Godot;
using System;
using System.Collections.Generic;

public class WindowManager : Node {
    // TODO: don't have a window switching maximum of 800
    List<WindowDialog> Windows = new List<WindowDialog>();

    public void AddWindow(WindowDialog window) {
        Node2D lelsktop = GetNode<Node2D>("/root/Lelsktop");
        lelsktop.AddChild(window);
        window.Visible = true;
        window.RectPosition = new Vector2(690, 420);
    }
}
