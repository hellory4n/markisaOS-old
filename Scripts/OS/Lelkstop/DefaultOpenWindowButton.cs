using Godot;
using System;

public class DefaultOpenWindowButton : Button {
    [Export(PropertyHint.File, "*.tscn")]
    public string WindowScene;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>(WindowScene);
        BaseWindow jjkn = (BaseWindow)m.Instance();    
        wm.AddWindow(jjkn);
    }
}
