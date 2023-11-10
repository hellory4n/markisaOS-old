using Godot;
using System;

public partial class MinesRestart : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        // truly mind-boggling
        /*WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>("res://Apps/Mines/Mines.tscn");
        Lelwindow jjkn = m.Instantiate<Lelwindow>();
        wm.AddWindow(jjkn);
        jjkn.Position = GetParent().GetParent<Lelwindow>().Position;
        jjkn.Size = GetParent().GetParent<Lelwindow>().Size;
        GetParent().GetParent<Lelwindow>().Close();*/
    }
}
