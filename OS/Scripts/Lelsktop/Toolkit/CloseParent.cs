using Godot;
using System;

namespace Lelsktop.Toolkit;

[GlobalClass]
public partial class CloseParent : Button
{
    [Export]
    Node Parent;

    public override void _Pressed()
    {
        base._Pressed();
        Parent.QueueFree();
    }
}
