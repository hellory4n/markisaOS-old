using Godot;
using Kickstart.Records;
using System;

namespace Dashboard.Interface;

public partial class QuickSettingsUserIcon : Sprite2D
{
    public override void _Ready()
    {
        base._Ready();
        string photo = RecordManager.Load<MarkisaUser>().Photo;
        Texture = GD.Load<Texture2D>(photo);
    }
}
