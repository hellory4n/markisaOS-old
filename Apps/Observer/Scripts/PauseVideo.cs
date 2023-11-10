using Godot;
using System;

public partial class PauseVideo : Button {
    public override void _Toggled(bool toggledOn) {
        base._Toggled(toggledOn);
        GetNode<VideoStreamPlayer>("../M/Video").Paused = toggledOn;
    }
}
