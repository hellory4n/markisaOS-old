using Godot;
using System;

namespace Lelsktop.Toolkit;

public partial class QuickSettingsUserName : Label
{
    public override void _Ready()
    {
        base._Ready();
        Text = SavingManager.CurrentUser;
    }
}
