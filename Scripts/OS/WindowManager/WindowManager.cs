using Godot;
using System;
using System.Collections.Generic;

public class WindowManager : Node {
    // TODO: don't have a window switching maximum of 800
    List<Control> Windows = new List<Control>();
    int Layer = -4095;
    PackedScene windowDecoration;

    public override void _Ready() {
        base._Ready();
        windowDecoration = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/WindowDecoration.tscn");
    }

    public void AddWindow(Control window) {
        Control lelsktop = GetNode<Control>("/root/Lelsktop");

        // epic window decorations :)
        Panel bruh = (Panel)windowDecoration.Instance();
        bruh.RectMinSize = new Vector2(window.RectSize.x, 45);
        bruh.AddChild(window);

        // so we can have windows on top of windows and stuff
        Node2D thing = new Node2D {
            ZIndex = Layer
        };
        thing.AddChild(bruh);
        lelsktop.AddChild(thing);
        bruh.RectPosition = new Vector2(150, 150);
        window.RectPosition = new Vector2(0, 45);
        Layer += 1;
    }
}
