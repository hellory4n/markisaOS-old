using Godot;
using System;
using System.Collections.Generic;

public class WindowManager : Node {
    List<WindowDialog> Windows = new List<WindowDialog>();

    public void AddWindow(WindowDialog window) {
        Node2D lelsktop = GetNode<Node2D>("/root/Lelsktop");
        lelsktop.AddChild(window);
        window.Visible = true;
        window.RectPosition = new Vector2(150, 150);
    }
}
