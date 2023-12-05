using Godot;
using Kickstart.Records;
using System;
using System.Linq;

namespace QuickLaunchSetup;

public partial class ListApps : VBoxContainer
{
    readonly PackedScene yes = GD.Load<PackedScene>("res://Apps/QuickLaunchSetup/QuickLaunchAppThing.tscn");
    readonly PackedScene no = GD.Load<PackedScene>("res://Apps/QuickLaunchSetup/QuickLaunchAppThingPain.tscn");

    public override void _Ready()
    {
        base._Ready();
        
        var pain = new Record<DashboardConfig>();
        var m = pain.Data.AllApps;
        Package[] apps = m.OrderBy(fart => Tr(fart.DisplayName)).ToArray();
        
        foreach (var app in apps)
        {
            if (!pain.Data.QuickLaunch.Exists(x => x.Executable == app.Executable))
            {
                Control gksnj = yes.Instantiate<Control>();
                gksnj.GetNode<Label>("Label").Text = Tr(app.DisplayName);
                gksnj.GetNode<AddApp>("Button").App = app;
                AddChild(gksnj);
            }
            else
            {
                Control gksnj = no.Instantiate<Control>();
                gksnj.GetNode<Label>("Label").Text = Tr(app.DisplayName);
                gksnj.GetNode<RemoveApp>("Button").App = app;
                AddChild(gksnj);
            }
        }
    }
}
