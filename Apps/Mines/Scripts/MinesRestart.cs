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
        PackedScene m = GD.Load<PackedScene>("res://Apps/Mines/Mines.tscn");
        MksWindow jjkn = m.Instantiate<MksWindow>();
        wm.AddWindow(jjkn);
        jjkn.Position = GetParent().GetParent<MksWindow>().Position;
        jjkn.Size = GetParent().GetParent<MksWindow>().Size;
        GetParent().GetParent<MksWindow>().Close();*/
    }
}
