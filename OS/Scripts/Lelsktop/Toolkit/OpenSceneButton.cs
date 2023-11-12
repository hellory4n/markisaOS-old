using Godot;
using System;

namespace Lelsktop.Toolkit;

[GlobalClass]
public partial class OpenSceneButton : Button
{
    [Export(PropertyHint.File, "*.tscn")]
    public string Scene;

    public override void _Pressed()
    {
        base._Pressed();
        PackedScene m = GD.Load<PackedScene>(Scene);
        Node jjkn = m.Instantiate();
        GetTree().Root.AddChild(jjkn);
    }
}
