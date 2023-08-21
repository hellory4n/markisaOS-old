using Godot;
using System;

public class ApplicationMenuOpen : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");

        if (GetNodeOrNull("/root/Lelsktop/ApplicationMenu") == null) {
            PackedScene m = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/ApplicationMenu.tscn");
            Node jjkn = m.Instance();
            GetNode<Node2D>("/root/Lelsktop").AddChild(jjkn);
            jjkn.GetNode<AnimationPlayer>("AnimationPlayer").Play("Open");
            wm.HideAllWindows();
        } else {
            GetNode<AnimationPlayer>("/root/Lelsktop/ApplicationMenu/AnimationPlayer").Play("Close");
            wm.ShowAllWindows();
        }
    }
}
