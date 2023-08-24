using Godot;
using System;

public class QuickSettingsUserName : Label {
    public override void _Ready() {
        base._Ready();
        Text = SavingManager.CurrentUser;
    }
}
