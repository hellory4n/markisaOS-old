using Godot;
using System;
using System.Collections.Generic;
using System.Drawing;

public class WindowManager : Node {
    // TODO: don't have a window switching maximum of 8000
    List<Control> Windows = new List<Control>();
    int Layer = -4095;
    PackedScene windowDecoration;

    public override void _Ready() {
        base._Ready();
        windowDecoration = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/WindowDecoration.tscn");
    }

    public void AddWindow(Control window, float windowWidth) {
        Node2D lelsktop = GetNode<Node2D>("/root/Lelsktop");

        // epic window decorations :)
        KinematicBody2D bruh = (KinematicBody2D)windowDecoration.Instance();
        lelsktop.AddChild(bruh);
        bruh.Position = new Vector2(150, 150);
        window.RectPosition = new Vector2(0, 45);

        // make the window decorations have the width of the window and stuff
        CollisionShape2D m = GetNode<CollisionShape2D>($"{bruh.GetPath()}/EpicCollision");
        m.Shape = new RectangleShape2D {
            Extents = new Vector2(windowWidth, 45)
        };
        m.Position = new Vector2(windowWidth, 45);

        Sprite jgkf = GetNode<Sprite>($"{bruh.GetPath()}/Pain");
        jgkf.Scale *= new Vector2(690, 45);

        bruh.AddChild(window);
    }
}
