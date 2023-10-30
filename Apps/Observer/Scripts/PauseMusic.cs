using Godot;
using System;

public class PauseMusic : Button {
    public override void _Ready() {
        base._Ready();
        Connect("pressed", this, nameof(Click));
    }

    public void Click() {
        GetNode<MusicPlayer>("../Audio").SetPaused(Pressed);
    }

    public override void _Process(float delta) {
        base._Process(delta);
        // BLOODY HELL
        GetNode<MusicPlayer>("../Audio").CanPlay = GetParent().GetParent().GetParent().GetParent().GetParent<BaseWindow>().IsActive();

        if (GetParent().GetParent().GetParent().GetParent().GetParent<BaseWindow>().IsClosing) {
            GetNode<MusicManager>("/root/MusicManager").DeletePlayer(GetNode<MusicPlayer>("../Audio").PlayerIndex);
        }
    }
}
