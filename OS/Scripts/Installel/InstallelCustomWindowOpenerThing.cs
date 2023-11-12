using Godot;
using System;
using Lelsktop.WindowManager;

namespace Lelcore.Installel;

public partial class InstallelCustomWindowOpenerThing : Button
{
    [Export(PropertyHint.File, "*.tscn")]
    public string WindowScene;

    public override void _Pressed()
    {
        base._Pressed();
        PackedScene m = GD.Load<PackedScene>(WindowScene);
        Lelwindow jjkn = (Lelwindow)m.Instantiate();    
        GetNode<Control>("/root/Installel/1/Windows/ThemeThing").AddChild(jjkn);
        jjkn.Visible = true;
    }
}
