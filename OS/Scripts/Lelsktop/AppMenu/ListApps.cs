using Godot;
using System;

public class ListApps : VBoxContainer {
    [Export(PropertyHint.Enum, "All,Accessories,Development,Games,Graphics,Internet,Multimedia,Office,System,Utilities")]
    string Category = "All";

    public override void _Ready() {
        base._Ready();
        UpdateList();
    }

    public void UpdateList() {
        // clear previous list
        foreach (Node mbcicfda in GetChildren()) {
            mbcicfda.QueueFree();
        }

        // get the list stuff
        Lelapp[] apps = new Lelapp[]{};
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
        }

        if (apps.Length == 0) {
            Label epicbruhmoment = new Label {
                Text = "No apps found."
            };
            AddChild(epicbruhmoment);
        } else {
            PackedScene yes = ResourceLoader.Load<PackedScene>("res://OS/Lelsktop/AppMenuApp.tscn");
            foreach (var app in apps){
                DefaultOpenWindowButton amazingApp = yes.Instance<DefaultOpenWindowButton>();
                amazingApp.Text = app.Name;
                amazingApp.Icon = ResourceLoader.Load<Texture>(app.Icon);
                amazingApp.WindowScene = app.Scene;
                AddChild(amazingApp);
            }
        }
    }
}
