using Godot;
using System;

public partial class Close : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        GetParent<Lelwindow>().Close();
    }
}
