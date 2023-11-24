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
        DashboardWindow jjkn = m.Instantiate<DashboardWindow>();
        wm.AddWindow(jjkn);
        jjkn.Position = GetParent().GetParent<DashboardWindow>().Position;
        jjkn.Size = GetParent().GetParent<DashboardWindow>().Size;
        GetParent().GetParent<DashboardWindow>().Close();*/
    }
}
