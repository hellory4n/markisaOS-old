using Godot;
using System;

public class Lelsktop : Node2D {
    public override void _Ready() {
        base._Ready();
        Vector2 bruh = ResolutionManager.GetScreenSize();
        GD.Print($"Screen resolution is {bruh.x}, {bruh.y}");
        
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/WindowManagerTest/WindowManagerTest.tscn");
        WindowDialog jjkn = (WindowDialog)m.Instance();    
        wm.AddWindow(jjkn);

        // cool dock texture
        GetNode<Sprite>("DockTexture").Scale *= new Vector2(bruh.x, 100);
        GetNode<Sprite>("DockTexture").Position = new Vector2(0, bruh.y-100);
    }
}
