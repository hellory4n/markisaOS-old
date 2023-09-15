using Godot;
using System;

public class Close : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        GetParent<BaseWindow>().Close();
    }
}
