using Godot;
using System;

public class EpicCoolAmazingBeautifulGorgeousFantasticMajesticButton : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/WindowManagerTest/WindowManagerTest.tscn");
        WindowDialog jjkn = (WindowDialog)m.Instance();    
        wm.AddWindow(jjkn);
    }
}
