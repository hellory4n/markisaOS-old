using Godot;
using System;

public class Lelsktop : Control {
    public override void _Ready() {
        base._Ready();
        Vector2 bruh = ResolutionManager.GetScreenSize();
        GD.Print($"Screen resolution is {bruh.x}, {bruh.y}");

        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/WindowManagerTest/WindowManagerTest.tscn");
        WindowDialog jjkn = (WindowDialog)m.Instance();
        AddChild(jjkn);
        jjkn.Popup_();
        jjkn.PopupCentered(jjkn.RectSize);
    }
}
