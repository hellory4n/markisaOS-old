using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using Dashboard.Toolkit;
using Kickstart.Records;

namespace QuickLaunchSetup;

public partial class RemoveFromQuickLaunch : Button
{
    public Package App;

    public override void _Pressed()
    {
        base._Pressed();
        VBoxContainer yes = GetNode<VBoxContainer>("/root/DashboardInterface/Dock/DockStuff/QuickLaunch");
        foreach (Node quickSettingsThing in yes.GetChildren()) {
            if (quickSettingsThing is OpenWindow) {
                OpenWindow h = GetNode<OpenWindow>(quickSettingsThing.GetPath());
                if (h.WindowScene == App.Executable) {
                    h.QueueFree();
                }
            }
        }

        var m = RecordManager.Load<DashboardConfig>();
        List<Package> pain = m.QuickLaunch;
        // .Remove() is no worky :(
        for (int i = 0; i < pain.Count; i++) {
            if (pain[i].DisplayName == App.DisplayName) {
                pain.RemoveAt(i);
                i--;
            }
        }
        RecordManager.Save(m);

        GetParent().GetParent<ListAppsButQuickLaunch>().UpdateItems();
    }
}
