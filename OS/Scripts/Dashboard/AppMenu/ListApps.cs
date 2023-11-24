using Godot;
using System;
using Dashboard.Toolkit;

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
        {
            mbcicfda.QueueFree();
        }

        // get the list stuff
        Lelapp[] apps;
        InstalledApps m = SavingManager.Load<InstalledApps>(SavingManager.CurrentUser);
        switch (Category) {
            case "All":
                apps = m.All;
                break;
            case "Accessories":
                apps = m.Accessories;
                break;
            case "Development":
                apps = m.Development;
                break;
            case "Games":
                apps = m.Games;
                break;
            case "Graphics":
                apps = m.Graphics;
                break;
            case "Internet":
                apps = m.Internet;
                break;
            case "Multimedia":
                apps = m.Multimedia;
                break;
            case "Office":
                apps = m.Office;
                break;
            case "System":
                apps = m.System;
                break;
            case "Utilities":
                apps = m.Utilities;
                break;
            default:
                apps = m.All;
                break;
        }

        if (apps.Length == 0) {
            Label epicbruhmoment = new()
            {
                Text = "No apps found."
            };
            AddChild(epicbruhmoment);
        }
        else
        {
            PackedScene yes = GD.Load<PackedScene>("res://OS/Dashboard/AppMenuApp.tscn");
            foreach (var app in apps)
            {
                OpenWindow amazingApp = yes.Instantiate<OpenWindow>();
                amazingApp.Text = app.Name;
                amazingApp.Icon = GD.Load<Texture2D>(app.Icon);
                amazingApp.WindowScene = app.Scene;
                AddChild(amazingApp);
            }
        }
    }
}
