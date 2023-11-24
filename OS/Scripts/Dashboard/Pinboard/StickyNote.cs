using Godot;
using System;

namespace Dashboard.Pinboard;

public partial class StickyNote : Sticker
{
    public override void _Ready()
    {
        DoTheStickerThingy = false;
        base._Ready();
    }
}
