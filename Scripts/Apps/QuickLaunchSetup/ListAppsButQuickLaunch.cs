using Godot;
using System;
using System.Linq;

public class ListAppsButQuickLaunch : VBoxContainer {
    public override void _Ready() {
        base._Ready();
        InstalledApps m = SavingManager.Load<InstalledApps>(SavingManager.CurrentUser);
        Lelapp[] apps = m.All;
        Lelapp[] quickLaunch = SavingManager.Load<QuickLaunch>(SavingManager.CurrentUser).Apps;

        PackedScene yes = ResourceLoader.Load<PackedScene>("res://Apps/QuickLaunchSetup/QuickLaunchAppThing.tscn");
        PackedScene no = ResourceLoader.Load<PackedScene>("res://Apps/QuickLaunchSetup/QuickLaunchAppThingPain.tscn");
        foreach (var app in apps){
            if (!quickLaunch.Contains(app)) {
                Control gksnj = yes.Instance<Control>();
                gksnj.GetNode<Label>("Label").Text = app.Name;
                AddChild(gksnj);
            } else {
                Control gksnj = no.Instance<Control>();
                gksnj.GetNode<Label>("Label").Text = app.Name;
                AddChild(gksnj);
            }
        }
    }
}
