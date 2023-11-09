using Godot;
using System;

public partial class CloseParent : Button {
    [Export]
    int Parents = 1;

    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public void Click() {
        Node parent = GetParent();
        for (int i = 0; i < Parents-1; i++) {
            parent = parent.GetParent();
        }
        parent.QueueFree();
    }
}
