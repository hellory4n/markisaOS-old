using Godot;
using System;
using Dashboard.Toolkit;
using Kickstart.Records;
using System.Collections.Generic;
using System.Linq;

namespace Dashboard.Interface;

public partial class ListApps : VBoxContainer
{
    [Export(PropertyHint.Enum, "All,Accessories,Development,Games,Graphics,Internet,Multimedia,Office,System,Utilities")]
    string Category = "All";

    public override void _Ready()
    {
        base._Ready();
        UpdateList();
    }

    public void UpdateList()
    {
        // clear previous list
        foreach (Node mbcicfda in GetChildren())
            mbcicfda.QueueFree();

        List<Package> m = new Record<DashboardConfig>().Data.AllApps.Distinct().ToList();

        // i know
        List<Package> apps = Category switch
        {
            "Accessories" => m.Where(item => item.Categories.Contains(Categories.Accessories)).ToList(),
            "Development" => m.Where(item => item.Categories.Contains(Categories.Development)).ToList(),
            "Games" => m.Where(item => item.Categories.Contains(Categories.Games)).ToList(),
            "Graphics" => m.Where(item => item.Categories.Contains(Categories.Graphics)).ToList(),
            "Internet" => m.Where(item => item.Categories.Contains(Categories.Internet)).ToList(),
            "Multimedia" => m.Where(item => item.Categories.Contains(Categories.Office)).ToList(),
            "Office" => m.Where(item => item.Categories.Contains(Categories.Multimedia)).ToList(),
            "System" => m.Where(item => item.Categories.Contains(Categories.System)).ToList(),
            "Utilities" => m.Where(item => item.Categories.Contains(Categories.Utilities)).ToList(),
            _ => m,
        };
        
        if (apps.Count == 0) {
            Label epicbruhmoment = new()
            {
                Text = "No apps found.",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                SizeFlagsHorizontal = SizeFlags.ExpandFill,
                SizeFlagsVertical = SizeFlags.ExpandFill
            };
            AddChild(epicbruhmoment);
        }
        else
        {
            PackedScene yes = GD.Load<PackedScene>("res://OS/Dashboard/AppMenuApp.tscn");
            foreach (var app in apps)
            {
                OpenWindow amazingApp = yes.Instantiate<OpenWindow>();
                amazingApp.Text = app.DisplayName;
                amazingApp.Icon = GD.Load<Texture2D>(app.Icon);
                amazingApp.WindowScene = app.Executable;
                AddChild(amazingApp);
            }
        }
    }
}
