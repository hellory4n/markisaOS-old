using Godot;
using System;

public partial class OpenSceneButton : Button {
    [Export(PropertyHint.File, "*.tscn")]
    public string Scene;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        PackedScene m = ResourceLoader.Load<PackedScene>(Scene);
        Node jjkn = m.Instance();
        GetTree().Root.AddChild(jjkn);
    }
}
