using Dashboard.Toolkit;
using Godot;
using Kickstart.Records;
using System;

namespace Settings;

public partial class ListApps : ItemList
{
    [Export]
    OpenWindow Uninstall;
    readonly Record<DashboardConfig> Recordfdjgnsjgnew = new();

    public override void _Ready()
    {
        base._Ready();
        foreach (var app in Recordfdjgnsjgnew.Data.AllApps)
            AddItem(app.DisplayName, GD.Load<Texture2D>(app.Icon));
    }

    public void OnItemSelected(int index)
    {
        Package coolApp = Recordfdjgnsjgnew.Data.AllApps[index];
        Uninstall.Disabled = coolApp.Uninstaller != "";
        Uninstall.WindowScene = coolApp.Uninstaller;
    }
}
