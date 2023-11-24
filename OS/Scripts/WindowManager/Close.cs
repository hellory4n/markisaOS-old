using Godot;
using System;

namespace Dashboard.Wm;

[GlobalClass]
public partial class Close : Button
{
    [Export]
    DashboardWindow Window;

    public override void _Pressed()
    {
        base._Pressed();
        Window.EmitSignal(DashboardWindow.SignalName.CloseRequested);
    }
}
