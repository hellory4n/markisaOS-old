using Godot;
using System;
using System.Linq;

public partial class ListAppsButQuickLaunch : VBoxContainer {
    public override void _Ready() {
        base._Ready();
        UpdateItems();
    }

    public void UpdateItems() {
        // clear previous list
        foreach (Node mbcicfda in GetChildren()) {
            mbcicfda.QueueFree();
        }

        InstalledApps m = SavingManager.Load<InstalledApps>(SavingManager.CurrentUser);
        Lelapp[] apps = m.All;
        apps = apps.OrderBy(fart => fart.Name).ToArray();
        Lelapp[] quickLaunch = SavingManager.Load<QuickLaunch>(SavingManager.CurrentUser).Apps;

        PackedScene yes = GD.Load<PackedScene>("res://Apps/QuickLaunchSetup/QuickLaunchAppThing.tscn");
        PackedScene no = GD.Load<PackedScene>("res://Apps/QuickLaunchSetup/QuickLaunchAppThingPain.tscn");
        foreach (var app in apps) {
            // .Contains() is no worky :(
            bool nbh = false;
            foreach (var item in quickLaunch) {
                if (item.Name == app.Name) {
                    nbh = true;
                    break;
                }
            }

            if (!nbh) {
                Control gksnj = yes.Instantiate<Control>();
                gksnj.GetNode<Label>("Label").Text = app.Name;
                gksnj.GetNode<AddToQuickLaunch>("Button").App = app;
                AddChild(gksnj);
            } else {
                Control gksnj = no.Instantiate<Control>();
                gksnj.GetNode<Label>("Label").Text = app.Name;
                gksnj.GetNode<RemoveFromQuickLaunch>("Button").App = app;
                AddChild(gksnj);
            }
        }
    }
}
