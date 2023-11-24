using Godot;
using System;

namespace Dashboard.Toolkit;

public partial class QuickSettingsUserName : Label
{
    public override void _Ready()
    {
        base._Ready();
        Text = SavingManager.CurrentUser;
    }
}
