using Dashboard.Interface;
using Godot;
using Kickstart.Records;
using System;
using System.Linq;

namespace QuickLaunchSetup;

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

        var m = RecordManager.Load<DashboardConfig>().AllApps;
        Package[] apps = m.OrderBy(fart => fart.DisplayName).ToArray();
        Package[] quickLaunch = RecordManager.Load<DashboardConfig>().QuickLaunch.ToArray();

        PackedScene yes = GD.Load<PackedScene>("res://Apps/QuickLaunchSetup/QuickLaunchAppThing.tscn");
        PackedScene no = GD.Load<PackedScene>("res://Apps/QuickLaunchSetup/QuickLaunchAppThingPain.tscn");
        foreach (var app in apps) {
            // .Contains() is no worky :(
            bool nbh = false;
            foreach (var item in quickLaunch) {
                if (item.DisplayName == app.DisplayName) {
                    nbh = true;
                    break;
                }
            }

            if (!nbh) {
                Control gksnj = yes.Instantiate<Control>();
                gksnj.GetNode<Label>("Label").Text = app.DisplayName;
                gksnj.GetNode<AddToQuickLaunch>("Button").App = app;
                AddChild(gksnj);
            } else {
                Control gksnj = no.Instantiate<Control>();
                gksnj.GetNode<Label>("Label").Text = app.DisplayName;
                gksnj.GetNode<RemoveFromQuickLaunch>("Button").App = app;
                AddChild(gksnj);
            }
        }
    }
}
