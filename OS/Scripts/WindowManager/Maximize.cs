using Godot;
using System;

public partial class Maximize : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", new Callable(this, nameof(Click)));
    }

    public override void _Process(float delta) {
        base._Process(delta);
        Window window = (Window)GetParent();
        if (!window.Resizable) {
            GetParent().GetNode<Button>("Minimize").Position = Position;
            QueueFree();
        }
    }

    public void Click() {
        Window window = (Window)GetParent();
        Vector2 maximizedSize = ResolutionManager.Resolution;
        maximizedSize = new Vector2(maximizedSize.x-75, maximizedSize.y-85);

        // check if the window is maximized
        if (window.Position != new Vector2(0, 85) && window.Size != maximizedSize) {
            window.Position = new Vector2(0, 85);
            window.Size = maximizedSize;
        } else {
            window.Size = window.CustomMinimumSize;
            window.Position = new Vector2(150, 150);
        }
    }
}
