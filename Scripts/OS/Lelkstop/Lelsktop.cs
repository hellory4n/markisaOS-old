using Godot;
using System;

public class Lelsktop : Node2D {
    public override void _Ready() {
        base._Ready();
        Vector2 bruh = ResolutionManager.GetScreenSize();
        GD.Print($"Screen resolution is {bruh.x}, {bruh.y}");
        
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/WindowManagerTest/WindowManagerTest.tscn");
        // make 5 windows for testing :)
        for (int i = 0; i < 1; i++) {
            WindowDialog jjkn = (WindowDialog)m.Instance();    
            wm.AddWindow(jjkn);
        }
    }
}
