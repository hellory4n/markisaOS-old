using Godot;
using System;

public partial class PauseMusic : Button {
    /*public override void _Toggled(bool toggledOn) {
        base._Toggled(toggledOn);
        GetNode<MusicPlayer>("../Audio").SetPaused(toggledOn);
    }*/

    public override void _Process(double delta) {
        base._Process(delta);
        // BLOODY HELL
        /*GetNode<MusicPlayer>("../Audio").CanPlay = GetParent().GetParent().GetParent().GetParent().GetParent<MksWindow>().IsActive();

        if (GetParent().GetParent().GetParent().GetParent().GetParent<MksWindow>().IsClosing) {
            GetNode<MusicManager>("/root/MusicManager").DeletePlayer(GetNode<MusicPlayer>("../Audio").PlayerIndex);
        }*/
    }
}
