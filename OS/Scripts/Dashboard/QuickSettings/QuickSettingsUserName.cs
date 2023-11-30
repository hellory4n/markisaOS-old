using Godot;
using Kickstart.Records;
using System;

namespace Dashboard.Interface;

public partial class QuickSettingsUserName : Label
{
    public override void _Ready()
    {
        base._Ready();
        Text = RecordManager.CurrentUserDisplayName;
    }
}
