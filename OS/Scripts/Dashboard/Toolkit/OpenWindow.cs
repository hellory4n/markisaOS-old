using Godot;
using Dashboard.Wm;
using System;

namespace Dashboard.Toolkit;

[GlobalClass]
public partial class OpenWindow : Button
{
    [Export(PropertyHint.File, "*.tscn")]
    public string WindowScene;

    public override void _Pressed()
    {
        base._Pressed();
        WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = GD.Load<PackedScene>(WindowScene);
        MksWindow jjkn = (MksWindow)m.Instantiate();    
        wm.AddWindow(jjkn);
    }
}
