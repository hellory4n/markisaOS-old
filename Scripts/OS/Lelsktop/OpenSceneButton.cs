using Godot;
using System;

public class OpenSceneButton : Button {
    [Export(PropertyHint.File, "*.tscn")]
    public string Scene;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        PackedScene m = ResourceLoader.Load<PackedScene>(Scene);
        Node jjkn = m.Instance();
        GetTree().Root.AddChild(jjkn);
    }
}
