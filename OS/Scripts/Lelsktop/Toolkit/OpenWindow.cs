using Godot;
using System;

namespace Lelsktop.Toolkit;

[GlobalClass]
public partial class OpenWindow : Button
{
    [Export(PropertyHint.File, "*.tscn")]
    public string WindowScene;

    public override void _Pressed() {
        base._Pressed();
        /*WindowManager wm = GetNode<WindowManager>("/root/WindowManager");
        PackedScene m = ResourceLoader.Load<PackedScene>(WindowScene);
        Lelwindow jjkn = (Lelwindow)m.Instantiate();    
        wm.AddWindow(jjkn);*/
    }
}
