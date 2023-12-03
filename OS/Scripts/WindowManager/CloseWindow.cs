using Godot;
using System;

namespace Dashboard.Wm;

[GlobalClass]
public partial class CloseWindow : Button
{
    [Export]
    MksWindow Window;

    public override void _Pressed()
    {
        base._Pressed();
        Window.EmitSignal(MksWindow.SignalName.CloseRequested);
    }
}
