using Godot;
using System;

public class OpenOnLelsktop : Button {
    [Export(PropertyHint.File, "*.tscn")]
    public string WindowScene;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        PackedScene m = ResourceLoader.Load<PackedScene>(WindowScene);
        Node jjkn = m.Instance();    
        GetNode<Node2D>("/root/Lelsktop").AddChild(jjkn);
    }
}
