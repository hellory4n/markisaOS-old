using Godot;
using System;

namespace Lelsktop.Wm;

[GlobalClass]
public partial class Close : Button
{
    [Export]
    Lelwindow Window;

    public override void _Pressed()
    {
        base._Pressed();
        Window.EmitSignal(Lelwindow.SignalName.CloseRequested);
    }
}
