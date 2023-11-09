using Godot;
using System;

public partial class StickyNote : Sticker {
    public override void _Ready() {
        DoTheStickerThingy = false;
        base._Ready();
    }
}
