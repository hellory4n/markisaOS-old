using Godot;
using System;

public partial class InstallelCustomWindowOpenerThing : Button {
    [Export(PropertyHint.File, "*.tscn")]
    public string WindowScene;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        PackedScene m = ResourceLoader.Load<PackedScene>(WindowScene);
        BaseWindow jjkn = (BaseWindow)m.Instance();    
        GetNode<Control>("/root/Installel/1/Windows/ThemeThing").AddChild(jjkn);
        jjkn.Visible = true;
    }
}
