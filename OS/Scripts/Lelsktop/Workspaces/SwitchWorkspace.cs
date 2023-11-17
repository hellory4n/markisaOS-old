using Godot;
using System;
using Lelsktop.Wm;
using Lelcore.Drivers;

namespace Lelsktop.Interface;

public partial class SwitchWorkspace : Button
{
    [Export(PropertyHint.Range, "1,4")]
    int Workspace = 1;

    public override void _Pressed()
    {
        base._Pressed();
        GetNode<WindowManager>("/root/WindowManager").SwitchWorkspace(Workspace);
    }
}
