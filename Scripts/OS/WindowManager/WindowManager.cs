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

    public void AddWindow(Control window, float windowWidth, float windowHeight) {
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

        Label funni = GetNode<Label>($"{bruh.GetPath()}/Name");
        funni.Text = window.Name;
        funni.RectSize = new Vector2(windowWidth, 45);

        TextureButton close = GetNode<TextureButton>($"{bruh.GetPath()}/Close");
        close.RectPosition = new Vector2(windowWidth-36, 6);

        Sprite jgkf = GetNode<Sprite>($"{bruh.GetPath()}/Pain");
        jgkf.Scale *= new Vector2(windowWidth, 45);

        KinematicBody2D gaming = GetNode<KinematicBody2D>($"{bruh.GetPath()}/Resize");
        gaming.Position = new Vector2(windowWidth+40, windowHeight+40);

        bruh.AddChild(window);
    }
}
