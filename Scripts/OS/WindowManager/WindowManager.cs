using Godot;
using System;
using System.Collections.Generic;

public class WindowManager : Node {
    // TODO: don't have a window switching maximum of 800
    List<WindowDialog> Windows = new List<WindowDialog>();
    int Layer = -4095;

    public void AddWindow(WindowDialog window) {
        Control lelsktop = GetNode<Control>("/root/Lelsktop");
        // so we can have windows on top of windows and stuff
        Node2D thing = new Node2D {
            ZIndex = Layer
        };
        thing.AddChild(window);
        lelsktop.AddChild(thing);

        // ye
        window.Popup_();
        window.PopupCentered();
    }
}
