using Godot;
using System;

public partial class QuickSettingsUserName : Label {
    public override void _Ready() {
        base._Ready();
        Text = SavingManager.CurrentUser;
    }
}
