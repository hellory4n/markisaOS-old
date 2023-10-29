using Godot;
using System;

public class MinesRestart : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        // truly mind-boggling
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Mines/Mines.tscn");
        BaseWindow jjkn = m.Instance<BaseWindow>();
        wm.AddWindow(jjkn);
        jjkn.RectPosition = GetParent().GetParent<BaseWindow>().RectPosition;
        jjkn.RectSize = GetParent().GetParent<BaseWindow>().RectSize;
        GetParent().GetParent<BaseWindow>().Close();
    }
}
